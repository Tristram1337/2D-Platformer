using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public string firstLevel;

    void Start()
    {
        AudioManager.instance.PlayMenuMusic();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M)) // Just for testing
        {
            AudioManager.instance.PlayLevelCompleteMusic();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(firstLevel);
    }

    public void SavedGames()
    {

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
