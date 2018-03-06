using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDownOnDamage : MonoBehaviour {


    private Rigidbody [] bodies;
    private Collider[] colliders;
    private Collider mainCollider;
    private Rigidbody iFall;
    private Transform[] unparent;
    void Start()
        {
        bodies = GetComponentsInChildren<Rigidbody>();
        colliders = GetComponentsInChildren<Collider>();
        mainCollider = gameObject.GetComponent<Collider>();
        unparent = GetComponentsInChildren<Transform>();
        }
    void OnTriggerEnter(Collider other)
        {
        mainCollider.enabled = false;
        Destroy(gameObject, .01f);

        foreach (Transform unpa in unparent)
        {
            unpa.transform.SetParent(null, true);
        }
        foreach (Collider coll in colliders)
        {
            coll.enabled = true;
        }
        foreach (Rigidbody body in bodies)
        {
            //iFall = GetComponent<Rigidbody>();
            body.useGravity = true;
            body.constraints = RigidbodyConstraints.FreezePositionZ;
            Destroy(body.gameObject, 3f);
        }
        

    }
    }

