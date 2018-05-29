using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Timmy Alvelöv

//Ser till att knappen får rätt bild och text beroende på betyg och poäng.

public class Level_Button_Script : MonoBehaviour {
    
    Text scoreText;

    public void ChangeText(int score, int grade) //Ändrar text och bild beroende på betyg och score
    {

        scoreText = transform.GetChild(5).GetComponent<Text>();
        scoreText.text = "";
        for(int i = 0; i < 5; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        if (score != -1 && grade != 0)
        {
            scoreText.text = score.ToString(); //Sätter vilken poäng spelaren får
            transform.GetChild(grade - 1).gameObject.SetActive(true); //Sätter det betyg som spelaren får till aktiv
        }
    }
	
}
