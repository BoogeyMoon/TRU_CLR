using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Av Timmy Alvelöv

//Ger stöd för att öppna en dörr
interface Interactable
{
    void Activated();
}

public class Door : MonoBehaviour, Interactable
{
    public void Activated() //Öppnar en dörr
    {
        //Code goes here
        print("Door is opening");
    }
	
}
