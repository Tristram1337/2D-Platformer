using UnityEngine;
using UnityEngine.SceneManagement;

#nullable enable

public class MainMenuScript : MonoBehaviour
{
    private readonly string firstLevel = "Level 1";
    private readonly string credits = "Credits";
    private readonly string options = "Options";
    private readonly string mainMenu = "Main Menu";

    public int startingLives = 3;
    public int startingFruit = 0;

    public GameObject? savedGamesButton; // Possibly only resume / continue button, will see if I can implement saves

    void Start()
    {
        if (PlayerPrefs.HasKey("currentLevel") && savedGamesButton != null) // as stated in savedGamesButton comment
        {
            savedGamesButton.SetActive(true);
        }
    }

    public void StartGame()
    {
        StopMusic();

        InfoTracker.instance.currentLives = startingLives;
        InfoTracker.instance.currentFruit = startingFruit;

        InfoTracker.instance.SaveInfo();
        SceneManager.LoadScene(firstLevel);
    }

    public void SavedGames() // as stated in savedGamesButton comment
    {
        SceneManager.LoadScene(PlayerPrefs.GetString("currentLevel"));
        AudioManager.instance.StopMusic();
        AudioManager.instance.victoryMusic.Play();
    }

    public void Options()
    {
        SceneManager.LoadScene(options);
    }

    public void OptionsSave()
    {
        OptionsManager.instance.SaveSettings();
        SceneManager.LoadScene(mainMenu);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenu);
    }

    public void Credits()
    {
        SceneManager.LoadScene(credits);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Exiting");
    }

    private void StopMusic()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.StopMusic();
        }
        else
        {
            Debug.LogError("AudioManager instance is null");
        }
    }
}