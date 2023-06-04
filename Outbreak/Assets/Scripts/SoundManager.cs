using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource effectSource;
    [SerializeField] AudioClip[] allMusic;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        PlayMusic(2);
        musicSource.loop = true;
    }

    public void PlaySound(AudioClip clip)
    {
        effectSource.PlayOneShot(clip);
    }

    public void PlayMusic(int index)
    {
        if (allMusic.Length > index && index >= 0)
        {
            musicSource.PlayOneShot(allMusic[index]);
        }
    }
}
