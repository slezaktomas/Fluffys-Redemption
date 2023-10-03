using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private GameObject icon;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && camera != null)
        {
            //camera.transform.position = transform.position;
            //icon.transform.position = transform.position;
        }
    }
}