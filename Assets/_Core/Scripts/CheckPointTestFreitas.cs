using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointTestFreitas : MonoBehaviour
{
    [SerializeField]
    Transform smallCheckPoint, advancedCheckPoint;

    [SerializeField]
    float offsetY;

    [SerializeField]
    GameObject player;

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Checkpoint")
        {// += new Vector3(0.0f, offsetY, 0.0f);
            smallCheckPoint.position = new Vector3(smallCheckPoint.position.x, smallCheckPoint.transform.position.y + offsetY, smallCheckPoint.transform.position.z);
        }
    }
}
