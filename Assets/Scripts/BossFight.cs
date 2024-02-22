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
        if (Input.GetKey(KeyCode.E) && UIManager.Instance.isClearedAll)
        {
            LoadingManager.Instance.LoadScene(2);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enabled = true;
            if (!UIManager.Instance.isClearedAll)
            {
                UIManager.Instance.bossCannotPanel.SetActive(true);
            }
            if (UIManager.Instance.isClearedAll)
            {
                UIManager.Instance.bossPanel.SetActive(true);
                UIManager.Instance.bossCannotPanel.SetActive(false);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.Instance.bossPanel.SetActive(false);
            UIManager.Instance.bossCannotPanel.SetActive(false);
            enabled = false;

        }
    }
}