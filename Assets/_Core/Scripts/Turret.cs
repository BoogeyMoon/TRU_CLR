using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MobStats
{
    protected Transform head;

    protected override void Start()
    {
        base.Start();
        head = transform.GetChild(1);
    }
	void Update()
    {
        playerDistance = GetPlayerDistance(transform);
        timeLeft -= Time.deltaTime;
        burstTimer -= Time.deltaTime;

        if (aggroRange > GetPlayerDistance(transform))
        {
            head.LookAt(player);

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
