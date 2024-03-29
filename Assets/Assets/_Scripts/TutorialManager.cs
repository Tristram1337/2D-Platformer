using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public Tutorial[] allTutorials;

    public GameObject movementGuide;
    public GameObject jumpGuide;
    public GameObject doubleJumpGuide;
    public GameObject sprintGuide;

    // Start is called before the first frame update
    void Start()
    {
        allTutorials = FindObjectsByType<Tutorial>(FindObjectsSortMode.None);

        foreach (Tutorial t in allTutorials)
        {
            t.tutorial = this;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
