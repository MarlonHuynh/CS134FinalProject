/*

Purpose: Deals with the logic pausing the game

*/
using UnityEngine;
using Unity.Collections; 
using UnityEngine.SceneManagement;

public class PauseScreen : MonoBehaviour
{
    public bool enabledPauseScreen; 
    public bool isPaused; 
    public GameObject pauseMenu; 

    void Start()
    {
        enabledPauseScreen = true; 
        isPaused = false; 
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && enabledPauseScreen) 
        {
            if (!isPaused){
                pauseMenu.SetActive(true); 
                isPaused = true; 
                Time.timeScale = 0f;
            }
            else if (isPaused)
            {
                pauseMenu.SetActive(false);
                isPaused = false;  
                Time.timeScale = 1f;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && isPaused && enabledPauseScreen) 
        {
            SceneManager.LoadScene("MainMenu");
        }
        
    }

    // Enables ability to pause
    public void enablePauseScreen()
    {
        enabledPauseScreen = true; 
    }

    // Disables ability to pause
    public void disablePauseScreen()
    {
        enabledPauseScreen = false; 
    }
}
