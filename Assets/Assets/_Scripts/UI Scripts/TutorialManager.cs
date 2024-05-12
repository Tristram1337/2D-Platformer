#nullable enable

using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public Tutorial[]? allTutorials;

    public GameObject? movementGuide;
    public GameObject? jumpGuide;
    public GameObject? doubleJumpGuide;
    public GameObject? sprintGuide;
    public GameObject? secretLevel;

    void Start()
    {
        allTutorials = FindObjectsByType<Tutorial>(FindObjectsSortMode.None);

        foreach (Tutorial t in allTutorials)
        {
            t.tutorial = this;
        }
    }
}