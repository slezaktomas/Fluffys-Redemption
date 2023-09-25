using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField] private GameObject doors;
    [SerializeField] private GameObject enemyPrefab;
    public BoxCollider2D spawnArea;
    public List<GameObject> enemies = new List<GameObject>();
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Invoke("CloseDoors", 2f);
        }
    }

    void Spawn()
    {
        Debug.Log("Spawn enemies");
        Vector3 minBounds = spawnArea.bounds.min;
        Vector3 maxBounds = spawnArea.bounds.max;
        int enemyCount = UnityEngine.Random.Range(4, 8);

        for (int i = 0; i < enemyCount; i++)
        {
            float randomX = UnityEngine.Random.Range(minBounds.x, maxBounds.x);
            float randomY = UnityEngine.Random.Range(minBounds.y, maxBounds.y);
            Vector3 randomPosition = new Vector3(randomX, randomY, 0f);
            Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
            //enemies.Add(enemy);
        }
    }

    void CloseDoors()
    {
        Debug.Log("Zavřeno");
        doors.SetActive(true);
        Invoke("Spawn", 1f);
    }
}