using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    public static Tutorial tutorial;
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
    public GameObject fadeScreenObject;

    public Image fadeScreen;
    public float fadeSpeed;

    public bool fadingToBlack;
    public bool fadingFromBlack;

    public string mainMenuScene;
    private readonly string firstLevelScene = "Level 1";

    void Start()
    {
        FadeFromBlack();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }

        if (fadingFromBlack)
        {
            fadeScreen.color = new Color(
              fadeScreen.color.r,
              fadeScreen.color.g,
              fadeScreen.color.b,
              Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
        }

        if (fadingToBlack)
        {
            fadeScreen.color = new Color(
              fadeScreen.color.r,
              fadeScreen.color.g,
              fadeScreen.color.b,
              Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
        }
    }

    public void UpdateHealthDisplay(int health, int maxHealth) // Health controller frontend
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
                if (maxHealth <= i)
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
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        InfoTracker.instance.currentLives = 3;
        InfoTracker.instance.currentFruit = 0;

        InfoTracker.instance.SaveInfo();

        SceneManager.LoadScene(firstLevelScene);
        Time.timeScale = 1f;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenuScene);
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Exiting");
    }

    public void PauseUnpause() // Pause screen
    {
        if (pauseScreen.activeSelf == false)
        {
            if (Tutorial.instance != null)
            {
                Tutorial.instance.DeactivateTutorial();
            }
            pauseScreen.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            pauseScreen.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void FadeFromBlack()
    {
        fadeScreenObject.SetActive(true);
        fadingToBlack = false;
        fadingFromBlack = true;
    }

    public void FadeToBlack()
    {
        fadingToBlack = true;
        fadingFromBlack = false;
    }
}