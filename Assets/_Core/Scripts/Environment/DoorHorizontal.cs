using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Av Timmy Alvelöv

//Får en dörr att öppna sig från om den blir aktiverad från en annan källa
public class DoorHorizontal : MonoBehaviour, Interactable
{
    [SerializeField]
    float speed;
    bool openDoor, closeDoor = true, activated = false;
    float height = 7.4f, startX;
    AudioManager sound;

    void Start()
    {
        startX = transform.position.x;
        if (height == 0) height = 7.4f;
        sound = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }
    public void Activated() //Öppnar en dörr
    {
        if (!activated)
            activated = true;
        openDoor = !openDoor;
        if (Time.time > 2 && sound != null)
            sound.Play("door open");

    }
    void Update()
    {
        if (activated)
        {
            if (openDoor && transform.position.x < startX + height)
            {
                transform.Translate(Vector3.up * speed * Time.deltaTime);
            }
            else if (closeDoor && transform.position.x > startX)
            {
                transform.Translate(Vector3.up * -speed * Time.deltaTime);
            }
        }
    }

}
