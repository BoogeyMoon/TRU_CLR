using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Av Timmy Alvelöv

//Beskriver den blå kulans beteende
public class Cyan_Bullet : Projectile, IPoolable
{
    bool _newObj = true;
    public bool Active { get { return active; } set { active = value; PoolableStart(); } }
    protected override void Update() //Åker framåt
    {
        if (active)
        {
            base.Update();
            transform.Translate(Vector3.forward * startVelocity * Time.deltaTime);
            
        }
    }
    protected void PoolableStart()
    {
        if (_newObj)
        {
            _newObj = false;
            Start();
        }
        transform.rotation = rotation.transform.rotation;
        particle.Play(true);
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
                gameObject.GetComponent<ParticleKill>().Kill();
                active = false;
            }
        }

    }
}
