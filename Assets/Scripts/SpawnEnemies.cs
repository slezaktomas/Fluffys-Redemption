using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public List<GameObject> doors;
    public List<GameObject> enemyPrefabs = new List<GameObject>();
    public GameObject turretEnemy;
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
                Invoke("CloseDoors", 0.25f);
            }
        }
    }

    void Spawn()
    {
        hasSpawnedEnemies = true;
        bool turret = false;
        Vector3 minBounds = spawnArea.bounds.min;
        Vector3 maxBounds = spawnArea.bounds.max;
        int enemyCount = UnityEngine.Random.Range(1, 5);
        Vector3 enemyContainerPosition = enemyContainer.transform.position;
        Vector3 centerPosition = new Vector3(0f, 1f, 0f) + enemyContainerPosition;
        GameObject tEnemy = Instantiate(turretEnemy, centerPosition, Quaternion.Euler(45,0,0), enemyContainer.transform);
        enemies.Add(tEnemy);
        for (int i = 0; i < enemyCount; i++)
        {
            float randomX = UnityEngine.Random.Range(minBounds.x, maxBounds.x);
            float randomZ = UnityEngine.Random.Range(minBounds.z, maxBounds.z);
            Vector3 randomPosition = new Vector3(randomX, 1f, randomZ);
            GameObject randomEnemy = enemyPrefabs[UnityEngine.Random.Range(0, 1)];
           
            GameObject enemy = Instantiate(randomEnemy, randomPosition, Quaternion.identity, enemyContainer.transform);
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