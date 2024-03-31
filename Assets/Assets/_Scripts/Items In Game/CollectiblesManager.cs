using UnityEngine;

public class CollectiblesManager : MonoBehaviour
{
    public static CollectiblesManager instance;
    private void Awake() // Instantiate
    {
        instance = this;
    }

    public int collectibleTotalCount;

    private readonly int extraLifeThreshold = 5;

    void Start()
    {
        collectibleTotalCount = InfoTracker.instance.currentFruit; // Transfering info between levels

        UpdateCollectible(collectibleTotalCount);
    }

    void Update()
    {

    }

    public void GetCollectible(int collectibleScore) // If reaches set threshold, give player bonus life
    {
        collectibleTotalCount += collectibleScore;

        if (collectibleTotalCount >= extraLifeThreshold)
        {
            collectibleTotalCount -= extraLifeThreshold;

            if (LifeController.instance != null)
            {
                LifeController.instance.AddLife();
            }
        }
        UpdateCollectible(collectibleTotalCount);
    }

    public void UpdateCollectible(int collectibleScore)
    {
        if (UIController.instance != null)
        {
            UIController.instance.UpdateScoreCollectibles(collectibleScore);
        }
    }
}
