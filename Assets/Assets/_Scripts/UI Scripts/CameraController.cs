using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    private Vector3 positionStore;

    public Transform clampMin, clampMax;
    public bool clampPosition;
    public bool freezeVertical, freezeHorizontal;
    private float halfWidth, halfHeight;

    public Camera theCamera;

    void Start()
    {
        // Choose players position
        positionStore = transform.position;

        // Delete parents, so that clamping works
        clampMin.SetParent(null);
        clampMax.SetParent(null);

        // Size of camera
        halfHeight = theCamera.orthographicSize;
        halfWidth = theCamera.orthographicSize * theCamera.aspect;
        //Debug.Log("Camera Orthographic Size Height: " + halfHeight);
        //Debug.Log("Camera Orthographic Size Width: " + halfWidth);
    }

    void LateUpdate()
    {
        // Follow Player
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);

        // Freeze pozition - horizontal or vertical
        if (freezeVertical == true)
        {
            transform.position = new Vector3(transform.position.x, positionStore.y, transform.position.z);
        }
        if (freezeHorizontal == true)
        {
            transform.position = new Vector3(positionStore.x, transform.position.y, transform.position.z);

        }

        //Debug.Log("Camera Position Before Clamping: " + transform.position);

        // Clamping, "sticking" camera to a chosen place
        if (clampPosition == true)
        {
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, clampMin.position.x + halfWidth, clampMax.position.x - halfWidth),
                Mathf.Clamp(transform.position.y, clampMin.position.y + halfHeight, clampMax.position.y - halfHeight),
                transform.position.z);
        }

        // Only if the instance is not null move background
        if (ParallaxBackground.instance != null)
        {
            ParallaxBackground.instance.MoveBackground();
        }

        if (ParallaxSkyAttempt.instance != null)
        {
            ParallaxSkyAttempt.instance.MoveBackgroundExtra();
        }

        if (ParallaxSkyAttempt.instance != null)
        {
            ParallaxSkyAttempt.instance.MoveSky();
        }
    }

    // Lines that allow for clamping the screen, shows the selected max and min area
    private void OnDrawGizmos()
    {
        if (clampPosition == true)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(clampMin.position, new Vector3(clampMin.position.x, clampMax.position.y, 0f));
            Gizmos.DrawLine(clampMin.position, new Vector3(clampMax.position.x, clampMin.position.y, 0f));

            Gizmos.DrawLine(clampMax.position, new Vector3(clampMin.position.x, clampMax.position.y, 0f));
            Gizmos.DrawLine(clampMax.position, new Vector3(clampMax.position.x, clampMin.position.y, 0f));
        }
    }
}
