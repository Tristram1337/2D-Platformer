using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMusicPlayer : MonoBehaviour
{
    public int trackToPlay;

    void Start()
    {
        if(AudioManager.instance != null)
        {
            AudioManager.instance.PlayLevelMusic(trackToPlay);
        }
    }
}
