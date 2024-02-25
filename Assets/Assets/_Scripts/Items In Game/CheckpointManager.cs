using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public Checkpoint[] allCheckpoints;

    //private Checkpoint activeCheckpoint;

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

    void Update()
    {
#if UNITY_EDITOR    // Debug function, deactivating all checkpoints *after compiling, doesnt work anymore
        if (Input.GetKeyDown(KeyCode.C))
        {
            DeactivateAllCheckpoints();
        }
    }
#endif
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
        //activeCheckpoint = lastActiveCheckpoint;

        respawnPosition = lastActiveCheckpoint.transform.position;
    }
}
