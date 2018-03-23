using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedTheMC : MonoBehaviour {

    private Transform wheresMC;
    private float dist;

	// Use this for initialization
	void Start () {
        wheresMC = GameObject.FindGameObjectWithTag("ShootHere").transform;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.MoveTowards(transform.position, wheresMC.transform.position, 5*Time.deltaTime);
        if (Vector3.Distance(wheresMC.position, transform.position) <=.1f)
        {
            Destroy(gameObject, 1);
        }

	}


}
