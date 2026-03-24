using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] RectTransform settingsPanel;

    [Header("Slide Settings")]
    [SerializeField] float slideSpeed = 8f;
    [SerializeField] Vector2 hiddenPosition;
    [SerializeField] Vector2 visiblePosition;

    bool isAnimating = false;

    [Header("UI")]
    [SerializeField] Slider volumeSlider;

    void Start()
    {
        // make sure settings starts hidden
        settingsPanel.anchoredPosition = hiddenPosition;
        settingsPanel.gameObject.SetActive(false);

        if (volumeSlider != null)
        volumeSlider.value = PlayerPrefs.GetFloat("MasterVolume", 1f);
    }

    public void PlayGame()
    {
        Debug.Log("PlayGame called");
        SceneManager.LoadScene("MainRoom");
    }

    public void QuitGame()
    {
        Debug.Log("Quit pressed");
        Application.Quit();
    }

    public void OpenSettings()
    {
        if (isAnimating) return;
        settingsPanel.gameObject.SetActive(true);
        StartCoroutine(SlidePanel(hiddenPosition, visiblePosition));
    }

    public void CloseSettings()
    {
        if (isAnimating) return;
        StartCoroutine(SlidePanel(visiblePosition, hiddenPosition, deactivateOnDone: true));
    }

    public void SetVolume(float value)
    {
        AudioManager.Instance.SetVolume(value);
    }

    IEnumerator SlidePanel(Vector2 from, Vector2 to, bool deactivateOnDone = false)
    {
        isAnimating = true;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * slideSpeed;
            settingsPanel.anchoredPosition = Vector2.Lerp(from, to, EaseInOut(t));
            yield return null;
        }

        settingsPanel.anchoredPosition = to;
        if (deactivateOnDone) settingsPanel.gameObject.SetActive(false);
        isAnimating = false;
    }

    float EaseInOut(float t)
    {
        t = Mathf.Clamp01(t);
        return t * t * (3f - 2f * t);
    }
}