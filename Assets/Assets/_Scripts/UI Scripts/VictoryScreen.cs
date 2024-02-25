using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour
{

    public string mainMenu;

    void Start()
    {
        //PlayerPrefs.DeleteAll();   // Delete all the saved data
    }

    void Update()
    {

    }

    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenu);
    }

    public void QuitGameButton()
    {
        Application.Quit();
    }
}
