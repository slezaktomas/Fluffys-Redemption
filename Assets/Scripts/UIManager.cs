using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class UIManager : MonoBehaviour
{
     public GameObject bossHealthBar;
     public GameObject easeHealthBar;
     public GameObject pickedWeapon;
     public GameObject bossIcon;
     public GameObject playerUi;
     public GameObject playerHearts;
     public GameObject bossPanel;
     public GameObject bossCannotPanel;
     private RoomsType roomsType;
     [SerializeField] private GameObject quest;
     private Image questImage;
     [SerializeField] private Sprite questCompleted;
     public int roomsCleared = 0;
     public bool isClearedAll = false;

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

     private void Start()
     {
          roomsType = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomsType>();
          questImage = quest.GetComponent<Image>();
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
          if (roomsType.rooms?.Count - 1 == roomsCleared)
          {
               questImage.sprite = questCompleted;
               isClearedAll = true;
          }
     }
}
