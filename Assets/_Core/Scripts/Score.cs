using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Av Andreas de Freitas
public class Score : MonoBehaviour
{
    [SerializeField]
    Text displayScore;

    [SerializeField]
    int currentScore;

    [SerializeField]
    float multiplierSpeed, startCounter;

    void Start()
    {
        InvokeRepeating("AddScore", startCounter, multiplierSpeed);
    }

    void Update()
    {
        displayScore.text = "Score: " + currentScore.ToString();
    }

    void AddScore() //Kommer ta in score parametrar sen
    {
        int score = 10;
        currentScore += score;
    }
}

