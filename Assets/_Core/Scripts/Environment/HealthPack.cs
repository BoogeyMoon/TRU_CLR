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

    void Start()
    {
        soundManager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SoundManager>();
    }

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
