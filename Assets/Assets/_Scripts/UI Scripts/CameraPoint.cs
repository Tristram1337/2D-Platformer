using UnityEngine;

public class CameraPoint : MonoBehaviour
{
    public Transform camPoint;
    private CameraController camController;
    public float cameraMoveSpeed;
    public bool triggered = false;

    void Start()
    {
        camController = FindFirstObjectByType<CameraController>();
    }

    private void Update()
    {
        if (triggered == true)
        {
            camController.transform.position = Vector3.MoveTowards(camController.transform.position,
                camPoint.position,
                cameraMoveSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            triggered = true;
            camController.enabled = false;
        }
    }
}