using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// by Slavko Stojnic

public class FallDownOnDamage : MonoBehaviour {

    private Rigidbody [] bodies;
    private Collider[] colliders;
    private Collider mainCollider;
    private Rigidbody iFall;
    private Transform[] unparent;
    private HingeJoint[] unhinge;
    [SerializeField] float destroyAfter;
    [SerializeField] float randomScatter;
    void Start()
        {
        unhinge = GetComponentsInChildren<HingeJoint>();
        bodies = GetComponentsInChildren<Rigidbody>();
        colliders = GetComponentsInChildren<Collider>();
        /*foreach (Collider coll in colliders)
        {
            coll.enabled = false;
        }*/
        mainCollider = gameObject.GetComponent<Collider>();
        unparent = GetComponentsInChildren<Transform>();
        }
    void OnTriggerEnter(Collider other)
        {
        mainCollider.enabled = false;
        Destroy(gameObject, .01f);

        /*foreach (HingeJoint hing in unhinge)
        {
            Destroy(hing);
        }*/
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
            Destroy(body.gameObject, destroyAfter);
            body.AddForce(body.transform.forward * Random.Range(-randomScatter, randomScatter));
        }
        

    }
    }

