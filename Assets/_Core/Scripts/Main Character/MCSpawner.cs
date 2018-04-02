using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Av Timmy Alvelöv (och Erik Qvarnström på ett hörn)
public class MCSpawner : MonoBehaviour
{
    float timer;
    Transform player;
	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        player.transform.position = transform.position - new Vector3 (0.5f,1,0);
	}
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > 1.5f)
        {
            player.GetComponent<testMCmovement>().enabled = true;
            player.GetComponent<MC_ShootScript>().enabled = true;
            //player.GetComponent<IKHandler>().enabled = true;
            player.GetComponent<Animator>().SetLayerWeight(1, 1);

        }
    }

}
