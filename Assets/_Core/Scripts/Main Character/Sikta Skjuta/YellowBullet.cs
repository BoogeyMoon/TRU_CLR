using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Skapat av Moa Lindgren. Ärver från Projectile.

//Erik har stökat ner, tog bort så det inte va dubbel dropValue och variablarna inte sattes i start.

//Följande script lägger till en ökad gravity till det gula skottet.
public class YellowBullet : Projectile
{

    float dropValue = 0;
    [SerializeField]
    float gravity = 0.5f;
    float zOffSet = -0.85f;
    Vector3 position;

    void Start()
    {
        base.Start();
    }
    
    void Update()
    {
        base.Update();
        dropValue = dropValue - gravity * Time.deltaTime;
        transform.Translate(Vector3.forward * startVelocity * Time.deltaTime);
        //transform.Translate(new Vector3(0, dropValue, 0), Space.World);
        transform.position = new Vector3(transform.position.x, transform.position.y + dropValue, zOffSet);
    }

    protected override void OnTriggerEnter(Collider coll)
    {
        base.OnTriggerEnter(coll);
        if (coll.gameObject.tag != "Player" && coll.gameObject.tag != "Bullet" && coll.gameObject.tag != "PatrolPoint" && coll.gameObject.tag != "Shield")
        {
            if (coll.gameObject.tag == "Weakpoint")
                coll.GetComponent<MobStats>().TakeDamage(damage, color);

            Destroy(gameObject);
        }

    }
}
