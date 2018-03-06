using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Skapat av Moa Lindgren. Ärver från Projectile.
//Följande script lägger till en ökad gravity till det gula skottet.
public class YellowBullet : Projectile
{
    [SerializeField]
    float dropValue;

    // Use this for initialization
    void Start()
    {
        dropValue = 0;
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        dropValue = dropValue - 0.8f;
        base.Update();
        transform.Translate(new Vector3(0, dropValue, 1 * startVelocity) * Time.deltaTime);
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag != "Player")
        {
            if (coll.gameObject.tag == "Weakpoint")
                print("träffa rätt");

            Destroy(gameObject);
        }

    }
}
