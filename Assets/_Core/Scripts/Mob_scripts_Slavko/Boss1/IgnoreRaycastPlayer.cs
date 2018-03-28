using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Av Andreas de Freitas och Timmy Alvelöv

//Om spelaren går in i den här collidern sätts spelaren till ignoreRaycast lagret.
public class IgnoreRaycastPlayer : MonoBehaviour
{
    PlayerStats player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }
    void OnTriggerEnter(Collider coll)
    {
        if (coll.transform.tag == "Player")
            player.ChangeLayer(2);
    }
    void OnTriggerExit(Collider coll)
    {
        if (coll.transform.tag == "Player")
            player.ChangeLayer(0);
    }

}
