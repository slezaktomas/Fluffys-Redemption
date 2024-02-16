using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossFight : MonoBehaviour
{
    private void Awake()
    {
        enabled = false;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            LoadingManager.Instance.LoadScene(2);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enabled = true;

        }
    }
}