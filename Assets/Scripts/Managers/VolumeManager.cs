using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    public Slider volumeSlider;

    void Start()
    {
        // Restore saved volume
        volumeSlider.value = SaveData.masterVolume;
        AudioListener.volume = SaveData.masterVolume;

        // Listen for changes
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
    }

    void OnVolumeChanged(float value)
    {
        AudioListener.volume = value;
        SaveData.masterVolume = value;
    }
}