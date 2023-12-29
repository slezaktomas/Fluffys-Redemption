using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRoom : MonoBehaviour
{
    public BoxCollider boxCollider;
    public int doorsDirection; // Set this value in the Inspector: 1 for Left, 2 for Right, 3 for Top, 4 for Bottom

    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player has entered this room");
            MoveToNextRoom();
        }
    }

    private void MoveToNextRoom()
    {
        switch(doorsDirection){
            case (1):
                Debug.Log("Left room");
                break;
            case (2):
                Debug.Log("Right room");
                break;
            case (3):   
                Debug.Log("Top room");
                break;
            case (4):   
                Debug.Log("Bottom room");
                break;
        }
    }
}
