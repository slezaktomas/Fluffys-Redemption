using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Hp;
    [SerializeField] private float Damage;
    public SpawnEnemies spawnEnemies;
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
    }
}