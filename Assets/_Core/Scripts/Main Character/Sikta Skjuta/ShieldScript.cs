using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : MonoBehaviour
{

    void Update()
    {
        transform.Translate(Vector3.forward * 2 * Time.deltaTime);
    }

}
