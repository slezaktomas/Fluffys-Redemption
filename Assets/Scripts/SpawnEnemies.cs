using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public List<GameObject> doors;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private BoxCollider spawnArea;
    [SerializeField] private GameObject enemyContainer; 
    public List<GameObject> enemies = new List<GameObject>();
    private bool hasSpawnedEnemies = false;

    private void Update()
    {
        if (enemies.Count <= 0 && hasSpawnedEnemies)
        {
            Invoke("OpenDoors", 1);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!hasSpawnedEnemies)
            {
                Invoke("CloseDoors", 0.1f);
            }
        }
    }

    void Spawn()
    {
        hasSpawnedEnemies = true;
        Debug.Log("Spawn enemies");
        Vector3 minBounds = spawnArea.bounds.min;
        Vector3 maxBounds = spawnArea.bounds.max;
        int enemyCount = UnityEngine.Random.Range(1, 1);

        for (int i = 0; i < enemyCount; i++)
        {
            float randomX = UnityEngine.Random.Range(minBounds.x, maxBounds.x);
            float randomZ = UnityEngine.Random.Range(minBounds.z, maxBounds.z);
            Vector3 randomPosition = new Vector3(randomX, 0f, randomZ);
            GameObject enemy = Instantiate(enemyPrefab, randomPosition, Quaternion.identity, enemyContainer.transform);
            enemies.Add(enemy);
        }
    }
    void CloseDoors()
    {
        for (int i = 0; i < doors.Count; i++)
        {
            doors[i].SetActive(true);
        }
        Invoke("Spawn", 1);
    }
    void OpenDoors()
    {
        for (int i = 0; i < doors.Count; i++)
        {
            doors[i].SetActive(false);
        }
    }
}