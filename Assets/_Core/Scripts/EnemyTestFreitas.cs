using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTestFreitas : MonoBehaviour
{
    [SerializeField]
    Transform player;

    [SerializeField]
    GameObject enemyBullet, enemyGun;

    [SerializeField]
    Transform[] points;

    int destPoint;

    bool chasingPlayer;

    [SerializeField]
    float speed;

    NavMeshAgent agent; 

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        chasingPlayer = false;
        destPoint = 0;
    }

    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f && !chasingPlayer)
        {
            GotoNextPoint();
        }

        if (chasingPlayer)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, player.position, step);
        }
    }

    void GotoNextPoint()
    {
        if (points.Length == 0)
        {
            return;
        }

        agent.destination = points[destPoint].position;
        destPoint = (destPoint + 1) % points.Length;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            print("NU SKARU DÖ");
            chasingPlayer = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        print("Du hann undan lilla skit");
        chasingPlayer = false;
    }
}
