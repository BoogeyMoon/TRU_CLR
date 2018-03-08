using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
// Av Andreas de Freitas Timmy Alvelöv

// Håller koll på hälsa, fart, osv. för mobs
public class MobStats : MonoBehaviour
{
    [SerializeField]
    float health, speed, maxHealth, fireRate, startTime;
    float timeLeft;
    [SerializeField]
    int color;
    [SerializeField]
    GameObject destination, bullet, bulletSpawner;
    GameObject currentBullet;
    NavMeshAgent agent;
    bool onCooldown;

    void Start()
    {
        health = maxHealth;
        agent = GetComponent<NavMeshAgent>();
        timeLeft = startTime;
        onCooldown = false;
    }
    void Update()
    {
        timeLeft -= Time.deltaTime;

        if (destination != null)
        {
            Move();
        }

        if (Input.GetMouseButtonDown(1))
        {
            Shoot();
        }

        if (timeLeft < 0)
        {
            Shoot();
        }
    }
    public void TakeDamage(float damage, int color) //Om mob:en blir träffad av en kula som korresponderar med mob:ens färg så tar den skada.
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
        Destroy(gameObject);
    }

    void Shoot() //Mob:en skjuter
    {
        currentBullet = Instantiate(bullet);
        onCooldown = true;

        currentBullet.transform.position = bulletSpawner.transform.position;
        currentBullet.transform.rotation = bulletSpawner.transform.rotation;

        if (onCooldown)
        {
            timeLeft = startTime;
        }

    }

    void Move() //Flyttar mob:en
    {
        transform.position = Vector3.MoveTowards(agent.transform.position, destination.transform.position, speed * Time.deltaTime);
    }
    public void ChangeDestination(GameObject newDestination)
    {
        destination = newDestination;
    }





}
