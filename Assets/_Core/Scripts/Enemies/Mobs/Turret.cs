using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Av Timmy Alvelöv 

//Beskriver Turretens skjutmönster
public class Turret : MobStats
{
    [SerializeField]
    protected Transform head;

    Animator animator;

    protected override void Start() // Sätter startvärden
    {
        base.Start();
        SetToPlayerPlane(head);
        animator = gameObject.GetComponent<Animator>();

    }

    void Update() //Moben agerar
    {
        LookAtPlayer(head);
        playerDistance = GetPlayerDistance(head.transform);
        timeLeft -= Time.deltaTime;
        burstTimer -= Time.deltaTime;


        if (aggroRange > playerDistance)
        {
            if (burstTimer < 0)
            {
                if (timeLeft < 0)
                {
                    Shoot();
                    animator.SetTrigger("shoot");
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
