using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class SpiderMobs : MobStats
{

    NavMeshAgent agent;

    protected override void Start()
    {
        base.Start();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;

        if (destination != null)
        {
            Move();
        }

        if (timeLeft < 0)
        {
            Shoot(); 
        }

    }

    void Move()
    {
        transform.position = Vector3.MoveTowards(agent.transform.position, destination.transform.position, speed * Time.deltaTime);
    }

    void Shoot()
    {
        currentBullet = Instantiate(bullet);
        onCooldown = true;

        currentBullet.transform.position = bulletSpawner.transform.position;
        currentBullet.transform.rotation = bulletSpawner.transform.rotation;

        if (onCooldown)
        {
            timeLeft = fireCooldown;
        }
    }
	
}
