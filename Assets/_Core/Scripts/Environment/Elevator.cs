using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// by Slavko Stojnic
// Handles the behaviour of moving platforms
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
    [SerializeField]
    bool bossFightElevator;

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
        if (bossFightElevator == false)
        {
            float step = speed * Time.deltaTime;

            if (parenting) // If parenting, calculate distance
            {
                dist = Vector3.Distance(getPlayer.position, transform.position);
            }

            if ((dist > 6) & parenting) // Check if need to unparent MC (if they moved away from the platform)
            {
                getPlayer.parent = null;
                parenting = false;
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
        else return;
        
    }

    private void OnTriggerStay(Collider other) // If the player steps on the moving platform, make them move along with it (by parenting)
    {
        if ((other.transform.tag == "Player") & !parenting)
        {
            bossFightElevator = false;
            other.transform.parent = this.transform;
            parenting = true;
        }
    }
    public void OnStay(Collider other)
    {
        OnTriggerStay(other);
    }
}

