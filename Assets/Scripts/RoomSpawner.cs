using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    [SerializeField] private int doorsDirection;
    [SerializeField] private RoomsType roomsType;
    [SerializeField] private bool isSpawned = false;

    private void Start()
    {
        roomsType = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomsType>();
        Invoke("Spawn",0.1f);
    }

    private void Spawn()
    {
        if (isSpawned == false)
        {
            if (doorsDirection == 1) // Left room
            {
                var rand = Random.Range(0, roomsType.leftRooms.Length);
                var room = Instantiate(roomsType.leftRooms[rand], transform.position, roomsType.leftRooms[rand].transform.rotation);
                roomsType.rooms.Add(room);
            }
            else if (doorsDirection == 2) // Right room
            {
                var rand = Random.Range(0, roomsType.rightRooms.Length);
                var room = Instantiate(roomsType.rightRooms[rand], transform.position, roomsType.rightRooms[rand].transform.rotation);
                roomsType.rooms.Add(room);
            }
            else if (doorsDirection == 3) // Top room
            {
                var rand = Random.Range(0, roomsType.topRooms.Length);
                var room = Instantiate(roomsType.topRooms[rand], transform.position, roomsType.topRooms[rand].transform.rotation);
                roomsType.rooms.Add(room);
            }
            else if (doorsDirection == 4) // Bottom room
            {
                var rand = Random.Range(0, roomsType.bottomRooms.Length);
                var room = Instantiate(roomsType.bottomRooms[rand], transform.position, roomsType.bottomRooms[rand].transform.rotation);
                roomsType.rooms.Add(room);
            }
            isSpawned = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Spawnpoint"))
        {
            Destroy(gameObject);
            isSpawned = false;
        }
    }
}
