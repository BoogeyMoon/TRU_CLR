using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : Enviromental
{

    [SerializeField]
    AudioClip healthSound;

    [SerializeField]
    protected float healthGain;


    void OnTriggerEnter(Collider coll) //När spelaren träffar spikesen
    {
        if (coll.gameObject.tag == "Player")
        {
            soundManager.PlaySingle(healthSound,2); // play HealthPack sound
            player.GetComponent<PlayerStats>().ChangeHealth(+healthGain); //Spelaren får hälsa
            Destroy(gameObject);
        }
    }


}
