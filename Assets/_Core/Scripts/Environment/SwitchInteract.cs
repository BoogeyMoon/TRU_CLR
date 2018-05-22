using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Av Timmy Alvelöv

//Ger stöd för att saker att interageras med
public interface Interactable
{
    void Activated();
}

//Aktiverar en metod till ett gameobject som aktiveras via det här scriptet. (Luddig beskrivning, jag vet)
public class SwitchInteract : MonoBehaviour {
    [SerializeField]
    int color;
    [SerializeField]
    GameObject Object;
    bool activated;
    Collider coll;


    void Start()
    {
        coll = GetComponent<Collider>();
    }
    public void Trigger(int color) //Om switchen träffas av rätt färg
    {
        if(this.color == color && !activated)
        {
            coll.enabled = false;
            activated = true;
            Object.GetComponent<Interactable>().Activated();
        }
    }
}
