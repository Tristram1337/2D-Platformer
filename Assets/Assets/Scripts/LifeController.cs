using System.Collections;
using UnityEngine;

public class LifeController : MonoBehaviour
{
    public static LifeController instance; // Static instance
    private void Awake()
    {
        instance = this;
    }

    private PlayerController thePlayer;

    public float respawnDelay;

    public int currentLives;

    public GameObject deathEffect, respawnEffect;

    void Start()
    {   
        // Initial spawn point
        thePlayer = FindFirstObjectByType<PlayerController>();
    }

    void Update()
    {
        
    }

    public void Respawn() // Respawn with full health
    {
        thePlayer.gameObject.SetActive(false);
        // Set speed of player after respawn
        thePlayer.rigidBody.velocity = Vector2.zero;

        currentLives--;

        // If enough lives, continue respawning
        if(currentLives > 0)
        {
            StartCoroutine(RespawnCo());
            
        }
        else if (currentLives <= 0)
        {
            currentLives = 0;

            StartCoroutine(GameOverCo());
            

        }

        UIController.instance.UpdateLivesDisplay(currentLives);

        // Death effect animation
        Instantiate(deathEffect, thePlayer.transform.position, deathEffect.transform.rotation);
    }

    public IEnumerator RespawnCo() // Respawn coroutine, delaying respawn
    {
        yield return new WaitForSeconds(respawnDelay);

        thePlayer.transform.position = FindFirstObjectByType<CheckpointManager>().respawnPosition;
        PlayerHealthController.instance.AddHealth(PlayerHealthController.instance.maxHealth);

        thePlayer.gameObject.SetActive(true);

        Instantiate(respawnEffect, thePlayer.transform.position, Quaternion.identity.normalized);

    }
    public IEnumerator GameOverCo()
    {
        yield return new WaitForSeconds(respawnDelay);

        UIController.instance.ShowGameOver();
        
    }
    public void AddLife()
    {
        currentLives++;
        UIController.instance.UpdateLivesDisplay(currentLives);

        AudioManager.instance.PlaySFX(8);
    }
}
