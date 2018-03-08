using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Av: Andreas de Freitas och Timmy Alvelöv
public class Mob_bullet : Mob_Projectile
{
    void Start()
    {
        base.Start();
        transform.LookAt(player.transform.GetChild(1));
    }

    void Update()
    {
        base.Update();
        transform.Translate(Vector3.forward * startVelocity * Time.deltaTime);
    }

    void OnTriggerEnter(Collider coll)
    {
        if(coll.gameObject.tag != "Weakpoint" && coll.gameObject.tag != "Bullet")
        {
            if(coll.gameObject.tag == "Player")
            {
                player.GetComponent<PlayerStats>().ChangeHealth(-damage);
            }
            Destroy(gameObject);
        }
    }

}
