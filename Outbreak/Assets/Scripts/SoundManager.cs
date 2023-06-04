using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource effectSource;
    [SerializeField] AudioSource voiceSource;
    [SerializeField] AudioClip[] allMusic;
    [SerializeField] AudioClip[] storeVoicelines;
    [SerializeField] AudioClip[] parkingVoicelines;
    [SerializeField] AudioClip[] beachVoicelines;
    Queue<AudioClip> clipQueue;
    int currentDialogue = 0;
    int currentVoiceline = 0;
    int previousDialogue = 0;

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

        musicSource.loop = true;
        //StartLevel(1);

        //Dialogue 1 - Music 2
        //Dialogue 2 - Music 1
        //Dialogue 3 - Music 0
    }

    public void StartLevel(int level)
    {
        switch (level)
        {
            default:
                PlayMusic(2);
                PlayDialogue(1);
                break;
            case 2:
                PlayMusic(1);
                PlayDialogue(2);
                break;
            case 3:
                PlayMusic(0);
                PlayDialogue(3);
                break;
        }
    }

    public void PlaySound(AudioClip clip)
    {
        effectSource.PlayOneShot(clip);
    }

    public void PlayMusic(int index)
    {
        musicSource.Stop();
        if (allMusic.Length > index && index >= 0)
        {
            musicSource.PlayOneShot(allMusic[index]);
        }
    }

    public void PlayDialogue(int dialogue)
    {
        currentDialogue = dialogue;
        previousDialogue = dialogue;
        currentVoiceline = 0;
    }

    void Update()
    {
        if (voiceSource.isPlaying == false)
        {
            if (previousDialogue == 3)
                musicSource.volume = 0.3f;
            else
                musicSource.volume = 0.6f;

            //if (currentDialogue == 0 && previousDialogue != 0 && previousDialogue != 3)
            //{
            //    StartLevel(previousDialogue + 1);
            //}

            switch (currentDialogue)
            {
                default:
                    break;
                case 1:
                    voiceSource.PlayOneShot(storeVoicelines[currentVoiceline]);
                    currentVoiceline++;
                    if (storeVoicelines.Length == currentVoiceline)
                    {
                        currentDialogue = 0;
                    }
                    break;
                case 2:
                    voiceSource.PlayOneShot(parkingVoicelines[currentVoiceline]);
                    currentVoiceline++;
                    if (parkingVoicelines.Length == currentVoiceline)
                    {
                        currentDialogue = 0;
                    }
                    break;
                case 3:
                    voiceSource.PlayOneShot(beachVoicelines[currentVoiceline]);
                    currentVoiceline++;
                    if (beachVoicelines.Length == currentVoiceline)
                    {
                        currentDialogue = 0;
                    }
                    break;
            }
        }
        else
        {
            if (previousDialogue == 3)
                musicSource.volume = 0.1f;
            else
                musicSource.volume = 0.3f;
        }
    }
}
