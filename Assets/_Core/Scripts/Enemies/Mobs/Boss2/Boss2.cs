using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Av Timmy Alvelöv

//Håller koll på boss 2's beteende
public class Boss2 : MobStats
{

    Rigidbody body;
    int childs, childsLastFrame;
    Spawner spawner;
    WinScript win;

    protected override void Start()
    {
        base.Start();
        body = GetComponent<Rigidbody>();
        SetToPlayerPlane(transform);
        updatePatrolPoints();
        spawner = transform.GetChild(0).GetComponent<Spawner>();
        childs = transform.childCount;
        childsLastFrame = childs;
        win = GameObject.FindGameObjectWithTag("Win").GetComponent<WinScript>();

    }
    void Update()
    {
        if (!dead)
        {
            childs = transform.childCount;
            if (body.velocity != Vector3.zero)
            {
                body.velocity = Vector3.zero;
            }
            Patrol();
            transform.rotation = startRot;
            if (childs != childsLastFrame) //Om en mob dör så blir bossen starkare
            {
                spawner.Upgrade();
            }
            if (childs < 2) //om bossen har slut på bollar så dör den
            {
                win.WinConFinished(transform);
                Die();
            }
            childsLastFrame = childs;
        }
    }
}