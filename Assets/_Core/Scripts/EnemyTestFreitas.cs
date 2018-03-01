using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTestFreitas : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    [SerializeField]
    Transform[] points;

    int destPoint;

    [SerializeField]
    float speed;

    NavMeshAgent agent;

    [SerializeField]
    Collider deathZone;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        destPoint = 0;
    }

    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GotoNextPoint();
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

    void OnCollisionEnter(Collision col)
    {
        print("hej");
        if (col.gameObject.tag == "Player")
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
        }
    }
}
