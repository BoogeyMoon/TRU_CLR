using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Av Timmy Alvelöv 

//Beskriver Turretens skjutmönster
public class Turret : MobStats
{
    protected Transform head;

    protected override void Start() // Sätter startvärden
    {
        base.Start();
        head = transform.GetChild(1);
    }
    void Update() //Moben agerar
    {
        LookAtPlayer(head);
        playerDistance = GetPlayerDistance(transform);
        timeLeft -= Time.deltaTime;
        burstTimer -= Time.deltaTime;

        if (aggroRange > GetPlayerDistance(transform))
        {

            if (burstTimer < 0)
            {
                if (timeLeft < 0)
                {
                    Shoot();
                }
            }

        }
    }
}
