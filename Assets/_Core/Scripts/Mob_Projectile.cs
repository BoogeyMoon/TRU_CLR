using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Timmy Alvelöv
public class Mob_Projectile : MonoBehaviour
{

    protected GameObject rotation, player;
    [SerializeField]
    protected float startVelocity, damage, startTime, lifeTime;
    [SerializeField]
    protected int color;

    protected void Start()
    {
        player = GameObject.Find("SK_DemoDude_PF");

        lifeTime = 10;
    }
    protected void Update()
    {
        startTime += Time.deltaTime;
        if (startTime >= lifeTime)
        {
            Destroy(gameObject);
        }
    }


}
