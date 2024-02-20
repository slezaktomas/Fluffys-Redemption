using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
     public GameObject bossHealthBar;
     public GameObject easeHealthBar;
     public GameObject pickedWeapon;
     //public GameObject bossPrefab;
     public GameObject bossIcon;
     public GameObject playerUi;
     public GameObject playerHearts;
     public static UIManager Instance { get; private set; }
     private void Awake() 
     { 
          if (Instance != null && Instance != this) 
          { 
               Destroy(this); 
          } 
          else 
          { 
               Instance = this; 
          } 
     }

     private void Update()
     {
          if (LoadingManager.Instance?.isLoading == true)
          {
               playerUi.SetActive(false);
          }
          else
          {
               playerUi.SetActive(true);
          }
     }
}
