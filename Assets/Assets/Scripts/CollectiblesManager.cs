using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblesManager : MonoBehaviour
{
    public static CollectiblesManager instance;
    private void Awake() // Instantiate
    {
        instance = this;
    }

    public int collectibleTotalCount;

    public int extraLifeThreshold = 2;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void GetCollectible(int collectibleScore)
    {
        collectibleTotalCount += collectibleScore;

        if(collectibleTotalCount >= extraLifeThreshold)
        {
            collectibleTotalCount -= extraLifeThreshold;

            if (LifeController.instance != null)
            {
                LifeController.instance.AddLife();
            }
        }
        UIController.instance.UpdateScoreCollectibles(collectibleScore);
    }
}
