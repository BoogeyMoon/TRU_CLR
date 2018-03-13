using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempProjectile : MonoBehaviour
{
    [SerializeField]
    LineRenderer laserLineRenderer;
    [SerializeField]
    float laserLength = 50f;
    Vector3 targetPosition;
    Vector3 direction;
    MC_ShootScript player;


     void Start()
    {
        Vector3[] initLaserPositions = new Vector3[2] { Vector3.zero, Vector3.zero };
        laserLineRenderer.SetPositions(initLaserPositions);
        player = GameObject.Find("SK_DemoDude_PF").gameObject.GetComponent<MC_ShootScript>();
        
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if(player.ActiveColor == 2)
            {
                laserLineRenderer.enabled = true;
                targetPosition = transform.position;
                direction = transform.forward;
                ShootLaser();
            }

        }
        else
        {
            laserLineRenderer.enabled = false;
        }
    }

    void ShootLaser()
    {
        Ray ray = new Ray(targetPosition, direction);
        RaycastHit raycastHit;
        Vector3 endPosition = targetPosition + (laserLength * direction);

        if (Physics.Raycast(ray, out raycastHit, laserLength))
        {
            endPosition = raycastHit.point;
        }

        laserLineRenderer.SetPosition(0, targetPosition);
        laserLineRenderer.SetPosition(1, endPosition);
    }
}
