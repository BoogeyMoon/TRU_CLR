using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Av: Andreas de Freitas och Timmy Alvelöv

//En generisk kula som åker mot spelaren
public class Mob_bullet : Mob_Projectile
{
    protected override void Update()
    {
        base.Update();
        transform.Translate(Vector3.forward * startVelocity * Time.deltaTime); //Åker framåt
    }

    void OnTriggerEnter(Collider coll) //Kollar om den kolliderar med något
    {
        if(coll.gameObject.tag != "Weakpoint" && coll.gameObject.tag != "Bullet" && coll.gameObject.tag != "PatrolPoint" && coll.tag != "Boss") // Ignorerar andra fiender, kulor och patrullpunkter 
        {
            if(coll.gameObject.tag == "Player")
            {
                player.GetComponent<PlayerStats>().ChangeHealth(-damage); //Spelaren tar skada
            }
            Destroy(gameObject);
        }
    }

}
