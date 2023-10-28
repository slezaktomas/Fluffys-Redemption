using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RoomsType : MonoBehaviour
{
    public GameObject[] rightRooms;
    public GameObject[] leftRooms;
    public GameObject[] topRooms;
    public GameObject[] bottomRooms;
    public List<GameObject> rooms;
    public GameObject Boss;
    private float waitTime = 2f;
    private bool spawnedBoss;

    private void Update()
    {
        if (waitTime <= 0)
        {
            for (int i = 0; i < rooms.Count; i++)
            {
                if (i == rooms.Count-1 && spawnedBoss == false)
                {
                    Instantiate(Boss, rooms[i].transform.position, quaternion.identity);
                    SpawnEnemies spawnEnemies = rooms[i].GetComponent<SpawnEnemies>();
                    rooms[i].AddComponent<SpawnBoss>();
                    SpawnBoss spawnBoss = rooms[i].GetComponent<SpawnBoss>();
                    spawnBoss.doors = spawnEnemies.doors;
                    Destroy(spawnEnemies);
                    rooms[i].name = "BossRoom";
                    spawnedBoss = true;
                    Debug.Log(("Boss has been spawned!"));
                }
            }
        }
        else
        {
            waitTime -= Time.deltaTime;
        }
    }
}
