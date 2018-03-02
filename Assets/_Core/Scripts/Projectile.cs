using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Av Timmy Alvelöv
public class Projectile : MonoBehaviour
{
    protected GameObject rotation;
    protected float startVelocity, damage, startTime, lifeTime;

    protected void Start()
    {
        print("Varför fuckar allt?");
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
