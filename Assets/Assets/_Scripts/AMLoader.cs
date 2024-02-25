using UnityEngine;

public class AMLoader : MonoBehaviour
{
    public AudioManager audioManager;
    private void Awake() // For development purpose
    {
        if (AudioManager.instance == null)
        {
            Instantiate(audioManager).SetupAudioManager(); // Should take care of awake bug *rare apparently
        }
    }
}
