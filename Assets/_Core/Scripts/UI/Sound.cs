using UnityEngine;
using UnityEngine.Audio;
//Av Andreas de Freitas
[System.Serializable]
public class Sound
{
    [SerializeField]
    string _name;

    [SerializeField]
    bool _loop;

    [SerializeField]
    AudioClip _clip;

    [Range(0f, 1f)]
    [SerializeField]
    float _volume;

    [Range(.1f, 3f)]
    [SerializeField]
    float _pitch;

    [HideInInspector]
    AudioSource source;
    
    Sliders type;
   
    #region Properties
    public string Name { get { return _name; } }

    public bool Loop { get { return _loop; } }

    public AudioClip Clip { get { return _clip; } }

    public float Volume { get { return _volume; } set { _volume = value; } }

    public float Pitch { get { return _pitch; } set { _pitch = value; } }

    public AudioSource Source { get; set; }

    public Sliders Type { get; set; }
    #endregion 
}
