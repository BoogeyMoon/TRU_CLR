using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// by Slavko Stojnic
// makes the flying mobs move about instead of standing still 
public class SlerpAbout : MonoBehaviour
{

    [SerializeField]
    float howOften, howLong, howMuch, howFast;
    float timeToSlerp;
    float timeToStop;
    Vector3 randomVector;
    bool startLerping;

    void Start()
    {
        startLerping = false;
    }

    void Update()
    {
        if (!startLerping) // wait until moving again
        {
            timeToSlerp += Time.deltaTime;
        }

        if (timeToSlerp >= howOften) // set the random position to move towards
        {
            randomVector = (transform.position + new Vector3(Random.Range(-howMuch, howMuch), Random.Range(-howMuch, howMuch), 0));
            timeToSlerp = 0;
            startLerping = true;
        }
        if (startLerping) // move towards the position at a fairly slow pace
        {
            transform.position = Vector3.Slerp(transform.position, randomVector, Time.deltaTime * howFast);
            timeToStop += Time.deltaTime;
            if (timeToStop >= howLong)
            {
                timeToStop = 0;
                startLerping = false;
            }
        }
    }
}
