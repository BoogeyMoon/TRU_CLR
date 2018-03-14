using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Av Timmy Alvelöv 

//Håller koll på vilken som är nästa vägpoäng
public class Waypoint : MonoBehaviour
{
    [SerializeField]
    GameObject nextWaypoint = null;

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Weakpoint")
        {
            coll.gameObject.GetComponent<MobStats>().ChangeDestination(nextWaypoint, gameObject);
        }
    }
}
