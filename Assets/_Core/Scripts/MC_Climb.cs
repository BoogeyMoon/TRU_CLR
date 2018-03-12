using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Av Timmy Alvelöv

//Ser till att spelaren kan klättra över kanter (ANVÄNDS EJ I SPELET)
public class MC_Climb : MonoBehaviour
{

    void OnTriggerStay(Collider coll)
    {
        if (coll.gameObject.tag == "Edge")
        {
            Climb_Edge edge = coll.gameObject.GetComponent<Climb_Edge>();
            edge.Climb(transform);
        }
    }

}
