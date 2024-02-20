using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    public static LoadingManager Instance;
    public GameObject loadingPanel;
    private int targetScene;
    public float minLoadTime;
    public GameObject loadingWheel;
    public float wheelSpeed;

    public bool isLoading;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        if (loadingPanel != null)
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
        if (loadingPanel != null)
            loadingPanel.SetActive(true);

        StartCoroutine(SpinWheelRoutine());

        AsyncOperation op = SceneManager.LoadSceneAsync(targetScene);
        while (!op.isDone)
        {
            yield return null;
        }
        
        float elapsedLoadTime = 0f;
        while (elapsedLoadTime < minLoadTime)
        {
            elapsedLoadTime += Time.deltaTime;
            yield return null;
        }

        if (loadingPanel != null)
            loadingPanel.SetActive(false);
        
        isLoading = false;
    }

    private IEnumerator SpinWheelRoutine()
    {
        while (isLoading)
        {
            if (loadingWheel != null)
                loadingWheel.transform.Rotate(0, 0, -wheelSpeed);

            yield return null;
        }
    }
}