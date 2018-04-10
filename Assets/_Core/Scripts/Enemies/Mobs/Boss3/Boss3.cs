using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Av Timmy Alvelöv

//Beskriver Bossens rörelsemönster samt när den dör
public class Boss3 : MobStats
{
    [SerializeField]
    float timeBetweenRotation;
    float rotTimer, rotDoneTimer, rotateAngle = 120;
    WinScript win;


    protected override void Start()
    {
        base.Start();
        SetToPlayerPlane(transform);
        if(timeBetweenRotation <= 0)
        {
            timeBetweenRotation = 4;
        }
        rotTimer = timeBetweenRotation;
        rotDoneTimer = timeBetweenRotation + 0.7f;
        win = GameObject.FindGameObjectWithTag("Win").GetComponent<WinScript>();
    }

    void Update()
    {
        if (!dead)
        {
            rotTimer -= Time.deltaTime;
            rotDoneTimer -= Time.deltaTime;
            Patrol();

            if (transform.childCount == 0)
            {
                dead = true;
                win.WinConFinished(transform);
                Die();
            }
            else if (rotTimer <= 0)
            {
               Rotate();
            }
            if(rotDoneTimer <= 0)
            {
                rotDoneTimer = timeBetweenRotation + 0.7f;
                rotTimer = timeBetweenRotation;
                rotateAngle = (rotateAngle + 120) % 360;
            }
        }

    }
    void Rotate()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(transform.rotation.eulerAngles.x,transform.rotation.eulerAngles.y,rotateAngle), 5 *Time.deltaTime);
    }
}
