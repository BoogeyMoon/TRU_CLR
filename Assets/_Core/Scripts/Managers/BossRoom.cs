using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Av Timmy Alvelöv

//Ser till att kameran låser sig till en punkt när spelaren kommer fram till triggern
public class BossRoom : MonoBehaviour
{
    Transform CameraPosition;
    CameraManager jig;
    void Start()
    {
        CameraPosition = transform.GetChild(0);
        jig = GameObject.FindGameObjectWithTag("Camera").GetComponent<CameraManager>();
    }
    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Player")
        {
            jig.SetCameraPosition(CameraPosition);
        }
    }
    void OnTriggerExit(Collider coll)
    {
        if(coll.tag == "Player")
        {
            jig.SetCameraPosition(null);
        }
    }
	
}
