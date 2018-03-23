using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealth2 : MonoBehaviour
{
    PlayerStats playerStats;

    [SerializeField]
    int startLives, currentHealth, heal;

    int maxHealthAmount = 10, fullHealth;

    [SerializeField]
    Image[] healthImages;


    void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        currentHealth = startLives * heal;
        fullHealth = maxHealthAmount * heal;
        CheckHealth(fullHealth);
    }

    void CheckHealth(int health)
    {
        for (int i = 0; i < maxHealthAmount; i++)
        {
            if (health <= i)
            {
                healthImages[i].enabled = false;
            }
            else
            {
                healthImages[i].enabled = true;
            }
        }
    }

    public void TakeDamage(int health)
    {
        currentHealth = health;
        currentHealth = Mathf.Clamp(currentHealth, 0, startLives * heal);
        CheckHealth(health);
    }

    /*   void AddHealth()
       {
           startLives++;
           startLives = Mathf.Clamp(startLives, 0, maxHealthAmount);

           // currentHealth = startLives * heal;
           // fullHealth = maxHealthAmount * heal;

           CheckHealth(startLives);
       } */
}
