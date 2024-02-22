using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject weaponPanel;
    [SerializeField] private GameObject weapons;
    public Image pickedUpWeaponImage;
    [SerializeField] private Sprite weaponImage;
    [SerializeField] private List<GameObject> walls = new List<GameObject>();
    public string weaponName;
    private bool isWeaponPanelActive = false;
    public float attackRange;
    public float lightRange;
    public int weaponDamage;
    public bool weaponPickedUp = false;
    public static Weapon Instance { get; private set; }
 
    private void Awake() 
    { 
        if (Instance != null && Instance != this) 
        { 
            Debug.Log(""); 
        } 
        else 
        { 
            Instance = this; 
        } 
        weaponPanel.SetActive(false);
    }

    private void Update()
    {
        if (isWeaponPanelActive && Input.GetKeyDown(KeyCode.E))
        {
            weapons.SetActive(false);
            weaponName = gameObject.name;
            weaponPickedUp = true;
            pickedUpWeaponImage.gameObject.SetActive(true);
            pickedUpWeaponImage.sprite = weaponImage;

            
            PlayerAttack.Instance.SetRanges(attackRange, lightRange);
            PlayerAttack.Instance.SetName(weaponName);

            foreach (var wall in walls)
            {
                wall.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            weaponPanel.SetActive(true);
            isWeaponPanelActive = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            weaponPanel.SetActive(false);
            isWeaponPanelActive = false;
        }
    }
}