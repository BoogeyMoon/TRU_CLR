using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bosslight : MobStats
{

    [SerializeField]
    Transform[] points;
    private Transform destinationLocal;
    private int destPoint = 0;
    [SerializeField]
    float speedWhenAggro;
    private ParticleSystem[] damageParticles;
    private int timesGotHit;
    private bool aggro;
    private float time;
    private float dist;
    [SerializeField]
    Transform target;
    [SerializeField]
    float radiusOfReaction;
    [SerializeField]
    bool lookAtMc;


    void Start()
    {
        base.Start();

        damageParticles = GetComponentsInChildren<ParticleSystem>();
        target = GameObject.FindWithTag("Player").transform;
        timesGotHit = 0;

        aggro = false;

        foreach (ParticleSystem damageParticle in damageParticles)
        {
            var emission = damageParticle.emission;
            emission.enabled = false;
        }

    }


    void Update()
    {
        // Choose the next destination point when the agent gets
        // close to the current one.

        float step = speed * Time.deltaTime;
        time += Time.deltaTime;

        dist = Vector3.Distance(target.position, transform.position);
        if (aggro)
        {
            step = speedWhenAggro * Time.deltaTime;

            {
                if (lookAtMc)
                {
                    transform.LookAt(target.position); // turn towards MC
                }

                
            }

        }

    }
    public override void TakeDamage(float damage, int color) //Om mob:en blir träffad av en kula som korresponderar med mob:ens färg så tar den skada.
    {
        aggro = true;
        base.TakeDamage(damage, color);
        if ((color == this.color) && (timesGotHit <= damageParticles.Length - 1))
        {
            var emission = damageParticles[timesGotHit].emission;
            emission.enabled = true;
            timesGotHit++;
        }


    }
}