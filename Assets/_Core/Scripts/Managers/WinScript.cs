using System.Collections;
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
    GameObject canvas;
    void Start()
    {
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
        StartCoroutine(Win());
    }
    IEnumerator Win()
    {
        ScoreManager.GetGrade();
        yield return new WaitForSeconds(1.5f);
        canvas.transform.GetChild(7).gameObject.SetActive(true);
        Time.timeScale = 0;
    }

}

