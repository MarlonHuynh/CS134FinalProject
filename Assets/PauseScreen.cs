using UnityEngine;
using Unity.Collections; 
using UnityEngine.SceneManagement;

public class PauseScreen : MonoBehaviour
{
    public bool isPaused; 
    public GameObject pauseMenu; 

    void Start()
    {
        isPaused = false; 
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
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

        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            SceneManager.LoadScene("MainMenu");
        }
        
    }
}
