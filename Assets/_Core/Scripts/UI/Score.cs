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
    int startScore, currentScore, scoreMultiplier, levelIndex;
    public int LevelIndex { get { return levelIndex; } }
    int scorePerLife;

    public int CurrentScore { get { return currentScore; } }

    [SerializeField]
    int[] gradesCaps = new int[4];

    [SerializeField]
    float multiplierSpeed, startCounter;
    float timer;

    string[] grades = new string[] { "Pass", "Good", "Great", "Amazing", "TRU_CLR!" };

    XmlScript xml;
    PlayerStats player;

    void Start()
    {
        scorePerLife = 80;
        currentScore = startScore;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        xml = GameObject.FindGameObjectWithTag("Canvas").GetComponent<XmlScript>();
        timer = startCounter;
    }

    void Update()
    {
        displayScore.text = "Score: " + currentScore.ToString();
        timer -= Time.deltaTime;
        if(timer <= 0 )
        {
            LooseScore();
            timer = multiplierSpeed;
            multiplierSpeed -= 0.1f;
            if (multiplierSpeed < 1)
                multiplierSpeed = 1;
        }

    }

    public void LooseScore() //Kommer ta in score parametrar sen
    {
        currentScore -= scoreMultiplier;
        if (currentScore < 0)
            currentScore = 0;
    }

    public void AddScore(int score) //Lägger till ytterligare poäng till spelarens nuvarande score
    {
        currentScore += score;
    }
    public int GetGrade() //Återlämnar vilket betyg spelaren skulle få med sin nuvarande poäng och sparar det i XML-databasen om det är ett rekord
    {
        currentScore += (int)player.Health * scorePerLife;
        print("score: " + currentScore);
        
        for (int i = 0; i < gradesCaps.Length; i++)
        {
            if (currentScore >= gradesCaps[gradesCaps.Length - 1 - i])
            {
                string i1;
                int gradeIndex;
                i1 = grades[grades.Length - i - 1];
                gradeIndex = grades.Length - i;
                if (xml.GetScore(levelIndex) < currentScore)
                    xml.ChangeStats(levelIndex, currentScore, gradeIndex);
                print("Grade: " + gradeIndex);
                return gradeIndex;
            }
        }
        if (xml.GetScore(levelIndex) < currentScore)
            xml.ChangeStats(levelIndex, currentScore, 1);
        return 1;
    }
}

