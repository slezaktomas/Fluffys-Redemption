using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    public static LoadingManager Instance;
    public GameObject loadingPanel;
    private int targetScene;
    public float minLoadTime;
    public GameObject loadingWheel;
    public float WheelSpeed;
    public bool isLoading;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        loadingPanel.SetActive(false);
    }

    public void LoadScene(int sceneId)
    {
        targetScene = sceneId;
        StartCoroutine(LoadSceneRoutine());
    }

    private IEnumerator LoadSceneRoutine()
    {
        isLoading = true;
        loadingPanel.SetActive(true);
        StartCoroutine(SpinWheelRoutine());
        AsyncOperation op = SceneManager.LoadSceneAsync(targetScene);
        float elapsedLoadTime = 0f;

        while (!op.isDone)
        {
            elapsedLoadTime += Time.deltaTime;
            yield return null; 
        }

        while (elapsedLoadTime < minLoadTime)
        {
            elapsedLoadTime += Time.deltaTime;
            yield return null;
        }
        
        loadingPanel.SetActive(false);
        Debug.Log((elapsedLoadTime));
        isLoading = false;
    }

    private IEnumerator SpinWheelRoutine()
    {
        while (isLoading)
        {
           loadingWheel.transform.Rotate(0,0, -WheelSpeed);
           yield return null;
        }
    }
}
