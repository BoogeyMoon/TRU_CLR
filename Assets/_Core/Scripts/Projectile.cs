using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Av Timmy Alvelöv
public class Projectile : MonoBehaviour
{
    protected GameObject rotation;
    protected float startVelocity, damage;

    protected void Start()
    {
        rotation = GameObject.Find("ShoulderAim");
        transform.rotation = rotation.transform.rotation;
    }
    void Update()
    {

    }
}
