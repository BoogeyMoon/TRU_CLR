using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossElevatorTrigger : MonoBehaviour {
    Elevator parent;
    void Start()
    {
        parent = transform.parent.GetComponent<Elevator>();
    }
	void OnTriggerStay(Collider coll)
    {
        parent.OnStay(coll);
    }
}
