using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomsType : MonoBehaviour
{
    public GameObject[] rightRooms;
    public GameObject[] leftRooms;
    public GameObject[] topRooms;
    public GameObject[] bottomRooms;

    public GameObject closedRoom;
    public List<GameObject> rooms;
    public GameObject bossRoomIndicator;
    private float waitTime = 2f;

    private void Update()
    {
        SpawnBoss();
    }

    void SpawnBoss()
    {
        waitTime -= 0.5f;
        for (int i = 0; i < rooms.Count; i++)
        {
            if (i == rooms.Count-1 && waitTime <= 0f)
            {
                Instantiate(bossRoomIndicator, rooms[i].transform.position, rooms[i].transform.rotation);
                Debug.Log(("Boss has been spawned!"));
            }
        }
    }
}
