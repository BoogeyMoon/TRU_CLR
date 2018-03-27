using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : Enviromental {

   
    [SerializeField]
    protected float damageOfSpikes;


	void OnTriggerEnter (Collider coll) //När spelaren träffar spikesen
    {
        if (coll.gameObject.tag == "Player")
        {
            player.GetComponent<PlayerStats>().ChangeHealth(-damageOfSpikes); //Spelaren tar skada
            //spelaren "knockas" ifrån grejen 
            //Visa animation - farligt att vara här
           
        }
    }

  
}
