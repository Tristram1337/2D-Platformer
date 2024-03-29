using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform[] patrolPoints;

    public Animator anim;

    private int currentPoint;
    private int lastPatrolPoint;

    public float moveSpeed;

    public float timeAtPoints;

    private float waitCounter;

    public EnemyController enemyController;

    // New Mechanics for bird
    public bool shouldChasePlayer;
    private bool isChasing;
    public float distanceToChasePlayer;
    private PlayerController thePlayer;

    void Start()
    {
        lastPatrolPoint = 0;
        // Remove parent from patrol points, so they stay at predetermined place
        foreach (Transform t in patrolPoints)
        {
            t.SetParent(null);
        }

        waitCounter = timeAtPoints;

        if (anim == null)
        {
            anim = GetComponent<Animator>();
        }

        anim.SetBool("isMoving", true);

        // New mechanics for bird

        if (shouldChasePlayer == true)
        {
            thePlayer = FindFirstObjectByType<PlayerController>();
        }
    }

    void Update() // This entire part is a shitshow ...
    {
        if (enemyController.isDefeated == false)
        {
            if (shouldChasePlayer == true) // Applies to enemy thats chasing
            {
                if (isChasing == false)
                {
                    if (Vector3.Distance(transform.position, thePlayer.transform.position) < distanceToChasePlayer)
                    {
                        lastPatrolPoint = currentPoint;
                        isChasing = true;
                    }
                }

                else
                {
                    if (Vector3.Distance(transform.position, thePlayer.transform.position) > distanceToChasePlayer)
                    {
                        isChasing = false;
                    }
                    // Reset sprites position based on last patrol point, so that sprites always facing correct position
                    if (transform.position.x < patrolPoints[lastPatrolPoint].position.x)
                    {
                        transform.localScale = new Vector3(-1f, 1f, 1f);
                    }

                    else
                    {
                        transform.localScale = Vector3.one;
                    }
                }
            }

            if (shouldChasePlayer == false || (shouldChasePlayer == true && isChasing == false)) // Applies to everyone
            {
                // Allows for enemy to move towards the points
                transform.position = Vector3.MoveTowards(transform.position, patrolPoints[currentPoint].position, moveSpeed * Time.deltaTime);

                // How close enemy is to given point
                if (Vector3.Distance(transform.position, patrolPoints[currentPoint].position) < .001f)
                {
                    waitCounter -= Time.deltaTime;

                    anim.SetBool("isMoving", false);

                    if (waitCounter <= 0)
                    {
                        currentPoint++;

                        // Change point to the other one, also reset if reaching out of array
                        if (currentPoint >= patrolPoints.Length)
                        {
                            currentPoint = 0;
                        }

                        waitCounter = timeAtPoints;

                        anim.SetBool("isMoving", true);

                        // Changing position of sprite based on the side hes walking towards
                        if (transform.position.x < patrolPoints[currentPoint].position.x)
                        {
                            transform.localScale = new Vector3(-1f, 1f, 1f);
                        }
                        else
                        {
                            transform.localScale = Vector3.one; // new Vector3(1f, 1f, 1f);
                        }
                    }
                }
            }
            else if (isChasing == true)
            {
                transform.position = Vector3.MoveTowards(transform.position, thePlayer.transform.position, moveSpeed * Time.deltaTime);

                if (transform.position.x > thePlayer.transform.position.x)
                {
                    transform.localScale = Vector3.one;
                }
                else
                {
                    transform.localScale = new Vector3(-1f, 1f, 1f);
                }
            }
        }
    }
}
