﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climb_Edge : MonoBehaviour
{
    Transform start;
    Transform destination;
    // Use this for initialization
    void Start()
    {
        start = transform.GetChild(0);
        destination = transform.GetChild(1);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Climb(Transform player)
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            StartCoroutine(ClimbTheEdge(player));
        }
    }
    IEnumerator ClimbTheEdge(Transform player)
    {
        print("Climbing");
        player.position = start.position;
        yield return new WaitForSeconds(0.5f);
        print("Done");
        player.position = destination.position;
    }
    
}
