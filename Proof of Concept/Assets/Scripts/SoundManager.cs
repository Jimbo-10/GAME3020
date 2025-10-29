using UnityEngine;

public class SoundManager : MonoBehaviour
{
    AudioSource audioSource;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
