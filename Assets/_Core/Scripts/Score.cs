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
    int currentScore, positiveScore, negativeScore;

    [SerializeField]
    float multiplierSpeed, startCounter;

    void Start()
    {
        InvokeRepeating("AddScore", startCounter, multiplierSpeed);
        InvokeRepeating("DecreaseScore", startCounter, multiplierSpeed);
    }

    void Update()
    {
        displayScore.text = "Score: " + currentScore.ToString();
    }

    void AddScore()
    {
        currentScore += positiveScore;
    }

    void DecreaseScore()
    {
        if (currentScore > 0)
        {
            currentScore -= negativeScore;
        }
    }

}

