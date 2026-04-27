using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 1f;
    }

    public void NewGame()
    {
        // Wipe save data
        SaveData.hasExistingSave = false;
        SaveData.trueDay = 1;
        SaveData.dayIncludingFillerDays = 1;
        SaveData.AIAngerMeter = 0;
        SaveData.captchaPoints = 0;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SceneManager.LoadScene("MainRoom");
    }

    public void ContinueGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SceneManager.LoadScene("MainRoom");
        // SaveData is untouched so GoalsManager.Start() will restore everything
    }

    public void LoadMainMenu()
    {
        // in case paused
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}