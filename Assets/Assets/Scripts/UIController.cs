using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    private void Awake() // Instantiate
    {
        instance = this;
    }

    public Image[] heartIcons;

    public Sprite heartFull, heartEmpty;

    public TMP_Text livesText;
    public TMP_Text scoreText;

    public GameObject gameOverScreen;
    public GameObject pauseScreen;

    public string mainMenuScene;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }
    }

    public void UpdateHealthDisplay(int health, int maxHealth)  // Health controller frontend
    {
        for (int i = 0; i < heartIcons.Length; i++)
        {
            heartIcons[i].enabled = true;

            // Icons for full hearth
            if (health > i)
            {
                heartIcons[i].sprite = heartFull;
            }
            // Icons for empty hearth
            else
            {
                heartIcons[i].sprite = heartEmpty;

                // Checking max health
                if(maxHealth <= i)
                {
                    heartIcons[i].enabled = false;
                }
            }
        }
    }

    public void UpdateLivesDisplay(int currentLives) // Updating text of remaining lives
    {
        livesText.text = currentLives.ToString();
    }

    public void UpdateScoreCollectibles(int amount) // Updates score of collectibles
    {
        scoreText.text = amount.ToString();
    }

    public void ShowGameOver() // Shows game over screen
    {
        gameOverScreen.SetActive(true);
    }

    public void Restart() // Restart button
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1.0f;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenuScene);
        Time.timeScale = 1.0f;
    }

    public void Quit()
    {
        Debug.Log("Exiting");
    }

    public void PauseUnpause() // Pause screen
    {
        if(pauseScreen.activeSelf == false)
        {
            pauseScreen.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            pauseScreen.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}
