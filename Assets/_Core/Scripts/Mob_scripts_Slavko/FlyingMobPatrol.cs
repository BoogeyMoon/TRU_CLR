using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// by Slavko Stojnic

public class FlyingMobPatrol : MonoBehaviour {

    [SerializeField] Transform[] points;
    private Transform destination;
    private int destPoint = 0;
    [SerializeField] float speed;

    /*[SerializeField] Transform target;
    [SerializeField] Transform bullet;
    
    float dist;
    private float time;
    private float howOftenToShoot;
    [SerializeField] Transform bulletSpawnPoint;
    private int bulletCount;*/

    void Start () {

        GotoNextPoint();

        /*howOftenToShoot = 0.15f;
        bulletCount = 0;
        time = 0.0f;*/
    }

    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (points.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
       destination = points[destPoint];

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;
    }

    void Update () {
        // Choose the next destination point when the agent gets
        // close to the current one.
        if ((Vector3.Distance(destination.position, transform.position) <= .1))
        {
            GotoNextPoint();
        }

            
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, destination.position, step);
        /*time += Time.deltaTime; 

        dist = Vector3.Distance(target.position, transform.position);
        if (dist > 5 && dist < 40) // follow MC if he's between 5 and 40 distance
        {
            transform.LookAt(target.position); // turn towards MC
            Vector2 randomVector = new Vector2(Random.Range(-1.5f, 1.5f), Random.Range(-1.5f, 1.5f));
            transform.Translate(randomVector * Time.deltaTime * 5); // jiggle about randomly because it's shitty if the mob stands still
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        }
        if ((transform.position.y-target.position.y) < 5) 
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
        }

        if (time >= howOftenToShoot && dist < 40) // stop shooting if MC is far away
        {
            if (bulletCount == 0) // shoot five times with .15 second pause (Phase 1)
            { howOftenToShoot = 0.15f; } 
            time = 0.0f;
            
            Instantiate(bullet, bulletSpawnPoint.position, bullet.rotation);
            bulletCount++;

            if (bulletCount == 5)
            {
                howOftenToShoot = 2f; // pause for 2 seconds (Phase 2)
                bulletCount = 0;
            }
        }*/
    }
}
