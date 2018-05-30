using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Av Timmy Alvelöv

//Får en dörr att öppna sig från om den blir aktiverad från en annan källa
public class Door : MonoBehaviour, Interactable
{

    [SerializeField]
    AudioClip[] doorSound;

    [SerializeField]
    float speed, height;
    bool openDoor, closeDoor = true, activated = false;
    float startY;
    AudioManager sound;

    void Start()
    {
        startY = transform.position.y;
        if (height == 0) height = 7.4f;
        sound = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }
    public void Activated() //Öppnar en dörr
    {
        if (!activated)
            activated = true;
        openDoor = !openDoor;
        if (Time.timeSinceLevelLoad > 2 && sound != null)
            sound.Play("door open");
    }
    void Update()
    {
        if (activated)
        {
            if (openDoor && transform.position.y < startY + height)
            {
                transform.Translate(Vector3.up * speed * Time.deltaTime);
            }
            else if (!openDoor && transform.position.y > startY)
            {
                transform.Translate(Vector3.up * -speed * Time.deltaTime);
            }
        }
    }

}
