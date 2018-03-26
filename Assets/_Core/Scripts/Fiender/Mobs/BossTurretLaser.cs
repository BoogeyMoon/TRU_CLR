using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Av Andreas de Freitas
public class BossTurretLaser : MobStats
{
    [SerializeField]
    float laserCooldown, maxLaserCharge, laserLength, laserDamage;
    [SerializeField]
    GameObject laserCannon;
    LineRenderer bossLineRend;
    bool hasShot, charging;
    Vector3 startPosition, direction;

    protected override void Start()
    {
        base.Start();
        bossLineRend = GetComponent<LineRenderer>();
        charging = true;
        hasShot = false;
    }

    void Update()
    {
        laserCooldown -= Time.deltaTime;

        startPosition = laserCannon.transform.position;
        direction = laserCannon.transform.forward;
        Ray ray = new Ray(startPosition, direction);
        RaycastHit raycastHit;
        Vector3 endPosition = startPosition + (laserLength * direction);

        if (Physics.Raycast(ray, out raycastHit, laserLength))
        {
            endPosition = raycastHit.point;
            if (raycastHit.transform.gameObject.tag == "Player")
            {
                raycastHit.transform.gameObject.GetComponent<PlayerStats>().ChangeHealth(laserDamage);
            }
        }

        if (charging)
        {
            LookAtPlayer(laserCannon.transform);
        }

        if (laserCooldown < 0)
        {
            charging = false;
            ShootLaser();
        }

        if (hasShot)
        {
            charging = true;
            bossLineRend.enabled = false;
            laserCooldown += maxLaserCharge;
        }
    }


    protected virtual void ShootLaser()
    {
        bossLineRend.enabled = true;
        //fixa skotten här
        hasShot = true;
    }

}
