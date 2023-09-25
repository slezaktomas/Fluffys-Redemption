using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float Hp;
    [SerializeField] private float Damage;
    [SerializeField] private GameObject DamageArea;
    private static Player instance;
    private Player() { }
    public static Player GetInstance()
    {
        return instance;
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
