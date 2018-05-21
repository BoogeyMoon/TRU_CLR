using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Skapat av Moa Lindgren. Ärver från Projectile.

//Erik har stökat ner, tog bort så det inte va dubbel dropValue och variablarna inte sattes i start.

//Följande script lägger till en ökad gravity till det gula skottet.
public class YellowBullet : Projectile, IPoolable
{

    float dropValue = 0;
    [SerializeField]
    float gravity = 0.5f;
    float zOffSet = -0.85f;
    Vector3 position;
    bool _newObj = true;
    TrailRenderer trail;
    public bool Active { get { return active; } set { active = value; PoolableStart(); } }

    protected override void Start()
    {
        base.Start();
        trail = transform.GetChild(0).GetComponent<TrailRenderer>();
    }

    protected void PoolableStart() //Ersätter startmetoden för när något tar detta objekt från poolen.
    {
        if (_newObj)
        {
            _newObj = false;
            Start();
        }
        transform.rotation = rotation.transform.rotation;
        particle.Play(true);
        trail.Clear();
        trail.enabled = true;
        dropValue = 0;
        startTime = 0;
    }

    protected override void Update()
    {
        if (active)
        {
            base.Update();
            dropValue = dropValue - gravity * Time.deltaTime;
            transform.Translate(Vector3.forward * startVelocity * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, transform.position.y + dropValue, zOffSet);
        }

    }

    protected override void OnTriggerEnter(Collider coll) //Tar hand om collisioner
    {
        if (active)
        {
            base.OnTriggerEnter(coll);
            if (coll.gameObject.tag != "Player" && coll.gameObject.tag != "Bullet" && coll.gameObject.tag != "PatrolPoint" && coll.gameObject.tag != "Shield")
            {
                if (coll.gameObject.tag == "Weakpoint")
                    coll.GetComponent<MobStats>().TakeDamage(damage, color);
                particle.Stop(true);
                trail.enabled = false;
                trail.Clear();
                _pool.DestroyPool(transform);
            }
        }


    }
}
