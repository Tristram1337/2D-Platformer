using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform[] patrolPoints;

    public Animator anim;

    private int currentPoint;

    public float moveSpeed;

    public float timeAtPoints;

    private float waitCounter;

    public EnemyController enemyController;

    void Start()
    {
        // Remove parent from patrol points, so they stay at predetermined place
        foreach (Transform t in patrolPoints)
        {
            t.SetParent(null);
        }

        waitCounter = timeAtPoints;

        anim.SetBool("isMoving", true);
    }

    void Update()
    {
        if (enemyController.isDefeated == false)
        {
            // Allows for enemy to move towards the points
            transform.position = Vector3.MoveTowards(transform.position, patrolPoints[currentPoint].position, moveSpeed * Time.deltaTime);

            // How close enemy is to the point
            if (Vector3.Distance(transform.position, patrolPoints[currentPoint].position) < 0.001f)
            {
                // Waiting at point
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
                        transform.localScale = new Vector3(1f, 1f, 1f);
                    }
                }
            }
        }
    }
}
