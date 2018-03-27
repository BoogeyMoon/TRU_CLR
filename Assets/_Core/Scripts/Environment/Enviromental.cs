using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enviromental : MonoBehaviour {

    protected GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    // Update is called once per frame
    void Update () {
		
	}
}
