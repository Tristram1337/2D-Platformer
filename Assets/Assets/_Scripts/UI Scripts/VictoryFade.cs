using UnityEngine;
using UnityEngine.UI;

public class VictoryFade : MonoBehaviour
{
    public GameObject fadeScreenObject;
    public Image fadeScreen;
    public float fadeSpeed;

    public bool fadingToBlack;
    public bool fadingFromBlack;

    public static VictoryFade instance;
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        FadeFromBlack();
    }

    void Update()
    {
        if (fadingFromBlack)
        {
            fadeScreen.color = new Color(
              fadeScreen.color.r,
              fadeScreen.color.g,
              fadeScreen.color.b,
              Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
        }

        if (fadingToBlack)
        {
            fadeScreen.color = new Color(
              fadeScreen.color.r,
              fadeScreen.color.g,
              fadeScreen.color.b,
              Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
        }
    }

    public void FadeFromBlack()
    {
        fadeScreenObject.SetActive(true);
        fadingToBlack = false;
        fadingFromBlack = true;
    }

    public void FadeToBlack()
    {
        fadingToBlack = true;
        fadingFromBlack = false;
    }
}