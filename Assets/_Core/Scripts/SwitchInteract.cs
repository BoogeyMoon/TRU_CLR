using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Av Timmy Alvelöv

//Aktiverar en metod till ett gameobject som aktiveras via det här scriptet. (Luddig beskrivning, jag vet)
public class SwitchInteract : MonoBehaviour {
    [SerializeField]
    int color;
    [SerializeField]
    GameObject Object;


    public void Trigger(int color) //Om switchen träffas av rätt färg
    {
        if(this.color == color)
        {
            Object.GetComponent<Interactable>().Activated();
        }
    }
}
