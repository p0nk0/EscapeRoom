using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
    }

    public List<Sound> sounds;
    private AudioSource audioSource;

    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = GetComponent<AudioSource>();
    }

    public void Play(string soundName)
    {
        Sound s = sounds.Find(x => x.name == soundName);
        if (s != null)
        {
            audioSource.PlayOneShot(s.clip);
        }
        else
        {
            Debug.LogWarning("Sound not found: " + soundName);
        }
    }
}
