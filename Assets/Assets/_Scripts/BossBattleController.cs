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

    public Transform[] bossMovePoints;
    private int currentMovePoint;
    public float bossMoveSpeed;

    private int currentPhase;
    public GameObject deathEffect;

    void Start()
    {
        camController = FindFirstObjectByType<CameraController>();

        shootStartCounter = waitToStartShooting;

        blockers.transform.SetParent(null); // Blockers on sides would damage player, since they are children of the boss
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

            if (launcherRotation > 360f)
            {
                launcherRotation -= 360f;
            }
            projectileLauncher.transform.localRotation = Quaternion.Euler(0f, 0f, launcherRotation);

            // In how long should the shooting start
            if (shootStartCounter > 0f)
            {
                shootStartCounter -= Time.deltaTime;

                // Once timer reaches 0, initiate shooting

                if (shootStartCounter <= 0f)
                {
                    shotCounter = timeBetweenShots;

                    FireShot();
                }
            }

            // Shooting interval between each shot
            if (shotCounter > 0f)
            {
                // Once shot counter reaches 0, fire another shot

                shotCounter -= Time.deltaTime;
                if (shotCounter <= 0f)
                {
                    shotCounter = timeBetweenShots;

                    FireShot();
                }
            }

            // Move towards point
            if (isWeak == false)
            {
                theBoss.transform.position = Vector3.MoveTowards(
                    theBoss.transform.position,
                    bossMovePoints[currentMovePoint].position,
                    bossMoveSpeed * Time.deltaTime);

                // Move towards remaining points
                if (theBoss.transform.position == bossMovePoints[currentMovePoint].position)
                {
                    currentMovePoint++;

                    // Reset count; else it would reach outside of an array
                    if (currentMovePoint >= bossMovePoints.Length)
                    {
                        currentMovePoint = 0;
                    }
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

        // Sets camera in place
        camController.enabled = false;

        AudioManager.instance.PlayBossMusic();
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

    void MakeWeak() // Animation for boss
    {
        bossAnim.SetTrigger("isWeak");
        isWeak = true;
    }

    private void OnCollisionEnter2D(Collision2D other) // Takes care of damaging boss
    {

        if (other.gameObject.CompareTag("Player"))
        {
            // If boss isnt weak, damage player
            if (isWeak == false)
            {
                PlayerHealthController.instance.DamagePlayer(1);
            }
            // else damage boss
            else
            {
                if (other.transform.position.y > theBoss.position.y) // If wont hit boss, implement timer that will restart the phase for the boss * not implemented
                {

                    bossAnim.SetTrigger("hit");

                    FindFirstObjectByType<PlayerController>().Jump();

                    MoveToNextPhase();
                }
            }
        }
    }

    void MoveToNextPhase()
    {
        currentPhase++;

        // Starts next phase with harder stats, after hitting boss
        if (currentPhase < 3)
        {
            isWeak = false;

            waitToStartShooting *= .5f;
            timeBetweenShots *= .75f;
            bossMoveSpeed *= 1.5f;

            shootStartCounter = waitToStartShooting;

            projectileLauncher.localScale = Vector3.zero;

            // Reload projectiles
            foreach (Transform point in projectilePoints)
            {
                point.gameObject.SetActive(true);
            }

            currentShot = 0;

            AudioManager.instance.PlaySFX(1);


        }
        else
        {
            // End the boss fight
            gameObject.SetActive(false);
            blockers.SetActive(false);

            // Reactivate camera
            camController.enabled = true;

            Instantiate(deathEffect, theBoss.position, Quaternion.identity);

            AudioManager.instance.PlaySFX(16);

            AudioManager.instance.PlayLevelMusic(FindFirstObjectByType<LevelMusicPlayer>().trackToPlay);
        }
    }
}