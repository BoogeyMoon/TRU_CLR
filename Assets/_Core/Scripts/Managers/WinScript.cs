﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Av Timmy Alvelöv

//Låter spelaren vinna när alla conditions har blivit uppfyllda.
public class WinScript : MonoBehaviour
{
    
    [SerializeField]
    List<Transform> winCons;
    Score ScoreManager;
    bool[] winConditions;
    void Start()
    {
        ScoreManager = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<Score>();
        print(winCons.Count);
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
        Win();
    }
    void Win()
    {
        print("Du vann! Du fick betyget: " + ScoreManager.GetGrade());
    }

}

