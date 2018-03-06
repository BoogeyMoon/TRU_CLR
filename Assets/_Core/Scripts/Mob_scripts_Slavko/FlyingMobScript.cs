using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// by Slavko Stojnic

public class FlyingMobScript : MonoBehaviour {
    [SerializeField] Transform target;
    [SerializeField] Transform bullet;
    [SerializeField] float speed;
    [SerializeField] float radiusOfReaction;
    float dist;
    private float time;
    private float howOftenToShoot;
    [SerializeField] Transform bulletSpawnPoint;
    private int bulletCount;

    void Start () {
        howOftenToShoot = 0.15f;
        bulletCount = 0;
        time = 0.0f;
    }
	
	void Update () {

        time += Time.deltaTime; 

        dist = Vector3.Distance(target.position, transform.position);
        if (dist > 5 && dist < radiusOfReaction) // follow MC if he's between 5 and the distance determined in the inspector
        {
            transform.LookAt(target.position); // turn towards MC
            Vector2 randomVector = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            transform.Translate(randomVector * Time.deltaTime * 5, Space.World); // jiggle about randomly because it's shitty if the mob stands still
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);


        }

        if ((dist < radiusOfReaction) && (transform.position.y - target.position.y) < 5)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
        }

        if (time >= howOftenToShoot && dist < radiusOfReaction) // stop shooting if MC is far away
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
        }
    }
}
