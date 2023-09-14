using UnityEngine;
using System.Collections;

public class RoomSpawner : MonoBehaviour
{
    [SerializeField] private int doorsDirection;
    [SerializeField] private RoomsType roomsType;
    [SerializeField] private bool isSpawned = false;
    private void Start()
    {
        roomsType = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomsType>();
        Invoke("Spawn", 1f);
    }

    private void Spawn()
    {
        if (isSpawned == false)
        {
            if (doorsDirection == 1) //leve mistnosti
            {
                var rand = Random.Range(0, roomsType.leftRooms.Length);
                Instantiate(roomsType.leftRooms[rand], transform.position, roomsType.leftRooms[rand].transform.rotation);
            }
            else if (doorsDirection == 2) //prave mistnosti
            {
                var rand = Random. Range(0, roomsType.rightRooms.Length);
                Instantiate(roomsType.rightRooms[rand], transform.position, roomsType.rightRooms[rand].transform.rotation);
            }
            else if (doorsDirection == 3) //vrchni mistnosti
            {
            
            }
            else if (doorsDirection == 4) //spodni mistnosti
            {
            
            }
            isSpawned = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(("Spawnpoint")) && other.GetComponent<RoomSpawner>().isSpawned == true)
        {
            Destroy(gameObject);
        }
    }
}
