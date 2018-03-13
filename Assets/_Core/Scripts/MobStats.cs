using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Av Andreas de Freitas Timmy Alvelöv

// Håller koll på hälsa, fart, osv. för mobs
public class MobStats : MonoBehaviour
{
    [SerializeField]
    protected float health, speed, maxHealth, fireCooldown;
    [SerializeField]
    protected int color;
    [SerializeField]
    protected GameObject destination, bullet, bulletSpawner;
    protected GameObject currentBullet;
    protected Transform player;
    protected bool onCooldown;
    protected float timeLeft;


    protected virtual void Start()
    {
        health = maxHealth;
        player = GameObject.Find("SK_MainCharacter_PF").transform;
        timeLeft = fireCooldown;
        onCooldown = false;


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

    
    public void ChangeDestination(GameObject newDestination)
    {
        destination = newDestination;
    }





}
