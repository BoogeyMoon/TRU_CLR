using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Av Timmy Alvelöv

//Ett script som beskriver beteendet som alla projektiler har gemensamt, alla projektiler ärver från detta script.
public class Projectile : MonoBehaviour
{
    protected GameObject rotation;
    [SerializeField]
    protected float startVelocity, damage, startTime, lifeTime;
    [SerializeField]
    protected int color;
    protected ParticleSystem particle;
    protected bool active;
    protected PoolManager _pool;

    protected void Start() // Hittar emptyn för att veta var vi siktar, samt sätter rotationen korrekt.
    {
        rotation = GameObject.Find("ShoulderAim");
        transform.rotation = rotation.transform.rotation;
        lifeTime = 10;
        particle = GetComponent<ParticleSystem>();
        if (color != 2)
            _pool = GameObject.FindGameObjectWithTag("PoolManagers").transform.GetChild(color).GetComponent<PoolManager>();
    }
    protected virtual void Update() //Förstör kulan om den missar kolliders.
    {
        if (active)
        {
            startTime += Time.deltaTime;
            if (startTime >= lifeTime)
            {
                _pool.DestroyPool(transform);
            }
        }

    }

    protected virtual void OnTriggerEnter(Collider coll) //Triggar switchar av rätt färg
    {
        if (active)
        {
            if (coll.transform.gameObject.tag == "Interactable")
            {
                coll.GetComponent<SwitchInteract>().Trigger(color);
                _pool.DestroyPool(transform);
            }
        }

    }
}
