using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float Hp;
    [SerializeField] private float Damage;
    public Image healthBar;
    public SpawnEnemies spawnEnemies;
    [SerializeField] private Image easeBar;
    private float timer = 0.5f;
    private void Start()
    {
        spawnEnemies = GetComponentInParent<SpawnEnemies>();
    }
    private void Update()
    {
        if (Hp <= 0)
        {
            Destroy(gameObject);
            if (spawnEnemies != null)
            {
                spawnEnemies.enemies.Remove(gameObject);
            }
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
        healthBar.fillAmount -= healthBar.fillAmount/Hp;
        Hp -= damage;
    }
}