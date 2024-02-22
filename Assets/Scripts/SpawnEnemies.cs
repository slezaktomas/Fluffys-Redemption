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
    private bool roomCleared = false;
    private bool roomClearedIncremented = false;
    private void Update()
    {
        // Podmínka pro otevření dveří po zabití všech nepřátel
        if (enemies.Count <= 0 && hasSpawnedEnemies)
        {
            Invoke("OpenDoors", 1);
        }

        if (roomCleared && !roomClearedIncremented)
        {
            UIManager.Instance.roomsCleared += 1;
            roomClearedIncremented = true;
        }
    }

    // Metoda volaná při kolizi s jiným objektem
    private void OnTriggerEnter(Collider other)
    {
        // Pokud je objektem hráč
        if (other.CompareTag("Player"))
        {
            // Podmínka pro zavření dveří
            if (!hasSpawnedEnemies)
            {
                Invoke("CloseDoors", 0.25f);
            }
        }
    }

    // Metoda pro spawnutí nepřátel
    void Spawn()
    {
        hasSpawnedEnemies = true;
        bool turret = false;

        // Minimální a maximální hranice spawnovací oblasti
        Vector3 minBounds = spawnArea.bounds.min;
        Vector3 maxBounds = spawnArea.bounds.max;

        // Náhodný počet nepřátel, kteří se mají spawnovat
        int enemyCount = UnityEngine.Random.Range(1, 5);
        Vector3 enemyContainerPosition = enemyContainer.transform.position;
        Vector3 centerPosition = new Vector3(0f, 1f, 0f) + enemyContainerPosition;

        // Spawn střílejícího nepřítele uprostřed místnosti
        GameObject tEnemy = Instantiate(turretEnemy, centerPosition, Quaternion.Euler(45,0,0), enemyContainer.transform);
        enemies.Add(tEnemy);

        // Pro každého nepřítele, který se má spawnovat, vygeneruje náhodnou pozici ve spawnovací oblasti
        for (int i = 0; i < enemyCount; i++)
        {
            float randomX = UnityEngine.Random.Range(minBounds.x, maxBounds.x);
            float randomZ = UnityEngine.Random.Range(minBounds.z, maxBounds.z);
            Vector3 randomPosition = new Vector3(randomX, 1f, randomZ);

            // Náhodný výběr prefabu nepřítele
            GameObject randomEnemy = enemyPrefabs[UnityEngine.Random.Range(0, 1)];

            // náhodný spawn nepřítele
            GameObject enemy = Instantiate(randomEnemy, randomPosition, Quaternion.identity, enemyContainer.transform);
            enemies.Add(enemy);
        }
    }

    // Metoda pro zavření dveří
    void CloseDoors()
    {
        // Aktivace každých dveří
        for (int i = 0; i < doors.Count; i++)
        {
            doors[i].SetActive(true);
        }
        // Spusť spawnutí nepřátel
        Invoke("Spawn", 1);
    }

    // Metoda pro otevření dveří
    void OpenDoors()
    {
        // Deaktivace každých dveří
        for (int i = 0; i < doors.Count; i++)
        {
            doors[i].SetActive(false);
        }

        roomCleared = true;
        
    }
}
