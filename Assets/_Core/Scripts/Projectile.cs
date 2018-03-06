using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Av Timmy Alvelöv

//Ett script som beskriver beteendet som alla projektiler har gemensamt, alla projektiler ärver från detta script.
public class Projectile : MonoBehaviour
{
    protected GameObject rotation;
    [SerializeField]
    protected float startVelocity, damage, startTime, lifeTime;
    [SerializeField]
    protected int color;

    protected void Start()
    {
        rotation = GameObject.Find("ShoulderAim");
        transform.rotation = rotation.transform.rotation;
        lifeTime = 10;
    }
    protected void Update()
    {
        startTime += Time.deltaTime;
        if(startTime >= lifeTime)
        {
            Destroy(gameObject);
        }
    }
}
