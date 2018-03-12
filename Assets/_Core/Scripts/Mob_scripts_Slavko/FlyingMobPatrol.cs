using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// by Slavko Stojnic

public class FlyingMobPatrol : MobStats {

    [SerializeField] Transform[] points;
    private Transform destinationLocal;
    private int destPoint = 0;
    [SerializeField] float speedWhenAggro;
    private ParticleSystem[] damageParticles;
    private int timesGotHit;
    private bool aggro;
    private float time;
    private float dist;
    [SerializeField]
    Transform target;
    [SerializeField]
    float radiusOfReaction;

    /*
    [SerializeField] Transform bullet;
    private float howOftenToShoot;
    [SerializeField] Transform bulletSpawnPoint;
    private int bulletCount;*/

    void Start () {
        base.Start();
        GotoNextPoint();
        damageParticles = GetComponentsInChildren<ParticleSystem>();
        timesGotHit = 0;
        //target = GameObject.Find("SK_DemoDude_PF").transform;
        aggro = false;
        foreach (ParticleSystem damageParticle in damageParticles)
        {
            var emission = damageParticle.emission;
            emission.enabled = false;
        }
        /*howOftenToShoot = 0.15f;
        bulletCount = 0;
        time = 0.0f;*/
    }

    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (points.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
       destinationLocal = points[destPoint];

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;
    }

    void Update()
    {
        // Choose the next destination point when the agent gets
        // close to the current one.
        if ((Vector3.Distance(destinationLocal.position, transform.position) <= .1) && !aggro)
        {
            GotoNextPoint();
        }

        float step = speed * Time.deltaTime;
        time += Time.deltaTime;
        if (!aggro)
        {
            transform.position = Vector3.MoveTowards(transform.position, destinationLocal.position, step);
        }
        dist = Vector3.Distance(target.position, transform.position);
        if (aggro)
        {
            step = speedWhenAggro * Time.deltaTime;
            if (dist > 10 && dist < radiusOfReaction) // follow MC if he's between 10 and aggro distance
        {
            //transform.LookAt(target.position); // turn towards MC
            Vector2 randomVector = new Vector2(Random.Range(-1.5f, 1.5f), Random.Range(-1.5f, 1.5f));
            transform.Translate(randomVector * Time.deltaTime * 5, Space.World); // jiggle about randomly because it's shitty if the mob stands still
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);

        }
        if ((transform.position.y - target.position.y) < 10) //keep flying above MC, move up if (s)he gets too close
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
        }
    }
        /*
        if (time >= howOftenToShoot && dist < radiusOfReaction) // stop shooting if MC is far away
        {
            if (bulletCount == 0) // shoot five times with .15 second pause (Phase 1)
            { howOftenToShoot = 0.15f; } 
            time = 0.0f;
            
            Instantiate(bullet, bulletSpawnPoint.position, bullet.rotation);
            bulletCount++;

            if (bulletCount == 5)
            {
                howOftenToShoot = 2f; // pause for 2 seconds (Phase 2)
                bulletCount = 0;
            }
        }*/
    }
    public override void TakeDamage(float damage, int color) //Om mob:en blir träffad av en kula som korresponderar med mob:ens färg så tar den skada.
    {
        aggro = true;
        base.TakeDamage(damage, color);
        if ((color == this.color) && (timesGotHit <= damageParticles.Length -1))
        {

            var emission = damageParticles[timesGotHit].emission;
            emission.enabled = true;
            timesGotHit++;
        }


    }
}
