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
    int startChilds;
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
        startChilds = transform.childCount;
    }

    void Update()
    {
        if (!dead)
        {
            timeLeft -= Time.deltaTime;
            burstTimer -= Time.deltaTime;
            rotTimer -= Time.deltaTime;
            rotDoneTimer -= Time.deltaTime;
            Patrol();

            if (transform.childCount == startChilds-3)
            {
                dead = true;       //Ser till att den inte fortsätter göra något medan den är i sitt dödsstadie
                win.WinConFinished(transform); //Låter winmanagern veta att det här winconditionet är slutfört
                Die();
            }
            else if (rotTimer <= 0) //Rotera
            {
               Rotate();
            }
            if(rotDoneTimer <= 0) //Nollställ värden & setup för nästa rotation
            {
                rotDoneTimer = timeBetweenRotation + 0.7f;
                rotTimer = timeBetweenRotation;
                rotateAngle = (rotateAngle + 120) % 360;
            }
            if (GetPlayerDistance(transform) < aggroRange && burstTimer < 0)
            {
                
                if (timeLeft < 0)
                {
                        Shoot();
                }
            }
        }

    }
    void Rotate() //Roterar bossen 120 grader (en tredjedels vard)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(transform.rotation.eulerAngles.x,transform.rotation.eulerAngles.y,rotateAngle), 5 *Time.deltaTime);
    }
}
