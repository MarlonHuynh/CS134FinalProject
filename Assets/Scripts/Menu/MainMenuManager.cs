using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    public GameObject playButton;
    public GameObject continueButton;


    public GameObject settingsButton;
    public GameObject volumeSlider;
    public GameObject closeSettingsButton;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 1f;
        
        Debug.Log("HasExistingSave: " + SaveData.hasExistingSave);

        if (SaveData.hasExistingSave)
        {
            playButton.SetActive(false);
            continueButton.SetActive(true);
        }
        else
        {
            playButton.SetActive(true);
            continueButton.SetActive(false);
        }
    }

    public void CloseSettings()
    {
        volumeSlider.SetActive(false);
        settingsButton.SetActive(true);
        closeSettingsButton.SetActive(false);
    }

    public void ToggleSettings()
    {
        bool isOpen = volumeSlider.activeSelf;
        volumeSlider.SetActive(!isOpen);
        settingsButton.SetActive(isOpen);
        closeSettingsButton.SetActive(true);
    }
}