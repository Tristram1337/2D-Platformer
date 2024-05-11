using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private bool isActive;
    public Animator anim;

    [HideInInspector]
    public CheckpointManager checkpointManager;

    private void OnTriggerEnter2D(Collider2D other) // Activation of checkpoint
    {
        if (other.CompareTag("Player") && isActive == false)
        {
            checkpointManager.SetActiveCheckpoint(this);

            if (anim != null)
            {
                anim.SetBool("flagActive", true);
            }

            isActive = true;

            AudioManager.instance.PlaySFX(3);
        }
    }

    public void DeactivateCheckpoint() // Deactivation of old checkpoints
    {
        if (anim != null)
        {
            anim.SetBool("flagActive", false);
        }

        isActive = false;
    }
}