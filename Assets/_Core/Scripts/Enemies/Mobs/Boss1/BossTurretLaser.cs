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
    bool hasShot, cooldown, playerLayer, shootingLaser;
    Vector3 startPosition, direction;
    Collider beam;
    Vector3 endPosition;
    AudioManager audio;
    ParticleSystem[] particleSystems;


    protected override void Start()
    {
        base.Start();
        laserCooldown = maxLaserCharge;
        laserDurTimer = laserDuration;
        cooldown = true;
        hasShot = false;
        shootingLaser = false;
        beam = transform.GetChild(2).GetComponent<Collider>();
        beam.enabled = false;
        transform.position = new Vector3(transform.position.x, transform.position.y, player.transform.position.z);
        particleSystems = new ParticleSystem[] { transform.GetChild(0).GetComponent<ParticleSystem>(), transform.GetChild(1).GetComponent<ParticleSystem>() };
        audio = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    void Update() //Hanterar cooldown conditions 
    {
        laserCooldown -= Time.deltaTime;
        playerDistance = GetPlayerDistance(raycastOrigin[0].transform);
        if (playerDistance < aggroRange)
        {
            if (cooldown) //Ifall lasern är på cooldown så fortsätter den kolla efter spelaren
            {
                LookAtPlayer(raycastOrigin[0].transform);
            }
            if (laserCooldown < 0) //Kallar på skjutmetoden
            {
                cooldown = false;
                StartCoroutine(ShootLaser());
            }
        }
        if (!cooldown && shootingLaser == true) //Kollar när lasern ska avaktiveras
        {
            laserDurTimer -= Time.deltaTime;
        }
        if (laserDurTimer <= 0) //Avaktiverar lasern
        {
            particleSystems[1].Stop(true);
            particleSystems[1].Clear(true);
            shootingLaser = false;
            beam.enabled = false;
            laserDurTimer = laserDuration;
            cooldown = true;
        }

    }

    IEnumerator ShootLaser() //Skjuter en laserstråle vid en angiven tid
    {
        particleSystems[0].Clear(true);
        particleSystems[0].Play(true);
        startPosition = bulletSpawners[0].transform.position;
        direction = bulletSpawners[0].transform.forward;
        endPosition = transform.GetChild(3).position;
        laserCooldown = maxLaserCharge + chargeUpTime;
        yield return new WaitForSeconds(chargeUpTime);
        audio.Play("laser 1 sec");
        particleSystems[0].Stop(true);
        particleSystems[0].Clear(true);
        particleSystems[1].Clear(true);
        particleSystems[1].Play(true);
        shootingLaser = true;
        beam.enabled = true;

    }
    public void Upgrade() //Gör så att cooldownen på bossen minskar
    {
        if (maxLaserCharge > 1)
            maxLaserCharge = maxLaserCharge - 1;
    }

}
