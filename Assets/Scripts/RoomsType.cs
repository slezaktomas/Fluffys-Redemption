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
    public bool spawnedBoss;

    private void Update()
    {
        if (waitTime <= 0)
        {
            for (int i = 0; i < rooms.Count; i++)
            {
                if (i == rooms.Count-1 && spawnedBoss == false)
                {
                    UIManager.Instance.bossIcon.SetActive(true);
                    rooms[i].AddComponent<BossFight>();
                    Instantiate(UIManager.Instance.bossIcon, rooms[i].transform.position, Quaternion.Euler(90f, 0f, 0f));
                    SpawnEnemies spawnEnemies = rooms[i].GetComponent<SpawnEnemies>();
                    Destroy(spawnEnemies);
                    rooms[i].name = "BossRoom";
                    spawnedBoss = true;
                }
            }
        }
        else
        {
            waitTime -= Time.deltaTime;
        }
    }
}
