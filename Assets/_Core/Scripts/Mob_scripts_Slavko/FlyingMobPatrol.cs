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
    [SerializeField]
    bool lookAtMc;
    float howMuchToJiggle;

    void Start () {
        base.Start();
        GotoNextPoint();
        damageParticles = GetComponentsInChildren<ParticleSystem>();
        target = GameObject.FindWithTag("Player").transform;
        timesGotHit = 0;
        howMuchToJiggle = 2.5f/maxHealth;
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
        if (destinationLocal != null && (Vector3.Distance(destinationLocal.position, transform.position) <= .1) && !aggro)
        {
            GotoNextPoint();
            //Patrol();
        }

        float step = speed * Time.deltaTime;
        time += Time.deltaTime;
        if (!aggro && destinationLocal !=null)
        {
            transform.position = Vector3.MoveTowards(transform.position, destinationLocal.position, step);
        }
        dist = Vector3.Distance(target.position, transform.position);
        if (aggro)
        {
            step = speedWhenAggro * Time.deltaTime;
            if (dist > 10 && dist < radiusOfReaction) // follow MC if he's between 10 and aggro distance
        {
                if (lookAtMc)
                {
                    transform.LookAt(target.position); // turn towards MC
                }

            Vector2 randomVector = new Vector2(Random.Range(-(howMuchToJiggle*timesGotHit), (howMuchToJiggle * timesGotHit)), Random.Range(-(howMuchToJiggle * timesGotHit), (howMuchToJiggle * timesGotHit)));
            transform.Translate(randomVector * Time.deltaTime * 5, Space.World); // jiggle about randomly because it's shitty if the mob stands still
            // transform.position = Vector2.Lerp(transform.position, (new Vector2(transform.position.x, transform.position.y) + randomVector), 5 * Time.deltaTime); // good effect to turn on when mob is about to die
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);

        }
        if ((transform.position.y - target.position.y) < 10) //keep flying above MC, move up if (s)he gets too close
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
        }
    }

    }
    public override void TakeDamage(float damage, int color) //Om mob:en blir träffad av en kula som korresponderar med mob:ens färg så tar den skada.
    {
        aggro = true;
        base.TakeDamage(damage, color);
        timesGotHit++;
        if ((color == this.color) && (timesGotHit <= damageParticles.Length -1))
        {
            var emission = damageParticles[timesGotHit].emission;
            //print(damageParticles[timesGotHit]);
            emission.enabled = true;

        }


    }
}
