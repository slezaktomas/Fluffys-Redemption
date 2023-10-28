using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public float Hp;
    [SerializeField] private float Damage;
    private Image healthBar;
    private Image easeBar;
    private float timer = 0.5f;
    private void Start()
    {
        healthBar = UIManager.Instance.bossHealthBar?.GetComponent<Image>();
        easeBar = UIManager.Instance.easeHealthBar?.GetComponent<Image>();
    }

    private void Update()
    {
        if (Hp <= 0f)
        {
            Destroy(gameObject);
            SpawnBoss.hasDefeatedBoss = true;
        }

        timer -= Time.deltaTime;
        if (timer < 0)
        {
            if (healthBar.fillAmount < easeBar.fillAmount)
            {
                float shrinkSpeed = 0.5f;
                easeBar.fillAmount -= shrinkSpeed * Time.deltaTime;
            }
        }
    }
    public void TakeDamage(float damage)
    {
        healthBar.fillAmount -=  healthBar.fillAmount/Hp;
        Hp -= damage;
    }
}
