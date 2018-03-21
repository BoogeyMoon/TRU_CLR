﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Av Andreas de Freitas Timmy Alvelöv

// Håller koll på hälsa, fart, osv. för mobs
public class MobStats : MonoBehaviour
{
    Score score;
    [SerializeField]
    protected float speed, maxHealth, fireRate, aggroRange, distanceInterval, timeBetweenBurst, shotsPerBurst, rotationBetweenBullets, health;
    [SerializeField]
    protected int color, numberOfBulletsPerShot, points;
    [SerializeField]
    protected GameObject[] bulletSpawners, eyes;
    [SerializeField]
    protected GameObject destination, bullet, patrolPoints;
    protected GameObject currentBullet;
    protected Transform player;
    protected List<Transform> patrolPointsList = new List<Transform>();
    protected bool onCooldown;
    protected float timeLeft, burstTimer, burstCounter, playerDistance;
    protected int patrolCounter;


    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("ShootHere").transform;
    }
    protected virtual void Start()
    {
        score = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<Score>();
        patrolCounter = 0;
        health = maxHealth;
        timeLeft = fireRate;
        onCooldown = false;
        updatePatrolPoints();
    }
    protected void updatePatrolPoints() //Kollar barnen på ett gameobject och lägger till dem i en lista.
    {
        if (patrolPoints != null)
        {
            for (int i = 0; i < patrolPoints.transform.childCount; i++)
            {
                patrolPointsList.Add(patrolPoints.transform.GetChild(i));
            }
            if (patrolPointsList.Count > 0)
                destination = patrolPointsList[0].gameObject;
        }

    }

    public virtual void TakeDamage(float damage, int color) //Om mob:en blir träffad av en kula som korresponderar med mob:ens färg så tar den skada.
    {
        if (color == this.color)
        {
            health -= damage;
            if (health <= 0)
            {
                Die();
            }
        }
    }

    public void GainHealth(float life) //Ökar mob:ens hälsa
    {
        health += life;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    void Die() //Mob:en dör.
    {
        score.AddScore(points);
        Destroy(gameObject);
    }


    public void ChangeDestination(GameObject newDestination, GameObject lastDestination) //Ger en mob sitt nästa mål, om input är null går den till nästa mål i sin lista.
    {
        if (lastDestination == destination) //Ser till att vi inte krockar in i fel patrullställe
        {
            if (newDestination != null) //Sätter ny destination
                destination = newDestination;
            else //Går till nästa plats i listan
            {
                if (patrolPointsList.Count != 1)
                {
                    if (patrolCounter > patrolPointsList.Count - 1)
                    {
                        patrolCounter = 0;
                    }
                    destination = patrolPointsList[patrolCounter].gameObject;
                    patrolCounter++;
                }
            }
        }

    }
    protected void Patrol() //Går mot nästa patrullplats
    {
        if (destination != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination.transform.position, speed / 2 * Time.deltaTime);
            transform.LookAt(destination.transform.position);
        }

    }

    protected virtual void Shoot() //Ser till att rätt antal skott skjuts samt räknar ut dess offset.
    {
        float startRot;
        onCooldown = true;
        if (numberOfBulletsPerShot % 2 == 0) //Om vi har ett jämnt antal kulor
        {
            startRot = ((numberOfBulletsPerShot / 2) - 1) * rotationBetweenBullets;
            startRot += rotationBetweenBullets / 2;
        }
        else //Om vi har ett udda antal kulor
        {
            int n = numberOfBulletsPerShot / 2;
            startRot = n * rotationBetweenBullets;
        }
        for (int i = 0; i < numberOfBulletsPerShot; i++)
        {
            ShootABullet(startRot - rotationBetweenBullets * i);
        }
        burstCounter--;
        if (burstCounter <= 0) //Räknar om vi är klara med bursten eller inte.
        {
            burstCounter = shotsPerBurst;
            burstTimer = timeBetweenBurst;
        }

        if (onCooldown)
        {
            timeLeft = fireRate;
        }
    }

    protected virtual void ShootABullet(float RotationOffset) //Sköter instansieringen av en kula och ger den en rotationsoffset.
    {
        for (int i = 0; i < bulletSpawners.Length; i++)
        {
            currentBullet = Instantiate(bullet); //Skapar en kula

            currentBullet.transform.position = bulletSpawners[i].transform.position; //Sätter positionen till mynningen på vapnet
            currentBullet.transform.rotation = bulletSpawners[i].transform.rotation; //Sätter rotationen så att skottet åker dit vapnet siktar
            currentBullet.transform.Rotate(RotationOffset, 0, 0); //Ändrar offseten för skottet om så önskas
        }

    }
    protected virtual float GetPlayerDistance(Transform position) //Ger tillbaka avståndet till spelaren med endast x -och yaxlarna i beaktning
    {
        return Mathf.Sqrt(Mathf.Abs(Mathf.Pow((player.transform.position.x - position.position.x), 2) + Mathf.Pow((player.transform.position.y - position.position.y), 2)));
    }
    protected bool CanSeePlayer()
    {
        for (int i = 0; i < eyes.Length; i++)
        {
            RaycastHit hit;

            if (Physics.Raycast(eyes[i].transform.position, new Vector3(player.position.x - eyes[i].transform.position.x, player.position.y - eyes[i].transform.position.y, player.position.z - eyes[i].transform.position.z), out hit, Mathf.Infinity))
            {
                Debug.DrawRay(eyes[i].transform.position, new Vector3(player.position.x - transform.position.x, player.position.y - transform.position.y, player.position.z - transform.position.z), Color.blue);
                if (hit.transform.gameObject.tag == "Player")
                {
                    return true;
                }
            }


        }
        return false;
    }

}
