using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Av Timmy Alvelöv
// Tillägg av Moa Lindgren

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
    float laserLength, fireRate, offsetZ;
    float cooldown;
    [SerializeField]
    float[] cooldowns;

    LineRenderer laserLineRenderer;
    Vector3 startPosition, direction;

    void Start()
    {
        offsetZ = -0.85f;
        laserLength = 50f;
        laserLineRenderer = GetComponent<LineRenderer>();
        activeColor = (int)ColorProjectiles.Blue;
    }

    void Update()
    {
        shoulderAim.transform.position = new Vector3(shoulderAim.transform.position.x, shoulderAim.transform.position.y, offsetZ);
        cooldown = cooldowns[activeColor];

        if (fireRate > 0)
        {
            fireRate -= Time.deltaTime;
        }
        if (Input.GetMouseButton(0))
        {
            if (fireRate <= 0)
            {
                Shoot();
            }
        }
        else
        {
            //Om man vill kunna skjuta på en gång vid första klick så ta bort kommentaren från följande:
            //fireRate = 0;

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
            startPosition = rifleBarrel.transform.position;
            direction = rifleBarrel.transform.forward;
            Ray ray = new Ray(startPosition, direction);
            RaycastHit raycastHit;
            Vector3 endPosition = startPosition + (laserLength * direction);
            if (Physics.Raycast(ray, out raycastHit, laserLength))
            {
                endPosition = raycastHit.point;
            }

            laserLineRenderer.SetPosition(0, startPosition);
            laserLineRenderer.SetPosition(1, endPosition);

            laserLineRenderer.enabled = true;
        }
        currentBullet = Instantiate(colorsBullets[activeColor],
        new Vector3(rifleBarrel.transform.position.x, rifleBarrel.transform.position.y, rifleBarrel.transform.position.z), Quaternion.identity);
        fireRate = cooldown;
    }
}
