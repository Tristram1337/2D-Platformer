using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoSceneLoader : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string sceneToLoad;

    void Start()
    {
        videoPlayer.loopPointReached += LoadScene;
        AudioManager.instance.PlayCreditsMusic();
    }

    void LoadScene(VideoPlayer vp)
    {
        SceneManager.LoadScene(sceneToLoad);
        AudioManager.instance.StopMusic();
    }
}