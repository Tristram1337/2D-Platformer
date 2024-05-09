using UnityEngine;

public class BossManager : MonoBehaviour
{
    public static BossManager instance;
    public bool bossDefeated;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetBossDefeated(bool defeated)
    {
        bossDefeated = defeated;
    }
}
