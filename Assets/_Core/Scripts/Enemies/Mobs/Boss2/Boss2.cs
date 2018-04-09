using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Av Timmy Alvelöv

//Håller koll på bossens beteende
public class Boss2 : MobStats
{

    Rigidbody body;
    int childs, childsLastFrame;
    Spawner spawner;

    protected override void Start()
    {
        base.Start();
        body = GetComponent<Rigidbody>();
        SetToPlayerPlane(transform);
        updatePatrolPoints();
        spawner = transform.GetChild(0).GetComponent<Spawner>();
        childs = transform.childCount;
        childsLastFrame = childs;

    }
    void Update()
    {
        childs = transform.childCount;
        if (body.velocity != Vector3.zero)
        {
            body.velocity = Vector3.zero;
        }
        Patrol();
        transform.rotation = startRot;
        if (childs != childsLastFrame)
        {
            spawner.Upgrade();
        }
        if (childs < 2)
        {
            Die();
        }
        childsLastFrame = childs;
    }
}