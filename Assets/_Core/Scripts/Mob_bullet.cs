using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Av: Andreas de Freitas och Timmy Alvelöv

//En generisk kula som åker mot spelaren
public class Mob_bullet : Mob_Projectile
{
    void Start()
    {
        base.Start();
        transform.LookAt(player.transform.GetChild(2).transform.GetChild(0)); // Hittar höften på spelaren (om höften ligger rätt i heirarkin)
    }

    void Update()
    {
        base.Update();
        transform.Translate(Vector3.forward * startVelocity * Time.deltaTime); //Åker framåt
    }

    void OnTriggerEnter(Collider coll) //Kollar om den kolliderar med något
    {
        if(coll.gameObject.tag != "Weakpoint" && coll.gameObject.tag != "Bullet") // Ignorerar andra fiender och kulor 
        {
            if(coll.gameObject.tag == "Player")
            {
                player.GetComponent<PlayerStats>().ChangeHealth(-damage); //Spelaren tar skada
            }
            Destroy(gameObject);
        }
    }

}
