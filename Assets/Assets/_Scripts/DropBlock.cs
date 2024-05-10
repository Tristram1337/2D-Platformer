using UnityEngine;

public class DropBlock : MonoBehaviour
{
    private Transform player;

    public float fallSpeed, raiseSpeed;
    public float maxActivationDistance = 5f;
    public float xActivationRange = 0.1f;

    public Transform dropPoint;

    private bool activated;

    private Vector3 startPoint;

    public float waitToFall;
    public float waitToRaise;

    private float fallCounter;
    private float raiseCounter;

    public Animator anim;

    void Start()
    {
        player = PlayerHealthController.instance.transform;

        dropPoint.SetParent(null); // Else the block would fall forever

        startPoint = transform.position;

        fallCounter = waitToFall;
        raiseCounter = waitToRaise;
    }

    void Update()
    {
        // Check if the block is at its starting position and is not activated
        if (activated == false && transform.position == startPoint)
        {
            // Check if the player is vertically below the block and within the activation range
            float xDifference = Mathf.Abs(player.position.x - transform.position.x);
            float yDifference = transform.position.y - player.position.y; // This is positive when the player is below the block

            if (xDifference <= xActivationRange && yDifference > 0 && yDifference <= maxActivationDistance)
            {
                activated = true;
                anim.SetTrigger("blink");
            }
        }

        if (activated == true)
        {
            if (fallCounter > 0)
            {
                fallCounter -= Time.deltaTime;
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, dropPoint.position, fallSpeed * Time.deltaTime);

                if (transform.position == dropPoint.position)
                {
                    ActivateHit();
                }
            }
        }

        // Restart position after countdown
        else
        {
            if (raiseCounter > 0)
            {
                raiseCounter -= Time.deltaTime;
            }
            else
            {
                // Raise up back to starting point
                if (transform.position != startPoint)
                {
                    transform.position = Vector3.MoveTowards(transform.position, startPoint, raiseSpeed * Time.deltaTime);
                }
            }
        }
    }

    void ActivateHit() // Hits player
    {
        activated = false;

        fallCounter = waitToFall;
        raiseCounter = waitToRaise;

        anim.SetTrigger("hit");

        AudioManager.instance.PlaySFX(4);
    }

    private void OnCollisionEnter2D(Collision2D other) // Damages Player on hit
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ActivateHit();

            PlayerHealthController.instance.DamagePlayer(1);
        }
    }
}
