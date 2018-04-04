using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Av Timmy Alvelöv (och Erik Qvarnström på ett hörn)
public class MCSpawner : MonoBehaviour
{
    float timer;
    Transform player, introPlayer;
    CameraManager camera;
	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        introPlayer = GameObject.FindGameObjectWithTag("IntroPlayer").transform;
        introPlayer.GetComponent<Animator>().enabled = false;
        camera = GameObject.FindGameObjectWithTag("Camera").GetComponent<CameraManager>();
	}
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > 1.5f)
        {
            if(timer > 5f)
            {
                camera.AnimDone = true;
                player.position = introPlayer.position;
                Destroy(introPlayer.gameObject);
                Destroy(this);
            }
            introPlayer.GetComponent<Animator>().enabled = true;
            introPlayer.transform.position = transform.position + new Vector3(2f, -1.5f, 0.1f);

            
        }
    }

}
