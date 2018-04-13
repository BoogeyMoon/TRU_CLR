using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


//Meny-script som hanterar alla menyer och knappar.
//Skapat av Moa Lindgren.

public class MenuScript : MonoBehaviour
{
    GameObject MainMenu, LoadMenu, SettingsMenu, ConfirmQuit, CreditsMenu, PauseMenu;
    [SerializeField]
    GameObject EventSystem;
    List<GameObject> Menus;
    int numberOfLevels, unlockedLevels;
    int score;
    bool paused, inGame;

    XmlScript xmlScript;

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
        xmlScript = GameObject.FindGameObjectWithTag("Canvas").GetComponent<XmlScript>();
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
        numberOfLevels = xmlScript.numberOfLevels;
        for (int i = 0; i < numberOfLevels; i++)
        {
            Menus[1].transform.GetChild(i).gameObject.SetActive(true); //Sätter parent aktiv.
            int tempScore = xmlScript.scoreList[i];
            Menus[1].transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
            Menus[1].transform.GetChild(0).transform.GetChild(0).GetComponent<Level_Button_Script>().ChangeText(xmlScript.GetScore(0), xmlScript.GetGrade(0));
            unlockedLevels = i + 1; //+1 för att nästa level ska låsas upp när en level är avklarad.
            if (tempScore > 0) //bör vara grade istället.
            {
                //Följande sätter alla upplåsta levels aktiva:
                Menus[1].transform.GetChild(unlockedLevels).transform.GetChild(0).gameObject.SetActive(true);
                Menus[1].transform.GetChild(unlockedLevels).transform.GetChild(0).GetComponent<Level_Button_Script>().
                    ChangeText(xmlScript.GetScore(unlockedLevels+1), xmlScript.GetGrade(unlockedLevels));             //Ser till att texten motsvarar spelarens poäng och betyg
            }
            //Följande sätter alla låsta levels aktiva:
            else
            {
                Menus[1].transform.GetChild(unlockedLevels).transform.GetChild(1).gameObject.SetActive(true);
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
        for (int i = 0; i < numberOfLevels; i++)
        {
            for (int j = 0; j < Menus[1].transform.GetChild(i).childCount; j++)
            {
                Menus[1].transform.GetChild(i).GetChild(j).gameObject.SetActive(false);
            }
        }
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

