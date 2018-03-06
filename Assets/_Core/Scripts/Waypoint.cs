using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour {
    [SerializeField]
	GameObject nextWaypoint;

    void OnTriggerEnter(Collider coll)
    {
        if(coll.gameObject.tag == "Weakpoint")
        {
            coll.gameObject.GetComponent<MobStats>().ChangeDestination(nextWaypoint);
        }
    }
}
