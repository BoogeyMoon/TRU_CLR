using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Skapat av Moa Lindgren. Ärver från Projectile.
//Följande script lägger till en ökad gravity till det gula skottet.
public class YellowBullet : Projectile
{

    float dropValue;
    [SerializeField]
    float gravity;
    float zOffSet;
    Vector3 position;
    // Use this for initialization
    void Start()
    {
        base.Start();
        zOffSet = -0.85f;
        dropValue = 0;
        gravity = 0.5f;
    }
    
    void Update()
    {
        base.Update();
        dropValue = dropValue - gravity * Time.deltaTime;
        transform.Translate(Vector3.forward * startVelocity * Time.deltaTime);
        transform.Translate(new Vector3(0, dropValue, 0), Space.World);
        transform.position = new Vector3(transform.position.x, transform.position.y + dropValue, zOffSet);
    }

    protected override void OnTriggerEnter(Collider coll)
    {
        base.OnTriggerEnter(coll);
        if (coll.gameObject.tag != "Player" && coll.gameObject.tag != "Bullet")
        {
            if (coll.gameObject.tag == "Weakpoint")
                coll.GetComponent<MobStats>().TakeDamage(damage, color);

            Destroy(gameObject);
        }

    }
}
