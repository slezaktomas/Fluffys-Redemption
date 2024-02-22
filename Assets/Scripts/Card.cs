using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int value;
    
    public static Card Instance { get; private set; }
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
    }
}
