using UnityEngine;

public class BouncePadPlayer : MonoBehaviour
{
    public float bounceAmount;

    public Animator anim;

    private void OnTriggerEnter2D(Collider2D other) // Allows for bounce pad to work
    {
        if (other.CompareTag("Player"))
        {
            anim.SetTrigger("bounce");

            other.GetComponent<PlayerController>().BouncePlayer(bounceAmount);
        }
    }
}
