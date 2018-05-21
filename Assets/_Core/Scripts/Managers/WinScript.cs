using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Av Timmy Alvelöv

//Låter spelaren vinna när alla conditions har blivit uppfyllda.
public class WinScript : MonoBehaviour
{
    
    [SerializeField]
    List<Transform> winCons;
    Sprite[] grades;

    Score ScoreManager;
    bool[] winConditions;
    GameObject canvas;

    void Start()
    {

        Sprite Pass = Resources.Load("Pass", typeof(Sprite)) as Sprite;
        Sprite Good = Resources.Load("Good", typeof(Sprite)) as Sprite;
        Sprite Great = Resources.Load("Great", typeof(Sprite)) as Sprite;
        Sprite Awesome = Resources.Load("Awesome", typeof(Sprite)) as Sprite;
        Sprite TRUCLR = Resources.Load("TRU CLR", typeof(Sprite)) as Sprite;

        print(Pass.name);

        grades = new Sprite[] { Pass, Good, Great, Awesome, TRUCLR };

        animatedCharacter = Resources.Load("SK_AnimatedMCWIN_PF") as GameObject;
        

        canvas = GameObject.FindGameObjectWithTag("MenuCanvas").gameObject;
        ScoreManager = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<Score>();
        winConditions = new bool[winCons.Count];
    }

    public void WinConFinished(Transform winConHolder) //Anropas av något när dess winCondition är färdigt
    {
        winCons.Remove(winConHolder);
        winConditions[winCons.Count] = true;
        for (int i = 0; i < winConditions.Length; i++)
        {
            if (!winConditions[i])
                return;
        }

        gameWon = true;
        StartCoroutine(Win());
    }

    IEnumerator Win() // Hanterar allt som ska hänta när man vinner
    {
        Vector3 playerposition = GameObject.FindGameObjectWithTag("Player").transform.position;

        Vector3 camerahereposition = GameObject.Find("CameraGoesHere").transform.position;

        Vector3 animatedspawn = new Vector3(camerahereposition.x, camerahereposition.y, playerposition.z);

        animatedCharacter = Instantiate(animatedCharacter, animatedspawn, new Quaternion(0, 90, 0, 0));

        yield return new WaitForSeconds(1.5f);
        canvas.transform.GetChild(7).gameObject.SetActive(true);
        Text levelName = canvas.transform.GetChild(7).GetChild(0).GetChild(0).GetComponent<Text>();
        levelName.text = "Level " + (ScoreManager.LevelIndex +1);
        Text baseScore = canvas.transform.GetChild(7).GetChild(0).GetChild(3).GetComponent<Text>();
        baseScore.text = ScoreManager.CurrentScore.ToString();
        Text totalScore = canvas.transform.GetChild(7).GetChild(0).GetChild(5).GetComponent<Text>();
        int grade = ScoreManager.GetGrade();
        totalScore.text = ScoreManager.CurrentScore.ToString();
        Image rating = canvas.transform.GetChild(7).GetChild(0).GetChild(7).GetComponent<Image>();
        rating.sprite = grades[grade - 1];
        Time.timeScale = 0;
    }

}

