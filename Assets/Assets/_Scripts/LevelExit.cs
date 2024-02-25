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
                anim.SetTrigger("ended");

                AudioManager.instance.PlayLevelCompleteMusic();

                blockers.SetActive(true); // Block movement after level finishes

                StartCoroutine(EndLevelCo());
            }
        }
    }
    IEnumerator EndLevelCo() // Fade screen
    {
        yield return new WaitForSeconds(waitToEndLevel - fadeTime);

        UIController.instance.FadeToBlack();

        yield return new WaitForSeconds(fadeTime);

        InfoTracker.instance.GetInfo(); // Transfering data from level to level
        InfoTracker.instance.SaveInfo(); // Some kind of save function // PlayerPrefs function idk 

        SceneManager.LoadScene(nextLevel);

        PlayerPrefs.SetString("currentLevel", nextLevel); // for continue / saved button

    }
}
