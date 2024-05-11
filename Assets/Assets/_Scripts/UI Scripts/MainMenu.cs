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
        Scene scene = SceneManager.GetActiveScene();
        string currentScene = scene.name;

        if (currentScene != "Credits")
        {
            AudioManager.instance.PlayMenuMusic();
        }

        if (PlayerPrefs.HasKey("currentLevel") && savedGamesButton != null) // as stated in savedGamesButton comment
        {
            savedGamesButton.SetActive(true);
        }
    }


#if UNITY_EDITOR
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M)) // Just for testing
        {
            AudioManager.instance.PlayLevelCompleteMusic();
        }

        if (Input.GetKeyDown(KeyCode.C)) // For testing, delete all data
        {
            PlayerPrefs.DeleteAll();
        }
    }
#endif

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
        StopMusic();
        SceneManager.LoadScene(PlayerPrefs.GetString("currentLevel"));
    }

    public void Options()
    {
        SceneManager.LoadScene(options);
    }

    public void MainMenu()
    {
        StopMusic();
        SceneManager.LoadScene(mainMenu);
    }

    public void Credits()
    {
        StopMusic();
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