using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Av Timmy Alvelöv

//Ett exempel på hur en projektil skulle kunna se ut, används som demaversion
public class Pretend_Bullet : Projectile
{
    // Use this for initialization
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        transform.Translate(Vector3.forward * startVelocity * Time.deltaTime);
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag != "Player")
        {
            if (coll.gameObject.tag == "Weakpoint")
                coll.GetComponent<MobStats>().TakeDamage(damage, color);

            Destroy(gameObject);
        }

    }
}
