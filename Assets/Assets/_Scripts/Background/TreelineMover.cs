using UnityEngine;

public class TreelineMover : MonoBehaviour
{
    public float maxDistance = 22f;

    void Update()
    {
        // Adding new treeline based on movement, not implemented for now
        float distance = transform.position.x - Camera.main.transform.position.x;

        if (distance > maxDistance)
        {
            transform.position -= new Vector3(maxDistance * 2f, 0f, 0f);
        }
        if (distance < -maxDistance)
        {
            transform.position += new Vector3(maxDistance * 2f, 0f, 0f);
        }
    }
}