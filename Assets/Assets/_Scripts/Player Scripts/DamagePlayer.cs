using UnityEngine;

public class DamagePlayer : MonoBehaviour
{

    public Animator anim;
    public int damage;

    void Start()
    {

    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)     // Damage function
    {
        // Check tag for player, if correct, damage him
        if (other.CompareTag("Player"))
        {
            // DELETE OBJECT
            //other.gameObject.SetActive(false);

            PlayerHealthController.instance.DamagePlayer(damage);
            //PlayerController.instance.KnockBack();
        }
    }
}
