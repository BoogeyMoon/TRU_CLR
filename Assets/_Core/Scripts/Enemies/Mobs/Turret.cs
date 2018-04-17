using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Av Timmy Alvelöv 

//Beskriver Turretens skjutmönster
public class Turret : MobStats
{
    [SerializeField]
    protected Transform head;

    protected override void Start() // Sätter startvärden
    {
        base.Start();
        SetToPlayerPlane(head);

    }

    void Update() //Moben agerar
    {
        if (head.position.z != player.position.z)
            SetToPlayerPlane(head);
        if (!dead)
        {
            LookAtPlayer(head);
            playerDistance = GetPlayerDistance(head.transform);
            timeLeft -= Time.deltaTime;
            burstTimer -= Time.deltaTime;


            if (aggroRange > playerDistance)
            {
                if (CanSeePlayer())
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
    }
    public void Upgrade() //Gör att fienden blir argare och farligare
    {
        numberOfBulletsPerShot++;
        shotsPerBurst = shotsPerBurst + 2;
    }
}
