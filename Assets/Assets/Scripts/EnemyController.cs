using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Animator anim;

    [HideInInspector]
    public bool isDefeated;
    public float waitToDestroy;

    void Start()
    {
        
    }

    void Update()
    {
        // When the enemy is defeated, wait for animation, else it would just destroy enemy immediately
        if(isDefeated == true)
        {
            waitToDestroy -= Time.deltaTime;

            if(waitToDestroy <= 0)
            {
                Destroy(gameObject);

                AudioManager.instance.PlaySFX(5);
            }
        }
    }
    
    private void OnCollisionEnter2D(Collision2D other) // Damage Player
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerHealthController.instance.DamagePlayer(1);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) // Damage Enemy
    {
        if (other.CompareTag("Player"))
        {
            //anim.SetBool("isDamaged", true);
            anim.SetTrigger("isTriggered");

            FindFirstObjectByType<PlayerController>().Jump();

            isDefeated = true;

            AudioManager.instance.PlaySFX(6);
        }
    }
}
