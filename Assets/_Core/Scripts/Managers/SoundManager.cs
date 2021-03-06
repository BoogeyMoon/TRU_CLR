﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Av Andreas de Freitas och Slavko Stojnic
//Hanterar alla våra ljud i spelet.
public class SoundManager : MonoBehaviour
{
    [SerializeField]
    AudioSource musicSource;
    [SerializeField]
    AudioSource[] efxSource;
    [SerializeField]
    static SoundManager instance = null;
    [SerializeField]
    AudioClip bossAudio;

    [SerializeField]
    float lowPitchRange = 0.95f, highPitchRange = 1.0f;

    void Awake() //Kollar så det bara finns en instans av soundmanager i scenen
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject); //Hindrar att soundmanagern förstörs vid omladdning av scenen
    }

    public void PlayMusic(AudioClip music) //Används för att spela musik
    {
        musicSource.clip = music;
        musicSource.Play();
    }

    public void PlaySingle(AudioClip clip, int whichSource) //Används för att spela upp enspårsljud
    {
        efxSource[whichSource].clip = clip;
        efxSource[whichSource].Play();
    }

    public void RandomizeSfx(AudioClip[] clips, int whichSource, bool waitForLoop) //Används för att slumpmässigt spela upp ljud och ändra pitchen på dem
    {
        if (clips.Length>0) // if sound clips exist, play them
        {
            int randomIndex = Random.Range(0, clips.Length - 1);
            float randomPitch = Random.Range(lowPitchRange, highPitchRange);
            efxSource[whichSource].pitch = randomPitch;
            efxSource[whichSource].clip = clips[randomIndex];
            if ( ((waitForLoop) && (!efxSource[whichSource].isPlaying)) || (!waitForLoop) ) // check if the sound needs to be played as a short loop or not
            {
                efxSource[whichSource].Play();
            }

        }
    }
    public void ChangeToBossMusic() //Byter låt till bosslåten
    {
        musicSource.clip = bossAudio;
        musicSource.Play();
    }
}
