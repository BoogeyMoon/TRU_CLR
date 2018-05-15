using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// by Slavko Stojnic
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
    int mobsSpawned;




    // Use this for initialization
    void Start()
    {
        getPlayer = GameObject.FindGameObjectWithTag("Player").transform;
        if (spawnPoint == null)
        {
            spawnPoint = transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(transform.position, getPlayer.position);
        time = time + Time.deltaTime;
        if ((time >= interval) && (dist <= aggroRange))
        {
            int r = Random.Range(0, spawnedEnemies.Length);
            GameObject mob = Instantiate(spawnedEnemies[r], spawnPoint.position, spawnedEnemies[r].transform.rotation);
            FlyingMob mobScript = mob.GetComponent<FlyingMob>();
            mobsSpawned++;
            mobScript.PatrolPoints = patrolpoints;
            if (mobScript.ScoreValue - ((mobsSpawned - 1) * 10) > 0)
                mobScript.ScoreValue -= (mobsSpawned - 1) * 10;
            else
                mobScript.ScoreValue = 1;
            time = 0;
        }
    }
    public void Upgrade() //Increases the spawners spawnrate.
    {
        interval = interval - interval / 8;
    }
}
