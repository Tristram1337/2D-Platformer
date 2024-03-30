using UnityEngine;

public class ParallaxSkyAttempt : MonoBehaviour
{
    public static ParallaxSkyAttempt instance;
    private void Awake() // Instantiate
    {
        instance = this;
    }

    private Transform theCam;
    public Transform background;

    public Transform[] skyLayers; // Array to hold all parallax layers

    [Range(0f, 1f)]
    public float[] skyParallaxSpeed; // Array to hold parallax speeds for each layer

    void Start()
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
        MoveBackgroundExtra();
        MoveSky();
    }

    public void MoveBackgroundExtra()
    {
        background.position = new Vector3(theCam.position.x, theCam.position.y, background.position.z);

        //background2.position = new Vector3(theCam.position.x * parallaxSpeed2, theCam.position.y * parallaxSpeed2, background2.position.z);

        //background3.position = new Vector3(theCam.position.x * parallaxSpeed3, theCam.position.y * parallaxSpeed3, background3.position.z);
    }

    public void MoveSky()
    {
        for (int i = 0; i < skyLayers.Length; i++)
        {
            // Calculate the new position
            Vector3 targetPosition = new Vector3(theCam.position.x * skyParallaxSpeed[i], theCam.position.y * skyParallaxSpeed[i], skyLayers[i].position.z);

            // Smoothly interpolate position
            skyLayers[i].position = Vector3.Lerp(skyLayers[i].position, targetPosition, Time.deltaTime);

        }
    }

}
