using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healthToAdd;

    public GameObject pickupEffect;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // If players current health equals to max health, dont add health, otherwise, add health and destroy hearth object
            if (PlayerHealthController.instance.currentHealth != PlayerHealthController.instance.maxHealth)
            {
                PlayerHealthController.instance.AddHealth(healthToAdd);

                Destroy(gameObject);

                Instantiate(pickupEffect, transform.position, transform.rotation);

                AudioManager.instance.PlaySFX(10);
            }
        }
    }
}
