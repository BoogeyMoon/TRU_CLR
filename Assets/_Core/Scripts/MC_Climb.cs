using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_Climb : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerStay(Collider coll)
    {
        if (coll.gameObject.tag == "Edge")
        {
            Climb_Edge edge = coll.gameObject.GetComponent<Climb_Edge>();
            edge.Climb(transform);
        }
    }

}
