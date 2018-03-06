using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Av Andreas de Freitas Timmy Alvelöv

// Håller koll på hälsa, fart, osv. för mobs
public class MobStats : MonoBehaviour
{
    [SerializeField]
    float health, speed, maxHealth, fireRate;
    [SerializeField]
    int color;


    void Start()
    {
        health = maxHealth;
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

    }

    void Move() //Flyttar mob:en
    {

    }





}
