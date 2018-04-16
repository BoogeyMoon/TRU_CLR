﻿using System.Collections;
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
            text.text = "\nGame" + gameNumber + "\n score: " + score;
            transform.GetChild(grade - 1).gameObject.SetActive(true);
        }
        else
        {
            print("Invalid input");
        }
        
    }
	
}
