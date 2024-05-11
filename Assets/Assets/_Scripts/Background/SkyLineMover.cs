using UnityEngine;

public class SkyLineMover : MonoBehaviour
{
    public float maxDistance = 22f;

    void Start()
    {

    }

    void Update()
    {
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