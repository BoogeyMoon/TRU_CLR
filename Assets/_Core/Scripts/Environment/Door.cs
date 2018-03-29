using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Av Timmy Alvelöv



public class Door : MonoBehaviour, Interactable
{
    bool boolen = true;
    public void Activated() //Öppnar en dörr
    {
        boolen = !boolen;
        gameObject.SetActive(boolen);
        //Code goes here
        print("Door is opening");
    }
	
}
