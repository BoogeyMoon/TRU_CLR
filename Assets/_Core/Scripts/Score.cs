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
    int currentScore, scoreMultiplyer;

    void Start()
    {
        InvokeRepeating("AddScore", 1, 1);
    }

    void Update()
    {
        displayScore.text = "Score: " + currentScore.ToString();
    }

    void AddScore()
    {
        currentScore += scoreMultiplyer;
    }

}
