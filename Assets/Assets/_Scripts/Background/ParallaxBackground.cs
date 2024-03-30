using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public static ParallaxBackground instance;
    private void Awake() // Instantiate
    {
        instance = this;
    }

    private Transform theCam;
    public Transform sky;
    public Transform treeline;

    [Range(0f, 1f)]
    public float parallaxSpeed;

    void Start() // Attempt to get rid of start glitch, doesnt work now - camera is fucked for a split second
    {
        theCam = Camera.main.transform;

        //sky.position = new Vector3(theCam.position.x, theCam.position.y, sky.position.z);

        //treeline.position = new Vector3(
        //    theCam.position.x * parallaxSpeed,
        //    theCam.position.y * parallaxSpeed,
        //    treeline.position.z);
    }

    void LateUpdate() // Moves background with player, causes split second glitch
    {
        MoveBackground();
    }

    public void MoveBackground()
    {
        sky.position = new Vector3(theCam.position.x, theCam.position.y, sky.position.z);

        treeline.position = new Vector3(
            theCam.position.x * parallaxSpeed,
            theCam.position.y * parallaxSpeed,
            treeline.position.z);
    }
}
