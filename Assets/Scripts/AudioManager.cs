using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }


    public void PlayMusic(AudioClip clip)
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource.clip != clip)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }

    public void StopMusic()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
