
//Meny-script som hanterar alla menyer och knappar.
//Skapat av Moa Lindgren.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    GameObject Panel, MainMenu, LoadMenu, SettingsMenu, ConfirmQuit, CreditsMenu, PauseMenu, ScenesParent, Scenes;
    List<GameObject> Menus;
    int numberOfSaves;
    string gameScene;
    bool paused,
         inGame;
    [SerializeField]
    Slider master, music, effects, dialogue;
    [SerializeField]
    AudioSource tempMaster; //Bara för att simulera ljud TA BORT SEN

    //Spara Canvas till nästa scen.
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
    //Sätter alla värden
    void Start()
    {
        Panel = transform.GetChild(0).gameObject;
        Panel.SetActive(true);

        Menus = new List<GameObject>() { MainMenu, LoadMenu, SettingsMenu, CreditsMenu, ConfirmQuit, PauseMenu, Scenes };
        for (int i = 0; i < Menus.Count; i++)
        {
            Menus[i] = Panel.transform.GetChild(i).gameObject;
        }
        Menus[0].SetActive(true);
    }
    //Öppna och stänga pausmeny.
    void Update()
    {
        tempMaster.volume = master.value; //Här sätter man ljudetsvolym LÄGG IN RESTEN SEN

        if (Input.GetKeyDown(KeyCode.Escape) && inGame)
        {
            paused = !paused;
        }
        if (paused && inGame)
        {
            Time.timeScale = 0;
            Panel.SetActive(true);
            Menus[5].SetActive(true);
        }
        else if(!paused && inGame)
        {
            Time.timeScale = 1;
            for (int i = 0; i < Menus.Count; i++)
            {
                Menus[i].SetActive(false);
            }
            Panel.SetActive(false);
        }

    }

    //Välj scen.
    public void SceneButtons(string text)
    {
        inGame = true;
        Menus[6].SetActive(false); //behövs den här? (sätter "välj scen"-menyn till false)
        Panel.SetActive(false);
        SceneManager.LoadScene(text);
    }

    //Följande metod är kopplat till OnClick() på knapparna i menyn i Unity.
    //Index är specificerat hos vardera knapp i Unity.
    public void ClickButtons(int index)
    {
        Menus[0].SetActive(false);
        Menus[5].SetActive(false);

        //Settings, Credit och Confirm Quit.
        if (index == 2 || index == 3 || index == 4)
        {
            Menus[index].SetActive(true);
        }

        switch (index)
        {
            //New Game-knappen (visar en meny med knappar där du kan välja vilken scen du vill gå in i).
            case 0:
                Menus[6].SetActive(true);
                break;

            //Load Game
            case 1:
                Menus[1].SetActive(true);
                numberOfSaves = 5;          //Det här bör ändras till att den hämtar värde från vår spar-funktion.

                if (numberOfSaves == 0)
                {
                    //Här ska "No saved games yet" texten aktiveras.
                }
                else
                {
                    for (int i = 0; i < numberOfSaves; i++)
                    {
                        Menus[1].transform.GetChild(i).gameObject.SetActive(true);
                    }
                }
                break;


            //Back-knappen, går till MainMenu on inGame är false, och till PauseMenu om inGame är true.
            case 5:
                for (int i = 0; i < Menus.Count; i++)
                {
                    Menus[i].SetActive(false);
                }
                if (inGame)
                {
                    Menus[5].SetActive(true);
                }
                else
                {
                    Menus[0].SetActive(true);
                }
                break;

            //Yes-knapp i ConfirmQuit.
            case 6:
                Application.Quit();
                break;

            //Return knappen
            case 7:
                paused = false;
                Panel.SetActive(false);
                break;

            //Main menu knappen. Saknar dock en confirm knapp.
            case 8:
                inGame = false;
                Panel.SetActive(false);
                SceneManager.LoadScene("MenuScene");
                break;


        }
    }

}

