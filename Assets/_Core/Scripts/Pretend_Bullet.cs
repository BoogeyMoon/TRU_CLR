using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pretend_Bullet : Projectile
{

    // Use this for initialization
    void Start()
    {
        base.Start();
        damage = 3;
        startVelocity = 25;
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
            Destroy(gameObject);
        }

    }
}
