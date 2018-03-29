using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class SpiderMobs : MobStats
{

    NavMeshAgent agent;
    Rigidbody body;
    [SerializeField]
    Transform head;

    protected override void Start()
    {
        base.Start();
        agent = GetComponent<NavMeshAgent>();
        body = GetComponent<Rigidbody>();
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        playerDistance = GetPlayerDistance(transform);
        if (body.velocity != Vector3.zero)
        {
            body.velocity = Vector3.zero;
        }
        if (destination != null)
        {
            Move();
        }
        
        if (playerDistance < aggroRange && timeLeft < 0)
        {
            Shoot(); 
        }
        else
        {
            LookAtPlayer(head);
        }

    }

    void Move()
    {
        transform.position = Vector3.MoveTowards(agent.transform.position, destination.transform.position, speed * Time.deltaTime);
    }
	
}
