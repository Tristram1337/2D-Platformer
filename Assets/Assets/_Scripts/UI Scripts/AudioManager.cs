using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public AudioSource menuMusic;
    public AudioSource creditsMusic;
    public AudioSource bossMusic;
    public AudioSource levelCompleteMusic;
    public AudioSource levelFinalCompleteMusic;
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

    public void StopMusic() // Takes care of music before playing anything, could cause overlay of music
    {
        creditsMusic.Stop();
        menuMusic.Stop();
        bossMusic.Stop();
        levelFinalCompleteMusic.Stop();
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

    public void PlayCreditsMusic()
    {
        StopMusic();
        creditsMusic.Play();
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

        //StopMusic();
        levelCompleteMusic.Play();
        if (current != "Level 6")
        {
            StartCoroutine(FadeOutLevelMusic(3.0f)); // Adjust the delay as needed
        }

    }

    public void PlayLevelMusic(int trackToPlay)
    {
        StopMusic();
        levelTracks[trackToPlay].Play();
    }

    public void PlayLevelFinalCompleteMusic()
    {
        // Check if the current level music is playing
        if (IsMusicPlaying())
        {
            // Start fading out the current level music
            StartCoroutine(FadeOutAndPlayFinalCompleteMusic(0f)); // Adjust the delay as needed
        }
        else
        {
            // If no music is playing, play the final complete music immediately
            levelFinalCompleteMusic.Play();
        }
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
    public bool IsMusicPlaying()
    {
        // Check if any of the music sources are playing
        if (menuMusic.isPlaying || bossMusic.isPlaying || levelCompleteMusic.isPlaying || levelFinalCompleteMusic.isPlaying)
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

    private IEnumerator FadeOutLevelMusic(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Start fading out the current level music
        StartCoroutine(FadeOutAudio(2.0f)); // Adjust the fade duration as needed
    }

    private IEnumerator FadeOutAndPlayFinalCompleteMusic(float delay)
    {
        // Start fading out the current level music
        yield return StartCoroutine(FadeOutLevelMusic(delay));

        // After the current music has faded out, play the final complete music
        levelFinalCompleteMusic.Play();
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
            yield break; // No currently playing track found, exit coroutine
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
    }
}