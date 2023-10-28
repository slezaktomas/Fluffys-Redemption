using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject PauseMenuUI;

    //Update se vol� jednou za sn�mek
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // Pokud stiskneme esc
        {
            if (GameIsPaused) // pokud se hodnota GameIsPaused rovn� true, tak se zavol� metoda Resume
            {
                Resume();
            }
            else // pokud se hodnota GameIsPaused rovn� false, tak se zavol� metoda Pause
            {
                Pause();
            }

        }
    }
    public void Resume() //Metoda pro vypnut� pause menu. Vypne se zde na�e pause menu, �as se nastav� na jedna a hodnota GameIsPaused se nastav� na false
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    void Pause() //Metoda pro zapnut� pause menu. Zapne se zde na�e pause menu, �as se nastav� na nula a hodnota GameIsPaused se nastav� na true
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu() // Metoda pro na�ten� hlavn�ho menu
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");

    }

    public void QuitGame() // Metoda pro ukon�en� hry
    {
        Application.Quit();

    }





}