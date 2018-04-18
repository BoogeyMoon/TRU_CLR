using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//by Slavko Stojnic

public class RotateWithPause : MonoBehaviour {

    [SerializeField]
    float timeBetweenRotations;
    float timeSinceStart;
    [SerializeField]
    float rotationAngle;
    float desiredAngle;
    bool rotationHasBegun;
    Quaternion rotateTo;

	// Use this for initialization
	void Start () {
        rotationHasBegun = false;

    }
	
	// Update is called once per frame
	void Update () {
        timeSinceStart = timeSinceStart + Time.deltaTime;
        if (timeSinceStart >= timeBetweenRotations)
        {
            if (!rotationHasBegun)
            {
                desiredAngle = transform.rotation.eulerAngles.z + rotationAngle;

                if (desiredAngle >= 360)
                {
                    desiredAngle = 360 - desiredAngle;
                }
                rotateTo = Quaternion.Euler(0, 0, desiredAngle);
            }

            rotationHasBegun = true;

            transform.rotation = Quaternion.Slerp(transform.rotation, rotateTo, Time.deltaTime * 3);
            if (!((transform.rotation.eulerAngles.z <= desiredAngle - .1) || (transform.rotation.eulerAngles.z > desiredAngle*3)))
            {
                transform.rotation = Quaternion.Euler(0,0,desiredAngle);
                timeSinceStart = -.1f ;
                rotationHasBegun = false;
            }
        }

    }
}
