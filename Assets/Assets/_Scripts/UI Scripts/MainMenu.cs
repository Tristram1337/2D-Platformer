using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public string firstLevel;
    public int startingLives = 3;
    public int startingFruit = 0;

    public GameObject savedGamesButton; // Possibly only resume / continue button, will see if I can implement saves

    void Start()
    {
        AudioManager.instance.PlayMenuMusic();

        if (PlayerPrefs.HasKey("currentLevel")) // as stated in savedGamesButton comment
        {
            savedGamesButton.SetActive(true);
        }
    }

    void Update()
    {

#if UNITY_EDITOR
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
        InfoTracker.instance.currentLives = startingLives;
        InfoTracker.instance.currentFruit = startingFruit;

        InfoTracker.instance.SaveInfo();

        SceneManager.LoadScene(firstLevel);
    }

    public void SavedGames() // as stated in savedGamesButton comment
    {
        SceneManager.LoadScene(PlayerPrefs.GetString("currentLevel"));
    }

    public void Options()
    {

    }

    public void QuitGame()
    {
        Application.Quit();

        Debug.Log("Exiting");
    }

    public void Informations()
    {

    }
}
