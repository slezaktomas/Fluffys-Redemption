using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HurtPlayer : MonoBehaviour
{
    public Image[] hearths;
    [SerializeField] private GameObject DeadScreen;
    void Start()
    {
        hearths = GetComponentsInChildren<Image>();
        DeadScreen.SetActive(false);
    }
    
    /*void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && hearths.Length > 0)
        {
            Image hearth = hearths.Last();
            Destroy(hearth);
            
            List<Image> tempHearths = hearths.ToList();
            tempHearths.RemoveAt(tempHearths.Count - 1);
            hearths = tempHearths.ToArray();
        }
        if(hearths.Length <=0){
            DeadScreen.SetActive(true);
            Time.timeScale = 0;
        }
    }*/
    public void Hurt(){
        if(hearths.Length > 0){
            Image hearth = hearths.Last();
            Destroy(hearth);
            
            List<Image> tempHearths = hearths.ToList();
            tempHearths.RemoveAt(tempHearths.Count - 1);
            hearths = tempHearths.ToArray();
        }
        if(hearths.Length <=0){
            DeadScreen.SetActive(true);
            Time.timeScale = 0;
        }
    }
    
}
