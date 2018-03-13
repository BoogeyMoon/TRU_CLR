using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Av Slavko Stojnic och Timmy Alvelöv

//Beskriver den flygande fiendens beteende, så som skjut -och rörelsemönster 
public class FlyingMob : MobStats
{
    [SerializeField]
    float aggroRange, distanceInterval, fireRate, timeBetweenBurst,bulletsPerBurst;
    float playerDistance, closestDistance, burstTimer, burstCounter;
    bool chase;

    protected override void Start() //Ger fienden dess starvärden
    {
        base.Start();
        burstTimer = timeBetweenBurst;
        burstCounter = bulletsPerBurst;
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
    }

    void Move() //Styr hur fienden rör sig.
    {
        transform.LookAt(player);
        
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
    void Shoot()
    {
        currentBullet = Instantiate(bullet);
        onCooldown = true;
        currentBullet.transform.position = bulletSpawner.transform.position;
        currentBullet.transform.rotation = bulletSpawner.transform.rotation;

        burstCounter--;
            
        if(burstCounter <= 0)
        {
            burstCounter = bulletsPerBurst;
            burstTimer = timeBetweenBurst;
        }

        if (onCooldown)
        {
            timeLeft = fireCooldown;
        }
        
    }
    float GetPlayerDistance(Transform position) //Ger tillbaka avståndet till spelaren med endast x -och yaxlarna i beaktning
    {
        return Mathf.Abs((player.transform.position.x - position.position.x) + (player.transform.position.y - position.position.y));
    }

}
