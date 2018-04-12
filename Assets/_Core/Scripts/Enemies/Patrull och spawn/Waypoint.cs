using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Av Timmy Alvelöv 

//Håller koll på vilken som är nästa vägpoäng, om en sådan finns
public class Waypoint : MonoBehaviour
{
    [SerializeField]
    GameObject nextWaypoint = null;

    void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, GameObject.FindGameObjectWithTag("Player").transform.position.z);
    }
    void OnTriggerEnter(Collider coll) //Tilldelar en ny patrullpunkt till något med rätt tag
    {
        if (coll.gameObject.tag == "Weakpoint" || coll.gameObject.tag == "Boss")
        {
            coll.gameObject.GetComponent<MobStats>().ChangeDestination(nextWaypoint, gameObject);
        }
    }
}
