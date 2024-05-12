using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public Checkpoint[] allCheckpoints;

    public Vector3 respawnPosition;

    void Start()
    {
        // Sequence of checkpoints does not matter, first passed = first checkpoint
        allCheckpoints = FindObjectsByType<Checkpoint>(FindObjectsSortMode.None);

        foreach (Checkpoint cp in allCheckpoints)
        {
            cp.checkpointManager = this;
        }
        // Initial position of spawn for player
        respawnPosition = FindFirstObjectByType<PlayerController>().transform.position;

    }

    public void DeactivateAllCheckpoints() // Deactivate all checkpoints
    {
        foreach (Checkpoint cp in allCheckpoints)
        {
            cp.DeactivateCheckpoint();
        }
    }

    public void SetActiveCheckpoint(Checkpoint lastActiveCheckpoint) // Last active checkpoint
    {
        DeactivateAllCheckpoints();

        respawnPosition = lastActiveCheckpoint.transform.position;
    }
}