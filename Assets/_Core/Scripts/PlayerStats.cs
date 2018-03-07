﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//Av Andreas de Freitas och Timmy Alvelöv.

//Håller koll på spelarens hälsa och checkpoints

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    float maxHealth;
    [SerializeField]
    float health, cPHealth;

    string checkpoint;

    void Start()
    {
        if (PlayerPrefs.GetFloat("savedHealth") != -1)
        {
            health = cPHealth;
        }
        
    }

    public void ChangeHealth(float value) //Lägg till eller ta bort hälsa från spelaren
    {
        health += value;

        if (health >= maxHealth)
        {
            health = maxHealth;
        }

        if (health <= 0)
        {
            PlayerDies();
        }
    }

    public void PlayerDies() //Ifall spelaren dör
    {
        PlayerPrefs.SetFloat("savedHealth", cPHealth);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        health = cPHealth;
        //  Lastcheckpoint();
    }

    public void SetCheckpoint(string checkpoint) //Bestämmer checkpointen för spelaren
    {
        this.checkpoint = checkpoint;
        cPHealth = health;
        
    }
}
