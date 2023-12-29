using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;


public class CameraSwitcher : MonoBehaviour
{

    public GameObject room;
    public CinemachineVirtualCamera vcam;
    public bool isActive;
    [SerializeField] private GameObject roomMinimap;
    
    private void OnTriggerEnter(Collider other) // Metoda pro trigger.
    {
        if(other.CompareTag("Player") && !other.isTrigger) // Pokud vejdeme s postavou s tagem player do box collideru a je�t� se nespustil trigger, tak se hodnota isActive nastav� na true 
        {
            isActive = true;
            if (roomMinimap != null)
            {
                SpriteRenderer color = roomMinimap.GetComponent<SpriteRenderer>();
                color.color = new Color(255,90,0, 100);
            }
        }
            
    }
    private void OnTriggerExit(Collider other) // metoda pro trigger
    {
        if (other.CompareTag("Player")) // Pokud vejdeme s postavou s tagem player do box collideru
        {
            if (other.CompareTag("Player") && !other.isTrigger)// Pokud vejdeme s postavou s tagem player do box collideru a je�t� se nespustil trigger, tak se hodnota isActive nastav� na false
            {
                isActive = false;
            }
        }
    }

    private void FixedUpdate()
    {
        if (isActive) //Pokud je hodnota isActive true, tak se zapne virtu�ln� kamera
        {
            vcam.enabled = true;
        }
        else //Pokud je hodnota isActive false, tak se vypne virtu�ln� kamera
        {
            vcam.enabled = false;
        }
    }
}