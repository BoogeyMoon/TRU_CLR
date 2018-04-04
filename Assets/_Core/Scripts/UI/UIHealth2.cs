using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// Av Andreas de Freitas
//Hanterar det visuella för spelarens hälsa
public class UIHealth2 : MonoBehaviour
{
    PlayerStats playerStats;

    [SerializeField]
    int currentHealth;

    int maxHealth = 20, fullHealth;

    [SerializeField]
    Image[] healthImages;

    void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        currentHealth = (int)playerStats.Health;
        fullHealth = maxHealth;
        CheckHealth(currentHealth);
    }

    void CheckHealth(int health)
    {

        for (int i = 0; i < maxHealth; i++)
        {
            healthImages[i].enabled = false;

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
