using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorIndicatior : MonoBehaviour
{
    float timer, rotateSpeed;

    public void SwitchColor(bool clockwise)
    {
        if(clockwise)
        {
            transform.Rotate(new Vector3 (0,0,-120));
        }
        else
        {
            transform.Rotate(new Vector3(0, 0, 120));
        }
    }
	
}
