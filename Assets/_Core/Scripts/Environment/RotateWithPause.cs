using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//by Slavko Stojnic
//Used for rotating platforms - one can regulate the frequency and angle of rotations
public class RotateWithPause : MonoBehaviour {

    [SerializeField]
    float timeBetweenRotations;
    float timeSinceStart;
    [SerializeField]
    float rotationAngle;
    float desiredAngle;
    bool rotationHasBegun;
    Quaternion rotateTo;

	void Start () {
        rotationHasBegun = false;
    }
	
	void Update () {
        timeSinceStart = timeSinceStart + Time.deltaTime; 
        if (timeSinceStart >= timeBetweenRotations)
        {
            if (!rotationHasBegun) // sets the next rotation angle around Z axis
            {
                desiredAngle = transform.rotation.eulerAngles.z + rotationAngle; 

                if (desiredAngle >= 360)
                {
                    desiredAngle = 360 - desiredAngle;
                }
                rotateTo = Quaternion.Euler(0, 0, desiredAngle);
            }
            rotationHasBegun = true;

            transform.rotation = Quaternion.Slerp(transform.rotation, rotateTo, Time.deltaTime * 3); // performs a smooth, quick rotation
            if (!((transform.rotation.eulerAngles.z <= desiredAngle - .1) || (transform.rotation.eulerAngles.z > desiredAngle*3)))
            {
                transform.rotation = Quaternion.Euler(0,0,desiredAngle); // make sure that the rotation snaps to the desired angle at the end instead of an imprecise one
                timeSinceStart = -.1f ;
                rotationHasBegun = false;
            }
        }
    }
}
