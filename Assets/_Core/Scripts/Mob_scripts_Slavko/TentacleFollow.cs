using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleFollow : MonoBehaviour {
    public Transform target;
    public float speed;
    // Use this for initialization
    void Start () {
        target = GameObject.Find("SK_MainCharacter_PF").transform;
	}

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }

}
