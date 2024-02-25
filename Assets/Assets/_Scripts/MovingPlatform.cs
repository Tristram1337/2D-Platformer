using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform thePlatform;

    public Transform[] movePoints;
    private int currentPoint;

    public float moveSpeed;

    void Update() // Basically can be used for any code that needs to move from point A to B or A B C D etc... Rename, improve readability for implementing in next codes
    {
        thePlatform.position = Vector3.MoveTowards(
            thePlatform.position,
            movePoints[currentPoint].position,
            moveSpeed * Time.deltaTime);

        if (thePlatform.position == movePoints[currentPoint].position)
        {
            currentPoint++;

            if (currentPoint >= movePoints.Length)
            {
                currentPoint = 0;
            }
        }
    }
}
