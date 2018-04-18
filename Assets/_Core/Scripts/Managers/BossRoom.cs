using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Av Timmy Alvelöv

//Ser till att kameran låser sig till en punkt när spelaren kommer fram till triggern
public class BossRoom : MonoBehaviour
{
    [SerializeField]
    Transform triggers;
    Transform cameraPosition;
    CameraManager jig;
    
    void Start() //hämtar komponenter
    {
        cameraPosition = transform.GetChild(0);
        jig = GameObject.FindGameObjectWithTag("Camera").GetComponent<CameraManager>();
    }
    void OnTriggerEnter(Collider coll) //Byter till "bossmode"
    {
        if (coll.tag == "Player")
        {
            jig.SetCameraPosition(cameraPosition);
        }
    }
    void OnTriggerExit(Collider coll) //Återställer kameran så att den följer spelaren
    {
        if(coll.tag == "Player")
        {
            jig.SetCameraPosition(null);
        }
    }
	
}
