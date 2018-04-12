
//Meny-script som hanterar alla menyer och knappar.
//Skapat av Moa Lindgren.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    GameObject MainMenu, LoadMenu, SettingsMenu, ConfirmQuit, CreditsMenu, PauseMenu;
    [SerializeField]
    GameObject EventSystem;
    List<GameObject> Menus;
    int numberOfLevels;
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
        DontDestroyOnLoad(EventSystem);
        gameObject.transform.parent = GameObject.Find("Canvas").transform;
    }
    //Sätter alla värden
    void Start()
    {
        inGame = false;

        Menus = new List<GameObject>() { MainMenu, LoadMenu, SettingsMenu, CreditsMenu, ConfirmQuit, PauseMenu };

        for (int i = 0; i < Menus.Count; i++)
        {
            Menus[i] = transform.GetChild(i).gameObject;
        }
        Menus[0].SetActive(true);
    }
    //Öppna och stänga pausmeny.
    void Update()
    {
        tempMaster.volume = master.value; //Här sätter man ljudetsvolym LÄGG IN RESTEN SEN

        //if (Input.GetKeyDown(KeyCode.Escape) && inGame)
        //{
        //    paused = !paused;
        //    if (paused)
        //    {
        //        //Canvas.SetActive(true);
        //        Menus[5].SetActive(true);
        //    }
        //    else
        //    {
        //        for (int i = 0; i < Menus.Count; i++)
        //        {
        //            Menus[i].SetActive(false);
        //        }
        //        //Canvas.SetActive(false);
        //    }
        //}
        if (paused && inGame)
        {
            Time.timeScale = 0;
        }
        else if (!paused && inGame)
        {
            Time.timeScale = 1;
        }

    }

    public void LoadGame(string gameScene)
    {
        Menus[1].SetActive(false); //varför?
        inGame = true;
        //Canvas.SetActive(false);
        SceneManager.LoadScene(gameScene);
    }

    public void LevelSelect()
    {
        Menus[0].SetActive(false);
        Menus[1].SetActive(true);

        //Hämta värde från xml:
        int unlockedLevels = 3;
        numberOfLevels = 8;
        for (int i = 0; i < numberOfLevels; i++)
        {
            Menus[1].transform.GetChild(i).gameObject.SetActive(true);

            for (int j = 0; j < unlockedLevels; j++)
            {
                Menus[1].transform.GetChild(j).transform.GetChild(0).gameObject.SetActive(true);
            }
            for (int k = unlockedLevels; k < numberOfLevels; k++)
            {
                Menus[1].transform.GetChild(k).transform.GetChild(1).gameObject.SetActive(true);
            }

        }
    }

    public void GeneralIndexButton(int index)
    {
        Menus[0].SetActive(false);
        Menus[index].SetActive(true);
    }

    public void Back()
    {
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
    }

    public void ResumeGame()
    {
        paused = false;
        //Canvas.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void MainMenuButton()
    {
        inGame = false;
        //Canvas.SetActive(false);
        SceneManager.LoadScene("MenuScene");
    }

    

}

