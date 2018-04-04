using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Av Andreas de Freitas
//Betyg tillagt av Timmy Alvelöv
public class Score : MonoBehaviour
{
    [SerializeField]
    Text displayScore;

    [SerializeField]
    int startScore, currentScore, scoreMultiplier;

    [SerializeField]
    int[] gradesCaps = new int[4];

    [SerializeField]
    float multiplierSpeed, startCounter;

    string[] grades = new string[] {"Pass","Good","Great","Amazing","TRU_CLR!"};

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
    public string GetGrade() //Återlämnar vilket betyg spelaren skulle få med sin nuvarande poäng
    {
        for (int i = 0; i < gradesCaps.Length; i++)
        {
            if(currentScore >= gradesCaps[gradesCaps.Length -1-i])
            {
                return grades[grades.Length - i -1];
            }
        }
        return grades[0];
    }
}

