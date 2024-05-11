using UnityEngine;

public class CollectiblePickup : MonoBehaviour
{
    public int amount = 1;
    public GameObject pickupEffect;

    private void OnTriggerEnter2D(Collider2D other) // Checks for player, if instance of manager exists, adds point and destroys object
    {
        if (other.CompareTag("Player"))
        {
            if (CollectiblesManager.instance != null)
            {
                CollectiblesManager.instance.GetCollectible(amount);

                Destroy(gameObject);

                Instantiate(pickupEffect, transform.position, Quaternion.identity);

                AudioManager.instance.PlaySFXPitched(9);
            }
        }
    }
}