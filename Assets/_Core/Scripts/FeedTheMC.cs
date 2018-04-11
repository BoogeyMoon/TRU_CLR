using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// by Slavko Stojnic
// Script can be used both for the "elements" that mobs leave behind them and for homing missiles

public class FeedTheMC : MonoBehaviour {

    private Transform wheresMC;
    private float dist;
    [SerializeField]
    bool doIdestroy;
    [SerializeField]
    float speed;

	// Use this for initialization
	void Start () {
        wheresMC = GameObject.FindGameObjectWithTag("ShootHere").transform;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (transform.position.z != wheresMC.position.z)
            transform.position = new Vector3(transform.position.x, transform.position.y, wheresMC.position.z);
        transform.position = Vector3.MoveTowards(transform.position, wheresMC.transform.position, speed*Time.deltaTime);
        if ((Vector3.Distance(wheresMC.position, transform.position) <=.3f) && doIdestroy)
        {
            Destroy(gameObject, 1);
        }

	}


}
