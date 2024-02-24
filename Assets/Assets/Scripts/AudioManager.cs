using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource menuMusic;
    public AudioSource bossMusic;
    public AudioSource levelCompleteMusic;
    public AudioSource[] levelTracks;
    public AudioSource[] allSFX;

    public static AudioManager instance;
    public void Awake() // Instantiate
    {
        // Check null eliminates stacking of the audio in game
        if (instance == null) 
        {
            SetupAudioManager();
        }
        // Deletes in case of bug
        else if(instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void SetupAudioManager()
    {
        instance = this;
        // Takes care of memory allocation, basically keeps this throughout the scenes
        DontDestroyOnLoad(gameObject); 
    }

    public void StopMusic() // Takes care of music before playing anything, could cause overlay of music
    {
        menuMusic.Stop();
        bossMusic.Stop();
        levelCompleteMusic.Stop();

        foreach (AudioSource track in levelTracks)
        {
            track.Stop();
        }
    }
    public void PlayMenuMusic()
    {
        StopMusic();
        menuMusic.Play();
    }

    public void PlayBossMusic()
    {
        StopMusic();
        bossMusic.Play();
    }

    public void PlayLevelCompleteMusic()
    {
        StopMusic();
        levelCompleteMusic.Play();
    }

    public void PlayLevelMusic(int trackToPlay)
    {
        StopMusic();
        levelTracks[trackToPlay].Play();
    }

    public void PlaySFX(int sfxToPlay) // Turns off music before it plays another
    {
        allSFX[sfxToPlay].Stop();
        allSFX[sfxToPlay].Play();
    }

    public void PlaySFXPitched(int sfxToPlay)
    {
        allSFX[sfxToPlay].Stop();

        // Takes care of pitch of certain sfx, allows for more diverse sounds
        allSFX[sfxToPlay].pitch = Random.Range(0.75f, 1.25f); 

        allSFX[sfxToPlay].Play();
    }
}
