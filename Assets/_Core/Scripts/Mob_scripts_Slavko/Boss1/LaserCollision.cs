using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Av Andreas de Freitas och Timmy Alvelöv
//Gör så att bossens laserstråle kan skada spelaren
public class LaserCollision : MonoBehaviour
{
    float timer;
    [SerializeField]
    float timeBetweenTicks;

    void OnTriggerStay(Collider coll)
    {
        if (timer < 0)
        {
            if (coll.transform.tag == "Player")
            {
                timer = timeBetweenTicks;
                coll.GetComponent<PlayerStats>().ChangeHealth(-1);

            }
        }
    }

    void Update()
    {
        timer -= Time.deltaTime;
    }
}
