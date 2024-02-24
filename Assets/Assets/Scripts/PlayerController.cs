using UnityEngine;

[SelectionBase]
public class PlayerController : MonoBehaviour
{
    public static PlayerController instance; 
    private void Awake() // Instantiate
    {
        instance = this;
    }

    public Rigidbody2D rigidBody;

    private float activeSpeed;
    public float moveSpeed;
    public float runSpeed;

    public float jumpForce;
    private bool canDoubleJump;
    private int oneJumpAfterFall;

    private bool isGrounded;
    public Transform groundCheckPoint;
    public float groundCheckRadius;
    public LayerMask whatIsGround;

    public Animator anim;

    public float knockbackLength, knockbackSpeed;
    private float knockbackCounter;

    void Start()
    {

    }

    void Update()
    {
        if (Time.timeScale > 0f) // Takes care of physics in case of pause
        {
            if (knockbackCounter <= 0) // Disables all functions while knockbacked??
            {
                JumpFunction();
                MoveFunction();
                AnimationHandling();
                ChangeSpriteSide();

            }
            else
            {
                knockbackCounter -= Time.deltaTime;
                rigidBody.velocity = new Vector2(knockbackSpeed * -transform.localScale.x, rigidBody.velocity.y);
            }
        }
    }

    void JumpFunction()
    {
        // Animation handling
        anim.SetBool("isDoubleJumping", false);

        // After falling, allows for 1 jump
        if (isGrounded == true)
        {
            oneJumpAfterFall = 1;
        }

        // Jump
        if (Input.GetButtonDown("Jump"))
        {
            // Only after fall
            if (oneJumpAfterFall > 0 && !isGrounded)
            {
                Jump();

                oneJumpAfterFall = 0;
            }
            // Jump from the ground
            if (isGrounded == true)
            {
                Jump();
                canDoubleJump = true;
            }
            // Double jump from the ground
            else if (canDoubleJump == true)
            {
                Jump();
                canDoubleJump = false;
                // Animation handling
                anim.SetBool("isDoubleJumping", true);
            }
        }
    }

    public void KnockBack() // Knockback animation and execution
    {
        rigidBody.velocity = new Vector2(0f, jumpForce * .5f);

        anim.SetTrigger("isKnockingBack");

        knockbackCounter = knockbackLength;
    }

    void AnimationHandling()
    {
        anim.SetFloat("speed", Mathf.Abs(rigidBody.velocity.x));
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("ySpeed", rigidBody.velocity.y);
    }

    void MoveFunction()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, whatIsGround);

        activeSpeed = moveSpeed;

        Run();
        Walk();
    }

    void ChangeSpriteSide() // Switch sprite's sides visually
    {

        if (rigidBody.velocity.x > 0)
        {
            transform.localScale = Vector3.one;
        }
        if (rigidBody.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }

    public void Jump()
    {
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);

        AudioManager.instance.PlaySFXPitched(14);
    }

    void Walk()
    {
        rigidBody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * activeSpeed, rigidBody.velocity.y);
    }

    void Run()
    {
        // Shift for run
        if (Input.GetButton("Fire3"))
        {
            activeSpeed = runSpeed;
        }
    }
}
