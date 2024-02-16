using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBoss : MonoBehaviour
{
    public List<GameObject> doors;
    private bool hasSpawnedBoss = false;
    public static bool hasDefeatedBoss = false;

    private void Update()
    {
        if (hasDefeatedBoss)
        {
            UIManager.Instance.bossHealthBar?.SetActive(false);
            UIManager.Instance.easeHealthBar?.SetActive(false);
            Invoke("OpenDoors", 1);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("ANO");
            UIManager.Instance.bossHealthBar?.SetActive(true);
            UIManager.Instance.easeHealthBar?.SetActive(true);
            Invoke("CloseDoors", 1f);
        }
    }

    void Spawn()
    {
        hasSpawnedBoss = true;
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