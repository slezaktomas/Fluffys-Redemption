using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float Hp;
    public static float Damage = 1f;
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
