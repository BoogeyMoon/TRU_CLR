using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.UI;
//Av Andreas de Freitas && Timmy Alvelöv

public enum Sliders { Music, Efx, Master };
public class AudioManager : MonoBehaviour
{
    [Header("Sound settings")]

    [SerializeField]
    Sound[] _sounds;

    [SerializeField]
    Slider _musicSlider, _efxSlider, _masterSlider;



    public static AudioManager instance;


    void Awake()
    {
        if (instance == null) //Kollar så att vi endast har en audiomanager i scenen
        {
            instance = this;
        }
        else //Om vi har fler så tar vi bort de övriga
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        foreach (Sound s in _sounds) //Hanterar volym, pitch och loop ifrån ljuder till audiosourcen
        {
            s.Source = gameObject.AddComponent<AudioSource>();
            s.Source.clip = s.Clip;

            s.Source.volume = s.Volume;
            s.Source.pitch = s.Pitch;
            s.Source.loop = s.Loop;
            if (s.Name.Substring(0, 2) == "S_")
                s.Type = Sliders.Music;
            else
                s.Type = Sliders.Efx;
        }

        AudioListener.volume = _musicSlider.value; //Sätter ljudets volym till sliderns volym 
    }

    public void RandomizeSfx(AudioClip[] soundList)
    {
        int s = Random.Range(0, soundList.Length);
        Play(soundList[s].name);
    }

    public void OnValueChanged(System.Enum sliderType) //Anpassar volymen med hjälp av slidern
    {
        AudioListener.volume = _musicSlider.value;
    }

    public void Play(string name) //Spelar upp rätt spår som angets i inspektorn
    {
        Sound s = System.Array.Find(_sounds, Sound => Sound.Name == name);

        if (s.Name == null) //Ifall spåret man försöker spela upp inte hittas ges ett felmeddelande
        {
            print("Sound " + name + " not found!");
            return;
        }

        s.Source.Play(); //Spelar upp det valda spåret
    }
}
