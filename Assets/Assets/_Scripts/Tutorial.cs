using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public static Tutorial instance;
    private void Awake() // Instantiate
    {
        instance = this;
    }
    //public GameObject guideScreen;

    public TutorialManager tutorial;

    private float countdown = 1.8f;
    private bool countdownStarted = false;

    private void Start()
    {
        //string sceneName = SceneManager.GetActiveScene().name;

        //if (sceneName == "First Level")
        //{
        //    //guideScreen.SetActive(true);
        //}
    }

    public void Update()
    {
        if (countdownStarted)
        {
            countdown -= Time.deltaTime;

            if (countdown <= 0)
            {
                ResetCountdown();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        GameObject collidedObject = this.gameObject;

        if (other.CompareTag("Player") && !countdownStarted)
        {
            countdownStarted = true;
        }

        if (countdownStarted)
        {
            // Check the name of the collider's GameObject
            string colliderName = collidedObject.name;

            if (tutorial != null)
            switch (colliderName)
            {
                case "Movement":
                tutorial.movementGuide.SetActive(true);
                break;

                case "Jump":
                tutorial.jumpGuide.SetActive(true);
                break;

                case "Double Jump":
                tutorial.doubleJumpGuide.SetActive(true);
                break;

                case "Sprint":
                tutorial.sprintGuide.SetActive(true);
                break;

                    case "Secret Level":
                    if (tutorial.secretLevel != null)
                        tutorial.secretLevel.SetActive(true);
                    break;
                default:
                break;
            }
        }
    }

    private void ResetCountdown()
    {
        countdownStarted = false;
        countdown = 1.8f;

        DeactivateTutorial();
    }

    public void DeactivateTutorial()
    {
        tutorial.movementGuide.SetActive(false);
        tutorial.jumpGuide.SetActive(false);
        tutorial.doubleJumpGuide.SetActive(false);
        tutorial.sprintGuide.SetActive(false);
    }
}
