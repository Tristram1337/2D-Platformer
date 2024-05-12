using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider volumeSlider;
    public Slider sfxVolumeSlider;
    public Toggle fullscreenToggle;
    public TMP_Dropdown resolutionDropdown;
    public Button applyButton;
    bool isFullscreen;
    Resolution[] resolutions;

    private readonly string mainMenu = "Main Menu";

    public static OptionsManager instance;

    private void Awake() // Instantiate
    {
        instance = this;
    }

    void Start()
    {
        // Get available screen resolutions
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new();

        int defaultResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            // If the resolution is 1920x1080, set it as the default resolution
            if (resolutions[i].width == 1920 && resolutions[i].height == 1080)
            {
                defaultResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = defaultResolutionIndex;
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
    }

    public void SetSFXVolume(float volume) // New method to set SFX volume
    {
        float db;

        if (volume != 0)
            db = Mathf.Log10(volume) * 20;
        else
            db = -80; // Set volume to -80dB when slider is at 0

        audioMixer.SetFloat("SFXVolume", db);
    }

    public void SetResolution(int resolutionIndex)
    {
        if (resolutionIndex >= 0 && resolutionIndex < resolutions.Length)
        {
            Resolution resolution = resolutions[resolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, isFullscreen); // Modify this line
        }
        else
        {
            Debug.LogError("Resolution index out of range!");
        }
    }

    public void SetFullscreen(bool isFullscreen)
    {
        this.isFullscreen = isFullscreen;
    }

    public void LoadSettings()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1f);
        sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);
        fullscreenToggle.isOn = PlayerPrefs.GetInt("Fullscreen", 1) == 1;
        SetFullscreen(fullscreenToggle.isOn); // Add this line
        SetResolution(PlayerPrefs.GetInt("Resolution", 18));

    }

    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolumeSlider.value);
        PlayerPrefs.SetInt("Fullscreen", fullscreenToggle.isOn ? 1 : 0);
        PlayerPrefs.SetInt("Resolution", resolutionDropdown.value);
        PlayerPrefs.Save();
        LoadSettings();
    }

    public void DeleteData()
    {
        PlayerPrefs.DeleteAll();
        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].width == 1920 && resolutions[i].height == 1080)
            {
                PlayerPrefs.SetInt("Resolution", i);
                break;
            }
        }
        SceneManager.LoadScene(mainMenu);
        LoadSettings();
    }
}