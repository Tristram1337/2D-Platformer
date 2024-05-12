using UnityEngine;

public class DamagePlayer : MonoBehaviour
{

    public Animator anim;
    public int damage;

    private void OnTriggerEnter2D(Collider2D other) // Damage function
    {
        // Check tag for player, if correct, damage him
        if (other.CompareTag("Player"))
        {
            PlayerHealthController.instance.DamagePlayer(damage);
        }
    }
}