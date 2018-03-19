using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    [SerializeField]
    GameObject spawnThis;
    [SerializeField]
    float interval;
    float time;
    [SerializeField]
    Transform spawnPoint;

	// Use this for initialization
	void Start () {
        
		
	}
	
	// Update is called once per frame
	void Update () {
        time = time + Time.deltaTime;
        if (time >= interval)
        {
            Instantiate(spawnThis, spawnPoint.position, spawnThis.transform.rotation);
            time = 0;
        }
		
	}
}
