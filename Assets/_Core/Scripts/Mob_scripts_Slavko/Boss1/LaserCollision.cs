using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Av Andreas de Freitas och Timmy Alvelöv
//Gör så att bossens laserstråle kan skada spelaren
public class LaserCollision : MonoBehaviour
{
    float timer, nextActionTime = 0.0f;
    [SerializeField]
    float timeBetweenTicks, damage;

    void OnTriggerStay(Collider coll)
    {
        if (timer < 0)
        {
            if (coll.transform.tag == "Player")
            {
                timer = timeBetweenTicks;
                nextActionTime += timeBetweenTicks; //Så spelaren endast tar skada vid varje x sekund hen befinner sig i laserstrålen
                coll.GetComponent<PlayerStats>().ChangeHealth(-damage); //Gör skada på spelaren
            }
        }
    }

    void Update() //Hanterar nedräkning
    {
        timer -= Time.deltaTime;
    }
}
