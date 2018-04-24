using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// by Slavko Stojnic and Timmy Alvelöv
// Spawns flying enemies at a desired rate 
public class Spawner : MonoBehaviour
{
    [SerializeField]
    GameObject[] spawnedEnemies;
    [SerializeField]
    GameObject patrolpoints;

    [SerializeField]
    float interval;
    float time;
    [SerializeField]
    Transform spawnPoint;
    float dist;
    Transform getPlayer;
    [SerializeField]
    float aggroRange;       


    // Use this for initialization
    void Start()
    {
        getPlayer = GameObject.FindGameObjectWithTag("Player").transform;
        if(spawnPoint == null)
        {
            spawnPoint = transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(transform.position, getPlayer.position);
        time = time + Time.deltaTime;
        if ((time >= interval) && (dist <= aggroRange)) // start spawning in player is in range and it's time to spawn again
        {
            int r = Random.Range(0,spawnedEnemies.Length);
            GameObject mob = Instantiate(spawnedEnemies[r], spawnPoint.position, spawnedEnemies[r].transform.rotation);
            mob.GetComponent<FlyingMob>().PatrolPoints = patrolpoints; // give patrol points to each spawned (Flying) mob 
            time = 0;
        }
    }
    public void Upgrade() // Boss add-on, increasing the spawn rate
    {
        interval = interval - interval / 3;
    }
}
