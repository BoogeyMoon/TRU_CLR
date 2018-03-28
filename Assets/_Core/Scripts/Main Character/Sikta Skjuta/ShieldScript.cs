using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : MonoBehaviour
{

    [SerializeField]
    float timeOfShield, lifeTime;

    private void Start()
    {
        lifeTime = timeOfShield;
    }

    void Update()
    {
        lifeTime -= 1 * Time.deltaTime;
        transform.Translate(Vector3.forward * 2 * Time.deltaTime);
        if (lifeTime <= 0)
            Destroy(gameObject);
    }



}
