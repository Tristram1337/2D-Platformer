using UnityEngine;

public class ParentsPlayerToPlatform : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(transform); // Allows to stay on platform

            other.GetComponent<Rigidbody2D>().interpolation = RigidbodyInterpolation2D.None; // Interpolation causes weird movement on platform
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(null); // Allows to get off the platform / investigate if true

            other.GetComponent<Rigidbody2D>().interpolation = RigidbodyInterpolation2D.Interpolate; // Interpolation causes weird movement on platform
        }
    }
}
