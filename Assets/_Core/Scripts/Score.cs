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
    int startScore, currentScore, scoreMultiplier;

    [SerializeField]
    float multiplierSpeed, startCounter;

    void Start()
    {
        currentScore = startScore;
        InvokeRepeating("LooseScore", startCounter, multiplierSpeed);
    }

    void Update()
    {
        displayScore.text = "Score: " + currentScore.ToString();
    }

    public void LooseScore() //Kommer ta in score parametrar sen
    {
        currentScore -= scoreMultiplier;
    }

    public void AddScore(int score)
    {
        currentScore += score;
    }
}

