//using UnityEngine;
//using UnityEngine.Audio;
//using UnityEngine.UI;

//public class OptionsManager : MonoBehaviour
//{
//    public AudioMixer audioMixer;
//    public Slider volumeSlider;
//    public Toggle fullscreenToggle;
//    public Button applyButton;

//    void Start()
//    {
//        // Listeners for UI elements
//        volumeSlider.onValueChanged.AddListener(SetVolume);
//        applyButton.onClick.AddListener(SaveSettings);

//        LoadSettings();
//    }

//    public void SetVolume(float volume)
//    {
//        audioMixer.SetFloat("Volume", Mathf.Log10(volume) * 20);
//        SaveSettings();
//    }

//    public void SetFullscreen(bool isFullscreen)
//    {
//        Screen.fullScreen = isFullscreen;
//        SaveSettings();
//    }

//    private void LoadSettings()
//    {
//        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 0.5f);
//        fullscreenToggle.isOn = Screen.fullScreen;
//    }

//    private void SaveSettings()
//    {
//        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
//        PlayerPrefs.SetInt("Fullscreen", fullscreenToggle.isOn ? 1 : 0);
//        PlayerPrefs.Save();
//    }
//}

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider volumeSlider;
    public Slider sfxVolumeSlider;
    public Toggle fullscreenToggle;
    public TMP_Dropdown resolutionDropdown;
    public Button applyButton;

    Resolution[] resolutions;

    void Start()
    {
        // Get available screen resolutions
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        // Listeners for UI elements
        sfxVolumeSlider.onValueChanged.AddListener(SetSFXVolume);
        volumeSlider.onValueChanged.AddListener(SetVolume);
        resolutionDropdown.onValueChanged.AddListener(SetResolution);
        applyButton.onClick.AddListener(SaveSettings);

        LoadSettings();
    }

    public void SetVolume(float volume)
    {
        float db;

        if (volume != 0)
            db = Mathf.Log10(volume) * 20;
        else
            db = -80; // Set volume to -80dB when slider is at 0

        audioMixer.SetFloat("Volume", db);
        SaveSettings();
    }

    public void SetSFXVolume(float volume) // New method to set SFX volume
    {
        float db;

        if (volume != 0)
            db = Mathf.Log10(volume) * 20;
        else
            db = -80; // Set volume to -80dB when slider is at 0

        audioMixer.SetFloat("SFXVolume", db);
        SaveSettings();
    }

    public void SetResolution(int resolutionIndex)
    {
        if (resolutionIndex >= 0 && resolutionIndex < resolutions.Length)
        {
            Resolution resolution = resolutions[resolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
            SaveSettings();
        }
        else
        {
            Debug.LogError("Resolution index out of range!");
        }
    }


    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        SaveSettings();
    }

    private void LoadSettings()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 0.5f);
        sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.5f);
        fullscreenToggle.isOn = PlayerPrefs.GetInt("Fullscreen", 0) == 1;
        // Call SetResolution instead of setting the dropdown value directly
        SetResolution(PlayerPrefs.GetInt("Resolution", 0));
    }

    private void SaveSettings()
    {
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolumeSlider.value);
        PlayerPrefs.SetInt("Fullscreen", fullscreenToggle.isOn ? 1 : 0);
        PlayerPrefs.SetInt("Resolution", resolutionDropdown.value);
        PlayerPrefs.Save();
    }
}