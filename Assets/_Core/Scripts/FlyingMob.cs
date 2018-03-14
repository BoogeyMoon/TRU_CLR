using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Av Slavko Stojnic och Timmy Alvelöv

//Beskriver den flygande fiendens beteende, så som skjut -och rörelsemönster 
public class FlyingMob : MobStats
{
    [SerializeField]
    float aggroRange, distanceInterval, timeBetweenBurst,shotsPerBurst, rotationBetweenBullets;
    [SerializeField]
    int numberOfBulletsPerShot;
    float playerDistance, closestDistance, burstTimer, burstCounter;
    bool chase;

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
    void Shoot()
    {
        float startRot;
        onCooldown = true;
        if (numberOfBulletsPerShot % 2 ==0)
        {
            startRot = ((numberOfBulletsPerShot / 2)-1) * rotationBetweenBullets;
            startRot += rotationBetweenBullets / 2;
        }
        else
        {
            int n = numberOfBulletsPerShot / 2;
            startRot = n * rotationBetweenBullets;
        }
        for (int i = 0; i < numberOfBulletsPerShot; i++)
        {
            ShootABullet(startRot- rotationBetweenBullets*i);
        }
        burstCounter--;
        if (burstCounter <= 0)
        {
            burstCounter = shotsPerBurst;
            burstTimer = timeBetweenBurst;
        }

        if (onCooldown)
        {
            timeLeft = fireRate;
        }
    }

    void ShootABullet(float RotationOffset)
    {
        currentBullet = Instantiate(bullet);
        
        currentBullet.transform.position = bulletSpawner.transform.position;
        currentBullet.transform.rotation = bulletSpawner.transform.rotation;
        currentBullet.transform.Rotate(RotationOffset, 0, 0);

        
            
        
        
    }
    float GetPlayerDistance(Transform position) //Ger tillbaka avståndet till spelaren med endast x -och yaxlarna i beaktning
    {
        return Mathf.Abs((player.transform.position.x - position.position.x) + (player.transform.position.y - position.position.y));
    }

}
