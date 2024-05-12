using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public TutorialManager tutorial;

    private float countdown = 1.8f;
    private bool countdownStarted = false;

    public static Tutorial instance;
    private void Awake() // Instantiate
    {
        instance = this;
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
            string colliderName = collidedObject.name;

            if (tutorial != null)
            {
                switch (colliderName)
                {
                    case "Movement":
                    if (tutorial.movementGuide != null)
                        tutorial.movementGuide.SetActive(true);
                    break;

                    case "Jump":
                    if (tutorial.jumpGuide != null)
                        tutorial.jumpGuide.SetActive(true);
                    break;

                    case "Double Jump":
                    if (tutorial.doubleJumpGuide != null)
                        tutorial.doubleJumpGuide.SetActive(true);
                    break;

                    case "Sprint":
                    if (tutorial.sprintGuide != null)
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
    }

    private void ResetCountdown()
    {
        countdownStarted = false;
        countdown = 1.8f;

        DeactivateTutorial();
    }

    public void DeactivateTutorial()
    {
        if (tutorial != null)
        {
            if (tutorial.movementGuide != null)
                tutorial.movementGuide.SetActive(false);
            if (tutorial.jumpGuide != null)
                tutorial.jumpGuide.SetActive(false);
            if (tutorial.doubleJumpGuide != null)
                tutorial.doubleJumpGuide.SetActive(false);
            if (tutorial.sprintGuide != null)
                tutorial.sprintGuide.SetActive(false);
            if (tutorial.secretLevel != null)
                tutorial.secretLevel.SetActive(false);
        }
    }
}