using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Kill / Respawn when going out of screen
        if (other.CompareTag("Player"))
        {
            LifeController.instance.Respawn();
        }
    }
}