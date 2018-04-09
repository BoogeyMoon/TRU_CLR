using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Av Timmy Alvelöv (och Erik Qvarnström på ett hörn)

//Hanterar starten av varje bana
public class MCSpawner : MonoBehaviour
{
    float timer;
    Transform player, introPlayer;
    CameraManager ourCamera;

	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        introPlayer = GameObject.FindGameObjectWithTag("IntroPlayer").transform;
        introPlayer.GetComponent<Animator>().enabled = false;
        ourCamera = GameObject.FindGameObjectWithTag("Camera").GetComponent<CameraManager>();
	}
    void Update() // Ser till att animationen spelas och att rätt objekt är på rätt plats.
    {
        timer += Time.deltaTime;
        if(timer > 1.5f)
        {
            if(timer > 5f)
            {
                ourCamera.FollowPlayer = true;
                player.position = introPlayer.position;
                Destroy(introPlayer.gameObject);
                Destroy(this);
            }
            introPlayer.GetComponent<Animator>().enabled = true;
            introPlayer.transform.position = transform.position + new Vector3(2f, -1.5f, 0.1f);

            
        }
    }

}
