using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public int doorsDirection;
    [SerializeField] private RoomsType roomsType;
    [SerializeField] private bool isSpawned = false;

    private void Start()
    {
        
        roomsType = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomsType>();
        // Spustí Spawn funkci po malém zpoždění (0.1 sekundy)
        Invoke("Spawn", 0.1f);
    }

    // Funkce pro vygenerování místnosti
    public void Spawn()
    {
        if (isSpawned == false)
        {
            // Podle směru dveří vybereme náhodně místnost
            if (doorsDirection == 1) // Levá místnost
            {
                var rand = Random.Range(0, roomsType.leftRooms.Length);
                var room = Instantiate(roomsType.leftRooms[rand], transform.position, roomsType.leftRooms[rand].transform.rotation);
                roomsType.rooms.Add(room); // Přidáme vygenerovanou místnost do seznamu místností v typu místnosti
            }
            else if (doorsDirection == 2) // Pravá místnost
            {
                var rand = Random.Range(0, roomsType.rightRooms.Length);
                var room = Instantiate(roomsType.rightRooms[rand], transform.position, roomsType.rightRooms[rand].transform.rotation);
                roomsType.rooms.Add(room);
            }
            else if (doorsDirection == 3) // Horní místnost
            {
                var rand = Random.Range(0, roomsType.topRooms.Length);
                var room = Instantiate(roomsType.topRooms[rand], transform.position, roomsType.topRooms[rand].transform.rotation);
                roomsType.rooms.Add(room);
            }
            else if (doorsDirection == 4) // Dolní místnost
            {
                var rand = Random.Range(0, roomsType.bottomRooms.Length);
                var room = Instantiate(roomsType.bottomRooms[rand], transform.position, roomsType.bottomRooms[rand].transform.rotation);
                roomsType.rooms.Add(room);
            }
            isSpawned = true;
        }
    }

    // Detekce kolize s jinými objekty
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Spawnpoint"))
        {
            Destroy(gameObject);
            isSpawned = false;
        }
    }
}
