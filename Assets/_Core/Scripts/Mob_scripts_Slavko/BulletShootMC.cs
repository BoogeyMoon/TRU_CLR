using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShootMC : MonoBehaviour {
    public Transform target;
    private Rigidbody rb;
    public float bulletSpeed;

    // by Slavko Stojnic
    void Start () {
        target = GameObject.Find("SK_DemoDude_PF").transform; // not sure why the script doesn't work if I don't add this 
        transform.LookAt(new Vector3(target.position.x, target.position.y + 1.5f, target.position.z));
        rb = GetComponent<Rigidbody>();
        
    }
	
	// Update is called once per frame
	void Update () {
        rb.AddForce(transform.forward * bulletSpeed);
    }
    private void OnCollisionEnter(Collision collision)
    {
        bulletSpeed = 0;
        Destroy(gameObject,3f);
    }
}
