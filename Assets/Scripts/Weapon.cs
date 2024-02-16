using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject weaponPanel;
    [SerializeField] private GameObject weapons;
    [SerializeField] private Image pickedUpWeaponImage;
    [SerializeField] private Sprite weaponImage;
    [SerializeField] private List<GameObject> walls = new List<GameObject>();
    private string weaponName;
    private bool isWeaponPanelActive = false;

    private void Awake()
    {
        weaponPanel.SetActive(false);
    }

    private void Update()
    {
        if (isWeaponPanelActive && Input.GetKeyDown(KeyCode.E))
        {
            weapons.SetActive(false);
            pickedUpWeaponImage.gameObject.SetActive(true);
            pickedUpWeaponImage.sprite = weaponImage;
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
