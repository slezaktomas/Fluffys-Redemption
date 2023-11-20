using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HurtPlayer : MonoBehaviour
{
    public Image[] hearths;
    void Start()
    {
        hearths = GetComponentsInChildren<Image>();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && hearths.Length > 0)
        {
            Image hearth = hearths.Last();
            Destroy(hearth);
            
            List<Image> tempHearths = hearths.ToList();
            tempHearths.RemoveAt(tempHearths.Count - 1);
            hearths = tempHearths.ToArray();
        }
    }
    
}
