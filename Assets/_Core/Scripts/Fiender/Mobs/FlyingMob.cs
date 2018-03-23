using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Av Slavko Stojnic och Timmy Alvelöv

//Beskriver den flygande fiendens beteende, så som rörelsemönster 
public class FlyingMob : MobStats
{
    float closestDistance, timeSinceSeenPlayer, loseTrackOfPlayer;
    bool chase;
    Rigidbody body;

    protected override void Start() //Ger fienden dess starvärden
    {
        body = GetComponent<Rigidbody>();
        base.Start();
        burstTimer = timeBetweenBurst;
        burstCounter = shotsPerBurst;
        closestDistance = aggroRange - distanceInterval;
        loseTrackOfPlayer = 1;
        timeSinceSeenPlayer = 0;

    }
    void Update() //Ser till att rätt metoder anropas när de ska.
    {
        if(CanSeePlayer())
        {
            timeSinceSeenPlayer = 0;
        }
        else
        {
            timeSinceSeenPlayer += Time.deltaTime;
        }
        if(body.velocity != Vector3.zero)
        {
            body.velocity = Vector3.zero;
        }
        playerDistance = GetPlayerDistance(transform);
        timeLeft -= Time.deltaTime;
        burstTimer -= Time.deltaTime;
        ChaseCheck();

        if (playerDistance <= aggroRange)
        {
            if (timeSinceSeenPlayer <= loseTrackOfPlayer)
            {
                if (burstTimer < 0)
                {
                    if (timeLeft < 0)
                    {
                        Shoot();
                    }
                }
                else
                {
                    Move();
                    transform.LookAt(player);
                }
            }
            else
            {
                Patrol();
            }
        }
        else
        {
            Patrol();
        }

        
    }

    void Move() //Styr hur fienden rör sig.
    {
        
        if (chase)
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        else if(closestDistance > playerDistance)
        {
            transform.Translate(-Vector3.forward * Time.deltaTime * speed);
        }


        transform.position = new Vector3 (transform.position.x, transform.position.y, player.transform.position.z);
    }
    void ChaseCheck() //Bestämmer om moben kommer att jaga spelaren eller inte.
    {
        if (playerDistance <= closestDistance && chase)
        {
            chase = false;
        }
        else if (playerDistance > (closestDistance + distanceInterval)*0.7f)
        {
            chase = true;
        }
    }
}
