using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    public float speed = 8f;

    private Vector3 direction;

    public float lifetime = 3f;

    void Start()
    {
        direction = (PlayerHealthController.instance.transform.position - transform.position).normalized; // Finds player health controller which is tied directly to the player, and shoots at its location at the time of shooting

        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.position += direction * (Time.deltaTime * speed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealthController.instance.DamagePlayer(1);
            Destroy(gameObject);
        }
    }
}