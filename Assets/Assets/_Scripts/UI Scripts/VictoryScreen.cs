using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour
{
    public string mainMenu;

    void Start()
    {
        // PlayerPrefs.DeleteAll();   // Delete all the saved data

        if (!AudioManager.instance.IsMusicPlaying())
        {
            AudioManager.instance.PlayLevelFinalCompleteMusic();
        }
    }

    public void MainMenu()
    {
        // Initiate fading to black
        VictoryFade.instance.FadeToBlack();

        // Load the main menu scene after a delay (optional)
        Invoke(nameof(LoadMainMenuScene), VictoryFade.instance.fadeSpeed);
    }

    void LoadMainMenuScene()
    {
        AudioManager.instance.StopMusic();
        SceneManager.LoadScene(mainMenu);
    }

    public void QuitGameButton()
    {
        Application.Quit();
    }
}