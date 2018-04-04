using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Av Timmy Alvelöv



public class Door : MonoBehaviour, Interactable
{
    [SerializeField]
    float speed;
    bool openDoor, closeDoor = true, activated = false;
    float height = 7.4f, startY; 
    void Start()
    {
        startY = transform.position.y;
    }
    public void Activated() //Öppnar en dörr
    {
        if (!activated)
            activated = true;
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
