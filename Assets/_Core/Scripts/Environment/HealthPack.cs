using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : Enviromental
{

    [SerializeField]
    AudioClip healthSound;
    SoundManager soundManager;

    [SerializeField]
    protected float healthGain;

    void OnTriggerEnter(Collider coll) //När spelaren träffar spikesen
    {
        if (coll.gameObject.tag == "Player")
        {

            player.GetComponent<PlayerStats>().ChangeHealth(+healthGain); //Spelaren tar skada
            Destroy(gameObject);
            // soundManager.PlaySingle(healthSound); // play HealthPack sound
        }
    }


}
