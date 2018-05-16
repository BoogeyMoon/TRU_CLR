using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enviromental : MonoBehaviour {

    protected GameObject player;
    protected AudioManager soundManager;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        soundManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }
}
