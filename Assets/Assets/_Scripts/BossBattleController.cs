using UnityEngine;

public class BossBattleController : MonoBehaviour
{
    private bool bossActive;

    public GameObject blockers;

    public Transform camPoint;

    private CameraController camController;

    public float cameraMoveSpeed;

    public Transform theBoss;
    public float bossGrowSpeed = 2f;

    public Transform projectileLauncher;
    public float launcherGrowSpeed = 2f;

    public float launcherRotateSpeed = 90f;
    private float launcherRotation;

    public GameObject projectileToFire;
    public Transform[] projectilePoints;

    public float waitToStartShooting;
    public float timeBetweenShots;
    private float shootStartCounter;
    private float shotCounter;
    private int currentShot;

    public Animator bossAnim;
    private bool isWeak;

    void Start()
    {
        camController = FindFirstObjectByType<CameraController>();

        shootStartCounter = waitToStartShooting;
    }

    void Update()
    {
        if (bossActive == true)
        {
            // Camera movement
            camController.transform.position = Vector3.MoveTowards(camController.transform.position, 
                camPoint.position,
                cameraMoveSpeed * Time.deltaTime);

            // Grow boss size to full size
            if (theBoss.localScale != Vector3.one) 
            {
                theBoss.localScale = Vector3.MoveTowards(
                    theBoss.localScale,
                    Vector3.one,
                    bossGrowSpeed * Time.deltaTime);
            }
            // Grow projectile size to full size
            if (projectileLauncher.transform.localScale != Vector3.one)
            {
                projectileLauncher.localScale = Vector3.MoveTowards(
                    projectileLauncher.localScale,
                    Vector3.one,
                    bossGrowSpeed * Time.deltaTime);
            }
            // Rotation of the projectiles
            launcherRotation += launcherRotateSpeed * Time.deltaTime;

            // Resets rotation of projectiles after reaching 360f
            if(launcherRotation > 360f)
            {
                launcherRotation -= 360f;
            }

            projectileLauncher.transform.localRotation = Quaternion.Euler(0f, 0f, launcherRotation);

            // Start shooting
            if(shootStartCounter > 0f)
            {
                shootStartCounter -= Time.deltaTime;

                if(shootStartCounter <= 0f)
                {
                    shotCounter = timeBetweenShots;

                    FireShot();
                }
            }

            if(shotCounter > 0f)
            {
                shotCounter -= Time.deltaTime;

                if(shotCounter <= 0f)
                {
                    shotCounter = timeBetweenShots;

                    FireShot();
                }
            }
        }
    }

    // Activates boss fight
    public void ActivateBossFight()
    {
        bossActive = true;

        // Sets blockers for player 
        blockers.SetActive(true);

        // Sets camera on place
        camController.enabled = false;
    }

    // Takes care of shooting
    void FireShot()
    {
        // Takes projectile from array and shoots it towards the player
        Instantiate(projectileToFire, projectilePoints[currentShot].position, projectilePoints[currentShot].rotation);

        // Deactivate already shot projectile
        projectilePoints[currentShot].gameObject.SetActive(false);

        currentShot++;

        // If all projectiles were shot, reset the counter
        if (currentShot >= projectilePoints.Length)
        {
            shotCounter = 0f;

            MakeWeak();
        }

        
        AudioManager.instance.PlaySFX(2);
    }
    void MakeWeak()
    {
        bossAnim.SetTrigger("isWeak");
        isWeak = true;
    }
}
