using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Av Andreas de Freitas Timmy Alvelöv

// Håller koll på hälsa, fart, osv. för mobs
public class MobStats : MonoBehaviour
{
    [SerializeField]
    protected float speed, maxHealth, fireRate;
    [SerializeField]
    protected int color;
    [SerializeField]
    protected GameObject destination, bullet, bulletSpawner, patrolPoints;
    protected GameObject currentBullet;
    protected Transform player;
    protected List<Transform> patrolPointsList = new List<Transform>();
    protected bool onCooldown;
    protected float health, timeLeft;
    protected int patrolCounter;



    protected virtual void Start()
    {
        patrolCounter = 0;
        health = maxHealth;
        player = GameObject.Find("SK_MainCharacter_PF").transform;
        timeLeft = fireRate;
        onCooldown = false;
        updatePatrolPoints();


    }
    protected void updatePatrolPoints() //Kollar barnen på ett gameobject och lägger till dem i en lista.
    {
        if(patrolPoints != null)
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
        Destroy(gameObject);
    }


    public void ChangeDestination(GameObject newDestination, GameObject lastDestination) //Ger en mob sitt nästa mål, om input är null går den till nästa mål i sin lista.
    {
        if(lastDestination == destination) //Ser till att vi inte krockar in i fel patrullställe
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
    protected void patrol() //Går mot nästa patrullplats
    {
        if (destination != null)
            transform.position = Vector3.MoveTowards(transform.position, destination.transform.position, speed / 2 * Time.deltaTime);
    }





}
