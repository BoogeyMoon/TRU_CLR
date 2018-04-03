using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Av Andreas de Freitas
//Hanterar alla våra ljud i spelet.
public class SoundManager : MonoBehaviour
{
    [SerializeField]
    AudioSource efxSource, musicSource;

    [SerializeField]
    static SoundManager instance = null;

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

    public void PlaySingle(AudioClip clip) //Används för att spela upp enspårsljud
    {
        efxSource.clip = clip;
        efxSource.Play();
    }

    public void RandomizeSfx(params AudioClip[] clips) //Används för att slumpmässigt spela upp ljud och ändra pitchen på dem
    {
        int randomIndex = Random.Range(0, clips.Length-1);
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);
        efxSource.pitch = randomPitch;
        efxSource.clip = clips[randomIndex];
        efxSource.Play();
    }
}
