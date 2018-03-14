using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Av Slavko Stojnic och Timmy Alvelöv

//Beskriver den flygande fiendens beteende, så som skjut -och rörelsemönster 
public class FlyingMob : MobStats
{
    protected float playerDistance, closestDistance;
    protected bool chase;

    protected override void Start() //Ger fienden dess starvärden
    {
        base.Start();
        burstTimer = timeBetweenBurst;
        burstCounter = shotsPerBurst;
        closestDistance = aggroRange - distanceInterval;
    }
    void Update() //Ser till att rätt metoder anropas när de ska.
    {
        playerDistance = GetPlayerDistance(transform);
        timeLeft -= Time.deltaTime;
        burstTimer -= Time.deltaTime;
        ChaseCheck();

        if (playerDistance <= aggroRange)
        {
            Move();
            if(burstTimer < 0)
            {
                if (timeLeft < 0)
                {
                    Shoot();
                }
            }   
        }
        else
        {
            patrol();
        }
    }

    void Move() //Styr hur fienden rör sig.
    {
        transform.LookAt(player.transform.GetChild(2).transform.GetChild(0));
        
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
