using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enviromental : MonoBehaviour {

    protected GameObject player;
    protected SoundManager soundManager;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        soundManager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SoundManager>();
    }
}
