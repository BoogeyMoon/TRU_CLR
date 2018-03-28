using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Av Andreas de Freitas och Timmy Alvelöv
//Gör så att bossens laserstråle kan skada spelaren
public class LaserCollision : MonoBehaviour
{
    float timer, nextActionTime = 0.0f;
    [SerializeField]
    float timeBetweenTicks;

    void OnTriggerStay(Collider coll)
    {
        if (timer < 0)
        {
            if (coll.transform.tag == "Player")
            {
                timer = timeBetweenTicks;

                if (Time.time > nextActionTime) //Så spelaren endast tar skada vid varje x sekund hen befinner sig i laserstrålen
                {
                    nextActionTime += timeBetweenTicks;
                    coll.GetComponent<PlayerStats>().ChangeHealth(-1);
                }
            }
        }
    }

    void Update() //Hanterar nedräkning
    {
        timer -= Time.deltaTime;
    }
}
