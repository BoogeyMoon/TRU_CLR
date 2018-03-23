using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Av Timmy Alvelöv

//Ett exempel på hur en projektil skulle kunna se ut, används som demoversion
public class Pretend_Bullet : Projectile
{
    void Start()
    {
        base.Start();
    }
    
    void Update() //Åker framåt
    {
        base.Update();
        transform.Translate(Vector3.forward * startVelocity * Time.deltaTime);
    }

    protected override void OnTriggerEnter(Collider coll) //Tar hand om collisioner
    {
        base.OnTriggerEnter(coll);
        if (coll.gameObject.tag != "Player" && coll.gameObject.tag != "Bullet")
        {
            if (coll.gameObject.tag == "Weakpoint")
                coll.GetComponent<MobStats>().TakeDamage(damage, color);
            gameObject.GetComponent<ParticleKill>().Kill();
            StartCoroutine(Die());
        }
    }
    IEnumerator Die() //Låter partikeleffekten kollidera innan jag förstör scriptet.
    {
        yield return new WaitForSeconds(0.05f);
        Destroy(this);
    }
}
