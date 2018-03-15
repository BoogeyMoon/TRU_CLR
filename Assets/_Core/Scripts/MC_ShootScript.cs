using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Av Timmy Alvelöv

//Används för att skapa projektilerna som MC skjuter.
public class MC_ShootScript : MonoBehaviour
{
    [SerializeField]
    GameObject[] colorsBullets;
    [SerializeField]
    GameObject rifleBarrel, shoulderAim;
    GameObject currentBullet;


    enum ColorProjectiles { Blue, Yellow, Red };
    int activeColor;

    [SerializeField]
    float cooldown, laserLength, fireRate, offsetZ;

    LineRenderer laserLineRenderer;
    Vector3 targetPosition, direction;

    void Start()
    {
        offsetZ = -0.85f;
        laserLength = 50f;
        laserLineRenderer = GetComponent<LineRenderer>();
       // fireRate = 0f;
       // cooldown = 0.5f;
        activeColor = (int)ColorProjectiles.Blue;
    }

    void Update()
    {
        shoulderAim.transform.position = new Vector3(shoulderAim.transform.position.x, shoulderAim.transform.position.y, offsetZ);
        if (fireRate > 0)
        {
            fireRate -= Time.deltaTime;
        }
        if (Input.GetMouseButton(0))
        {
            if (fireRate <= 0)
            {
                if(activeColor == (int)ColorProjectiles.Red)
                {
                    targetPosition = rifleBarrel.transform.position;
                    direction = rifleBarrel.transform.forward;
                }
                Shoot();
            }
            else
            {
                laserLineRenderer.enabled = false;
            }
        }
        else
        {
            laserLineRenderer.enabled = false;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            activeColor = (activeColor + 1) % 3;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            activeColor--;
            if (activeColor < 0)
            {
                activeColor = colorsBullets.Length - 1;
            }
        }
    }
    void Shoot()
    {
        if (activeColor == (int)ColorProjectiles.Red)
        {
            laserLineRenderer.enabled = true;
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
        currentBullet = Instantiate(colorsBullets[activeColor],
        new Vector3(rifleBarrel.transform.position.x, rifleBarrel.transform.position.y, rifleBarrel.transform.position.z), Quaternion.identity);
        fireRate = cooldown;
    }
}
