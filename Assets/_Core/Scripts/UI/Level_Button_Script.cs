using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level_Button_Script : MonoBehaviour {
    
    Text text;
    [SerializeField]
    int gameNumber;

    public void ChangeText(int score, int grade)
    {
        if(score != -1 && grade != 0)
        {
            text = transform.GetChild(transform.childCount-1).GetComponent<Text>();
            string myGrade;
            switch (grade)
            {
                case (1):
                    myGrade = "Pass";
                    break;
                case (2):
                    myGrade = "Good";
                    break;
                case (3):
                    myGrade = "Great";
                    break;
                case (4):
                    myGrade = "Awesome";
                    break;
                case (5):
                    myGrade = "TRU_CLR!";
                    break;
                default:
                    myGrade = "NO GRADE";
                    break;
            }
            text.text = "\nGame" + gameNumber + "\n score: " + score;
            transform.GetChild(grade - 1).gameObject.SetActive(true);
        }
        else
        {
            print("Invalid input");
        }
        
    }
	
}
