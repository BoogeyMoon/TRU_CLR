using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Av Timmy Alvelöv

//Får en dörr att öppna sig från om den blir aktiverad från en annan källa
public class Door : MonoBehaviour, Interactable
{
    [SerializeField]
    AudioClip[] doorSound;
    SoundManager soundManager;

    [SerializeField]
    float speed;
    bool openDoor, closeDoor = true, activated = false;
    float height = 7.4f, startY; 
    void Start()
    {
        startY = transform.position.y;
        soundManager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SoundManager>();
    }
    public void Activated() //Öppnar en dörr
    {
        if (!activated)
            activated = true;
        soundManager.RandomizeSfx(doorSound, 4,false);
        openDoor = !openDoor;
        closeDoor = !closeDoor;

    }
    void Update()
    {
        if(activated)
        {
            if(openDoor && transform.position.y < startY + height)
            {
                transform.Translate(Vector3.up * speed * Time.deltaTime);
            }
            else if(closeDoor && transform.position.y > startY)
            {
                transform.Translate(Vector3.up * -speed * Time.deltaTime);
            }
        }
    }
	
}
