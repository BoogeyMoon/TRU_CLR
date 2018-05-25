using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Av Timmy Alvelöv

//Ser till att kameran låser sig till en punkt när spelaren kommer fram till triggern, samt har hand om att spela upp rätt låt vid rätt tillfälle
public class BossRoom : MonoBehaviour
{
    [SerializeField]
    Transform trigger;
    Transform cameraPosition;
    CameraManager jig;
    [SerializeField]
    float startTime;
    float timer;
    bool startTimer;
    AudioManager sound;
    
    void Start() //hämtar komponenter
    {
        cameraPosition = transform.GetChild(0);
        timer = startTime;
        jig = GameObject.FindGameObjectWithTag("Camera").GetComponent<CameraManager>();
        if(trigger != null && trigger.GetComponent<Interactable>() != null)
        {
            trigger.GetComponent<Interactable>().Activated();
        }
        if (startTime == 0)
            startTime = 1;
        sound = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        sound.Play("S_TRU_CLR_InGame"); //Startar in-gameljudet 
    }
    void Update()
    {
        if (startTimer)
            timer -= Time.deltaTime;
        if(startTimer && timer < 0)
        {
            trigger.GetComponent<Interactable>().Activated();
            startTimer = false;
        }
    }
    void OnTriggerEnter(Collider coll) //Byter till "bossmode"
    {
        if (coll.tag == "Player")
        {
            jig.SetCameraPosition(cameraPosition);
            sound.Stop("S_TRU_CLR_InGame");
            sound.Play("S_TRU_CLR_Boss");
            if (trigger != null && trigger.GetComponent<Interactable>() != null)
            {
                startTimer = true;
            }
        }
    }
    void OnTriggerExit(Collider coll) //Återställer kameran så att den följer spelaren
    {
        if(coll.tag == "Player")
        {
            jig.SetCameraPosition(null);
        }
        timer = startTime;
        startTimer = false;
    }
    
	
}
