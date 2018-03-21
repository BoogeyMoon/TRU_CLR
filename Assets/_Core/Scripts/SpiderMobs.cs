using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class SpiderMobs : MobStats
{

    NavMeshAgent agent;
    Rigidbody body;
    Transform turret;

    protected override void Start()
    {
        base.Start();
        agent = GetComponent<NavMeshAgent>();
        body = GetComponent<Rigidbody>();
        turret = transform.GetChild(1).GetChild(0).GetChild(5).GetChild(0); //Hämtar barnet Turret_JNT
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        playerDistance = GetPlayerDistance(transform);
        if (body.velocity != Vector3.zero)
        {
            body.velocity = Vector3.zero;
        }
        turret.LookAt(player);
        if (destination != null)
        {
            Move();
        }
        
        if (playerDistance < aggroRange && timeLeft < 0)
        {
            Shoot(); 
        }

    }

    void Move()
    {
        transform.position = Vector3.MoveTowards(agent.transform.position, destination.transform.position, speed * Time.deltaTime);
    }
	
}
