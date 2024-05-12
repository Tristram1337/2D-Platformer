using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    public Animator anim;

    private bool isEnding;

    public string nextLevel;

    public float waitToEndLevel = 4f;

    public GameObject blockers;

    public float fadeTime = 1f;

    private void OnTriggerEnter2D(Collider2D other) // Level completed
    {
        if (isEnding == false)
        {
            if (other.CompareTag("Player"))
            {
                isEnding = true;

                if (anim != null)
                {
                    anim.SetTrigger("ended");
                }

                if (nextLevel == "Victory Scene")
                {
                    AudioManager.instance.PlayLevelFinalCompleteMusic(); // So it starts immediately, not in next scene
                }
                else
                {
                    AudioManager.instance.PlayLevelCompleteMusic();
                }

                if (blockers != null)
                {
                    blockers.SetActive(true); // Block movement after level finishes
                }

                StartCoroutine(EndLevelCo());
            }
        }
    }

    IEnumerator EndLevelCo() // Fade screen
    {
        yield return new WaitForSeconds(waitToEndLevel - fadeTime);

        if (UIController.instance != null)
        {
            UIController.instance.FadeToBlack();
        }

        yield return new WaitForSeconds(fadeTime);

        if (InfoTracker.instance != null)
        {
            InfoTracker.instance.GetInfo(); // Transfer data from level to level
            InfoTracker.instance.SaveInfo(); // Save function using PlayerPrefs or similar
        }

        SceneManager.LoadScene(nextLevel);

        if (nextLevel != "Victory Scene")
        {
            PlayerPrefs.SetString("currentLevel", nextLevel); // Save the last scene before the victory scene for the continue button
        }

        PlayerPrefs.SetString("currentLevel", nextLevel); // for continue / saved button
    }
}