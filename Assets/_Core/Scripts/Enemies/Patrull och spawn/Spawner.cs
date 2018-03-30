using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// by Slavko Stojnic
public class Spawner : MonoBehaviour
{
    [SerializeField]
    GameObject[] spawnedEnemies;

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
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(transform.position, getPlayer.position);
        time = time + Time.deltaTime;
        if ((time >= interval) && (dist <= aggroRange))
        {
            int r = Random.Range(0,spawnedEnemies.Length);
            Instantiate(spawnedEnemies[r], spawnPoint.position, spawnedEnemies[r].transform.rotation);
            time = 0;
        }
    }
}
