using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public AudioSource menuMusic;
    public AudioSource creditsMusic;
    public AudioSource bossMusic;
    public AudioSource levelCompleteMusic;
    public AudioSource victoryMusic;
    public AudioSource[] levelTracks;
    public AudioSource[] allSFX;
    public AudioMixer mainMixer;
    private int currentTrack = 0;

    public static AudioManager instance;

    public void Awake() // Instantiate
    {
        // Check null eliminates stacking of the audio in game
        if (instance == null)
        {
            SetupAudioManager();
        }

        // Deletes in case of bug
        else if (instance != this)
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

    public int GetCurrentTrack()
    {
        return currentTrack;
    }

    public void StopMusic() // Takes care of music before playing anything, could cause overlay of music
    {
        creditsMusic.Stop();
        menuMusic.Stop();
        bossMusic.Stop();
        victoryMusic.Stop();
        levelCompleteMusic.Stop();

        foreach (AudioSource track in levelTracks)
        {
            track.Stop();
        }
    }

    public void PlayBossMusic()
    {
        StopMusic();
        bossMusic.Play();
    }

    public void PlayLevelCompleteMusic()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string current = currentScene.name;

        levelCompleteMusic.Play();

        if (current != "Level 6") // Final Level
        {
            StartCoroutine(FadeOutLevelMusic(3f));
        }
    }

    public void PlayLevelMusic(int trackToPlay)
    {
        currentTrack = GetCurrentTrack();

        // If the requested track is already playing, just return
        if (currentTrack == trackToPlay && IsMusicPlaying())
        {
            return;
        }

        // Update the current track number, prolly useless, will keep just to be safe
        currentTrack = trackToPlay;

        StopMusic();
        levelTracks[trackToPlay].Play();
    }

    private IEnumerator FadeOutLevelMusic(float fadeDuration)
    {
        yield return StartCoroutine(FadeOutAudio(fadeDuration));
    }

    public void PlayLevelFinalCompleteMusic()
    {
        // Check if the current level music is playing
        if (IsMusicPlaying())
        {
            // Start fading out the current level music
            StartCoroutine(FadeOutAndPlayFinalCompleteMusic(0f));
        }
        else
        {
            // If no music is playing, play the final complete music immediately
            victoryMusic.Play();
        }
    }

    private IEnumerator FadeOutAndPlayFinalCompleteMusic(float delay)
    {
        // Start fading out the current level music
        yield return StartCoroutine(FadeOutLevelMusic(delay));

        // After the current music has faded out, play the final complete music
        victoryMusic.Play();
    }

    public void PlaySFX(int sfxToPlay)
    {
        // Turns off sfx before it plays another
        allSFX[sfxToPlay].Stop();
        allSFX[sfxToPlay].Play();
    }

    public void PlaySFXPitched(int sfxToPlay)
    {
        allSFX[sfxToPlay].Stop();

        // Takes care of pitch of certain sfx, allows for more diverse sounds - pitch
        allSFX[sfxToPlay].pitch = Random.Range(0.75f, 1.25f);

        allSFX[sfxToPlay].Play();
    }

    public bool IsMusicPlaying() // Check if any of the music sources are playing
    {
        if (menuMusic.isPlaying || bossMusic.isPlaying || levelCompleteMusic.isPlaying || victoryMusic.isPlaying || creditsMusic.isPlaying)
        {
            return true;
        }

        foreach (var track in levelTracks)
        {
            if (track.isPlaying)
            {
                return true;
            }
        }

        return false;
    }

    private IEnumerator FadeOutAudio(float fadeDuration)
    {
        AudioSource currentLevelTrack = null;
        foreach (var track in levelTracks)
        {
            if (track.isPlaying)
            {
                currentLevelTrack = track;
                break;
            }
        }

        if (currentLevelTrack == null)
        {
            yield break;
        }

        float startVolume = currentLevelTrack.volume;

        // Fade out the volume gradually
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            currentLevelTrack.volume = Mathf.Lerp(startVolume, 0f, t / fadeDuration);
            yield return null;
        }

        // Ensure volume is zero
        currentLevelTrack.volume = 0f;

        // Stop the audio source
        currentLevelTrack.Stop();

        currentLevelTrack.volume = startVolume;
    }
}