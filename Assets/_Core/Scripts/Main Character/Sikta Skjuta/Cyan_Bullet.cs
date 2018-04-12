using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Av Timmy Alvelöv

//Beskriver den blå kulans beteende
public class Cyan_Bullet : Projectile
{
    
    protected override void Update() //Åker framåt
    {
        base.Update();
        transform.Translate(Vector3.forward * startVelocity * Time.deltaTime);
    }

    protected override void OnTriggerEnter(Collider coll) //Tar hand om collisioner
    {
        base.OnTriggerEnter(coll);
        if (coll.gameObject.tag != "Player" && coll.gameObject.tag != "Bullet" && coll.gameObject.tag != "PatrolPoint")
        {
            if (coll.gameObject.tag == "Weakpoint")
                coll.GetComponent<MobStats>().TakeDamage(damage, color);
            gameObject.GetComponent<ParticleKill>().Kill();
            Destroy(this);     //Låter partikeleffekten kollidera innan jag förstör scriptet.
        }
    }
}
