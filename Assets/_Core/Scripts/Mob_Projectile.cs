using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Av Timmy Alvelöv

//En klass som alla skott som fiender skjuter ärver från och har gemensamt
//Scriptet har hand om var spelaren är och ser till att kulan förstörs om den missar, skada och hastighet.
public class Mob_Projectile : MonoBehaviour
{

    protected GameObject rotation, player;
    [SerializeField]
    protected float startVelocity, damage, startTime, lifeTime;
    [SerializeField]
    protected int color;

    protected void Start()
    {
        player = GameObject.Find("SK_MainCharacter_PF");

        lifeTime = 10;
    }
    protected void Update()
    {
        startTime += Time.deltaTime;
        if (startTime >= lifeTime) //Förstör kulan efter en angiven tid
        {
            Destroy(gameObject);
        }
    }


}
