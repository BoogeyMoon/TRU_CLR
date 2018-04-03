using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScript : MonoBehaviour
{
    Score ScoreManager;
    List<bool> winConditions = new List<bool>();

    void Start()
    {
        ScoreManager = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<Score>();
    }

    void OnTriggerEnter()
    {
        for (int i = 0; i < winConditions.Count; i++)
        {

        }
        print(ScoreManager.GetGrade());

    }

}

