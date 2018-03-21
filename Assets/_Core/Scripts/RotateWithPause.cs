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
                //print("desired angle is" + desiredAngle);

                rotateTo = Quaternion.Euler(0, 0, desiredAngle);
            }
            rotationHasBegun = true;
            //print(transform.rotation.eulerAngles.z);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotateTo, Time.deltaTime * 3);
            if ((transform.rotation.eulerAngles.z <= desiredAngle - .1) || (transform.rotation.eulerAngles.z > desiredAngle*3))
            {
                //transform.Rotate(Vector3.forward, rotationAngle * Time.deltaTime, Space.World);
               
            }
            else
            {
                transform.rotation = Quaternion.Euler(0,0,desiredAngle);
                //print("AFTER ROTATION THE ANGLE IS" + transform.rotation.eulerAngles.z);
                timeSinceStart = -.1f ;
                rotationHasBegun = false;
            }
        }

    }
}
