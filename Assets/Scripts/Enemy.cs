using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Hp;
    [SerializeField] private float Damage;

    private void Update()
    {
        if (Hp <= 0)
        {
            Destroy(gameObject);
            SpawnEnemies spawnEnemies = new SpawnEnemies();
            spawnEnemies.enemies.Remove(gameObject);
        }
    }
}