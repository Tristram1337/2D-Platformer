using UnityEngine;

public class InfoTracker : MonoBehaviour
{
    public static InfoTracker instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            transform.SetParent(null); // Deletes parent after load, else it wouldnt work

            DontDestroyOnLoad(gameObject); // Takes from scene to scene

            if (PlayerPrefs.HasKey("lives"))
            {
                currentLives = PlayerPrefs.GetInt("lives"); // Takes info from the PlayerPrefs "lives"
                currentFruit = PlayerPrefs.GetInt("fruit"); // Takes info from the PlayerPrefs "fruit"
            }
        }
        else
        {
            Destroy(gameObject); // Destroys in case of duplicating
        }
    }

    public int currentLives, currentFruit;

    public void GetInfo()
    {
        if (LifeController.instance != null)
        {
            currentLives = LifeController.instance.currentLives;
        }

        if (CollectiblesManager.instance != null)
        {
            currentFruit = CollectiblesManager.instance.collectibleTotalCount;
        }
    }

    public void SaveInfo()
    {
        PlayerPrefs.SetInt("lives", currentLives);
        PlayerPrefs.SetInt("fruit", currentFruit);
    }
}
