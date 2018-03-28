using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Av Andreas de Freitas
public class BossTurretLaser : MobStats
{
    [SerializeField]
    float maxLaserCharge, laserLength,chargeUpTime, laserDuration;
    float laserCooldown, laserDurTimer;
    LineRenderer bossLineRend;
    bool hasShot, cooldown, playerLayer;
    Vector3 startPosition, direction;

    protected override void Start()
    {
        base.Start();
        bossLineRend = GetComponent<LineRenderer>();
        cooldown = true;
        hasShot = false;
    }

    void Update()
    {
        laserCooldown -= Time.deltaTime;

        if (cooldown)
        {
            LookAtPlayer(bulletSpawners[0].transform);
        }
        else if(bossLineRend.enabled == true)
        {
            laserDurTimer -= Time.deltaTime;
        }

        if (laserCooldown < 0)
        {
            cooldown = false;
            StartCoroutine(ShootLaser());
        }
        if(laserDurTimer <= 0)
        {
            bossLineRend.enabled = false;
            laserDurTimer = laserDuration;
            cooldown = true;
        }
    }

    IEnumerator ShootLaser()
    {
        startPosition = bulletSpawners[0].transform.position;
        direction = bulletSpawners[0].transform.forward;
        Ray ray = new Ray(startPosition, direction);
        RaycastHit raycastHit;
        Vector3 endPosition = startPosition + (laserLength * direction);
        if (Physics.Raycast(ray, out raycastHit, laserLength))
        {
            endPosition = raycastHit.point;
        }
        laserCooldown = maxLaserCharge + chargeUpTime;
        bossLineRend.SetPosition(0, startPosition);
        bossLineRend.SetPosition(1, endPosition);
        yield return new WaitForSeconds(chargeUpTime);
        bossLineRend.enabled = true;
        
    }

}
