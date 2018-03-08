using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Av: Andreas de Freitas och Timmy Alvelöv
public class Mob_bullet : Mob_Projectile
{
    void Start() //Kollar på spelarens höft
    {
        base.Start();
        transform.LookAt(player.transform.GetChild(1));
    }

    void Update()
    {
        base.Update();
        transform.Translate(Vector3.forward * startVelocity * Time.deltaTime); //Hur projektilen färdas
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag != "Weakpoint") //Ifall spelaren missar weakpointen på mob:en
        {
            if (coll.gameObject.tag == "Player")
            {
                player.GetComponent<PlayerStats>().ChangeHealth(-damage); //Minskar spelarens hälsa 
            }
            Destroy(gameObject); //Förstör mob:en
        }
    }

}
