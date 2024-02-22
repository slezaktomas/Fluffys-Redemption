using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class UIManager2 : MonoBehaviour
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

   
        public static UIManager2 Instance { get; private set; }
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
