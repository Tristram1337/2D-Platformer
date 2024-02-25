using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;
    private void Awake() // Instantiate
    {
        instance = this;
    }

    public int currentHealth, maxHealth;

    public float invincibilityLength;
    private float invincibilityCounter;

    public SpriteRenderer spriteRenderer;
    public Color normalColor, fadeColor;

    void Start()
    {
        currentHealth = maxHealth;

        UpdateHealthDisplay();
    }

    void Update()
    {
        // Starts a countdown after taking damage; Allows for temporary invincibility
        if (invincibilityCounter > 0)
        {
            invincibilityCounter -= Time.deltaTime;

            if (invincibilityCounter <= 0)
            {
                spriteRenderer.color = normalColor;
            }
        }

#if UNITY_EDITOR // Functions only in Unity, not after compilation, debug function
        if (Input.GetKeyDown(KeyCode.H))
        {
            AddHealth(1);
        }
#endif
    }

    public void DamagePlayer(int damage)   // Health cntroller backend, updates after taking damage
    {
        // When invincibility counter is at 0, allows for taking damage
        if (invincibilityCounter <= 0)
        {
            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                currentHealth = 0;

                //gameObject.SetActive(false); // Dont destroy anymore

                // Respawn the player
                LifeController.instance.Respawn();

            }
            else
            {
                invincibilityCounter = invincibilityLength;

                spriteRenderer.color = fadeColor;

                PlayerController.instance.KnockBack();

                AudioManager.instance.PlaySFX(13); // if I put this in front of the instance of life controller, it doesnt allow it, wtf
            }
            UpdateHealthDisplay();
        }
    }

    public void AddHealth(int healthBoost)
    {
        currentHealth += healthBoost;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UpdateHealthDisplay();
    }

    public void UpdateHealthDisplay() // Update health after taking damage, frontend
    {
        UIController.instance.UpdateHealthDisplay(currentHealth, maxHealth);
    }
}
