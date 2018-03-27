using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// by Slavko Stojnic
public class Elevator : MonoBehaviour {

    [SerializeField]
    Transform[] points;
    private Transform destinationLocal;
    private int destPoint = 0;
    [SerializeField]
    float speed;
    private float time;
    private float unparentPause;
    private bool parenting;
    private Transform getPlayer;
    float dist; 

    void Start()
    {
        GotoNextPoint();
        getPlayer = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (points.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        destinationLocal = points[destPoint];

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;
    }

    void Update()
    {

        float step = speed * Time.deltaTime;

        if (parenting) // if parenting, calculate distance
        {
            //unparentPause = unparentPause + Time.deltaTime; // cooldown for parenting, probably unneessary
            dist = Vector3.Distance(getPlayer.position, transform.position);
            //print(dist);
        }

        if ((dist > 6) & parenting) // check if need to unparent MC
        {
            getPlayer.parent = null;
            parenting = false;
            print("Unparented!");
            dist = 0;
        }
        time += Time.deltaTime;



        if ((destinationLocal != null) && (Vector3.Distance(destinationLocal.position, transform.position) <= .1)) // reached one patrol point? go to next one.
         {
            GotoNextPoint();
        }
        if (destinationLocal != null) // travel towards the next patrol point at a designated speed 
        {
            transform.position = Vector3.MoveTowards(transform.position, destinationLocal.position, step);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if ((other.transform.tag == "Player") & !parenting)
        {
            other.transform.parent = this.transform;
            parenting = true;
            print("Parented!");
        }
    }
    /*private void OnTriggerExit(Collider other) // older unparenting solution, probably unnecessary 
    {
        if ((other.transform.tag == "Player") & (unparentPause > 0.1f) & parenting)
        {
            other.transform.parent = null;
            unparentPause = 0;
            print("Unparented!");
            parenting = false;
        }
    }*/
    

    

}

