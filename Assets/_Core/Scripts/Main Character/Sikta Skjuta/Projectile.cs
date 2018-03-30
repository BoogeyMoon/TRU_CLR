using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Av Timmy Alvelöv

//Ett script som beskriver beteendet som alla projektiler har gemensamt, alla projektiler ärver från detta script.
public class Projectile : MonoBehaviour
{
    protected GameObject rotation;
    [SerializeField]
    protected float startVelocity, damage, startTime, lifeTime;
    [SerializeField]
    protected int color;

    protected void Start() // Hittar emptyn för att veta var vi siktar, samt sätter rotationen korrekt.
    {
        rotation = GameObject.Find("ShoulderAim");
        transform.rotation = rotation.transform.rotation;
        lifeTime = 10;
    }
    protected void Update() //Förstör kulan om den missar kolliders.
    {
        startTime += Time.deltaTime;
        if (startTime >= lifeTime)
        {
            Destroy(gameObject);
        }
    }

    protected virtual void OnTriggerEnter(Collider coll)
    {
        if(coll.transform.gameObject.tag == "Interactable")
        {
            coll.GetComponent<SwitchInteract>().Trigger(color);
            Destroy(gameObject);
        }
    }
}
