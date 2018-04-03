using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScript : MonoBehaviour
{
    Score ScoreManager;

    void Start()
    {
        ScoreManager = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<Score>();
    }

    void OnTriggerEnter()
    {
        print(ScoreManager.GetGrade());

    }
	
}
