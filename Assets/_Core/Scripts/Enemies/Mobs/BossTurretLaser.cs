using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Av Andreas de Freitas och Timmy Alvelöv
//Hanterar turretbossens laserstråle
public class BossTurretLaser : MobStats
{
    [SerializeField]
    float maxLaserCharge, laserLength, chargeUpTime, laserDuration;
    float laserCooldown, laserDurTimer;
    LineRenderer bossLineRend;
    bool hasShot, cooldown, playerLayer;
    Vector3 startPosition, direction;
    Collider beam;

    protected override void Start()
    {
        base.Start();
        bossLineRend = GetComponent<LineRenderer>();
        laserCooldown = maxLaserCharge;
        laserDurTimer = laserDuration;
        cooldown = true;
        hasShot = false;
        bossLineRend.enabled = false;
        beam = transform.GetChild(0).GetComponent<Collider>();
        beam.enabled = false;
    }

    void Update() //Hanterar cooldown conditions 
    {
        laserCooldown -= Time.deltaTime;

        if (cooldown) //Ifall lasern är på cooldown så fortsätter den kolla efter spelaren
        {
            LookAtPlayer(eyes[0].transform);
        }
        else if (bossLineRend.enabled == true) //Kollar när lasern ska avaktiveras
        {
            laserDurTimer -= Time.deltaTime;
        }

        if (laserCooldown < 0) //Kallar på skjutmetoden
        {
            cooldown = false;
            StartCoroutine(ShootLaser());
        }
        if (laserDurTimer <= 0) //Avaktiverar lasern
        {
            bossLineRend.enabled = false;
            beam.enabled = false;
            laserDurTimer = laserDuration;
            cooldown = true;
        }
    }

    IEnumerator ShootLaser() //Skjuter en laserstråle vid en angiven tid
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
        beam.enabled = true;

    }

}
