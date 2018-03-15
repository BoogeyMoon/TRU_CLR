using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour {

    [SerializeField]
    Transform[] points;
    private Transform destinationLocal;
    private int destPoint = 0;
    [SerializeField]
    float speed;
    private float time;

    void Start()
    {
        GotoNextPoint();
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
        // Choose the next destination point when the agent gets
        // close to the current one.
        float step = speed * Time.deltaTime;
        time += Time.deltaTime;

        if (Vector3.Distance(destinationLocal.position, transform.position) <= .1)
        {
            GotoNextPoint();
        }
        transform.position = Vector3.MoveTowards(transform.position, destinationLocal.position, step);
    }

}

