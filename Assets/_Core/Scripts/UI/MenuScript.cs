using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Meny-script som hanterar alla menyer och knappar.
//Skapat av Moa Lindgren.
public class MenuScript : MonoBehaviour
{
    GameObject mainMenu, loadMenu, settingsMenu, confirmQuit, creditsMenu, pauseMenu, pausePanel, winScreen, loseScreen, areYouSure;
    [SerializeField]
    GameObject eventSystem;
    AudioSource menuSound;
    List<GameObject> menus;
    int numberOfLevels, unlockedLevels;
    int score;
    string currentGameScene;
    bool paused, inGame;
    public bool Paused
    {
        get { return paused; }
    }
    XmlScript xmlScript;
    [SerializeField]
    Slider master, music, effects, dialogue;
    [SerializeField]
    AudioSource tempMaster; //Bara för att simulera ljud TA BORT SEN

    //Spara Canvas till nästa scen.
    void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("MenuCanvas").Length > 1)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        xmlScript = GameObject.FindGameObjectWithTag("Canvas").GetComponent<XmlScript>();
    }
    //Sätter alla värden
    void Start()
    {
        inGame = false;
        menuSound = GameObject.FindGameObjectWithTag("MenuCanvas").GetComponent<AudioSource>();
        menus = new List<GameObject>() { mainMenu, pausePanel, loadMenu, settingsMenu, creditsMenu, confirmQuit, pauseMenu, winScreen, loseScreen, areYouSure };
        for (int i = 0; i < menus.Count; i++)
        {
            menus[i] = transform.GetChild(i).gameObject;
        }
        menus[0].SetActive(true);
    }
    //Öppna och stänga pausmeny.
    void Update()
    {
        tempMaster.volume = master.value; //Här sätter man ljudetsvolym LÄGG IN RESTEN SEN

        if (Input.GetKeyDown(KeyCode.Escape) && inGame)
        {
            paused = !paused;
            if (paused)
            {
                menus[1].SetActive(true);
                menus[6].SetActive(true);
            }
            else
            {
                for (int i = 0; i < menus.Count; i++)
                {
                    menus[i].SetActive(false);
                }
            }
        }
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
        currentGameScene = gameScene;
        SetMenusInactive();
        inGame = true;
        menuSound.Stop();
        SceneManager.LoadScene(currentGameScene);
    }

    public void LevelSelect()
    {
        menus[0].SetActive(false);
        menus[2].SetActive(true);
        numberOfLevels = xmlScript.numberOfLevels;
        for (int i = 0; i < menus[2].transform.childCount; i++) //Stänger av allt så att inget är kvar från förra sparningen
        {
            for (int j = 0; j < menus[2].transform.GetChild(i).childCount; j++)
            {
                for (int c = 0; c < menus[2].transform.GetChild(i).GetChild(j).childCount-1; c++)
                {
                    menus[2].transform.GetChild(i).GetChild(j).GetChild(c).gameObject.SetActive(false);
                }
                menus[2].transform.GetChild(i).GetChild(j).gameObject.SetActive(false);
            }
        }
        for (int i = 0; i < numberOfLevels; i++)
        {
            menus[2].transform.GetChild(i).gameObject.SetActive(true); //Sätter parent aktiv.
            int tempScore = xmlScript.ScoreList[i];
            menus[2].transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
            menus[2].transform.GetChild(0).transform.GetChild(0).GetComponent<Level_Button_Script>().ChangeText(xmlScript.GetScore(0), xmlScript.GetGrade(0));
            unlockedLevels = i + 1; //+1 för att nästa level ska låsas upp när en level är avklarad.
            if (tempScore > 0) //bör vara grade istället.
            {
                //Följande sätter alla upplåsta levels aktiva:
                menus[2].transform.GetChild(unlockedLevels).transform.GetChild(0).gameObject.SetActive(true);
                menus[2].transform.GetChild(unlockedLevels).transform.GetChild(0).GetComponent<Level_Button_Script>().
                ChangeText(xmlScript.GetScore(unlockedLevels + 1), xmlScript.GetGrade(unlockedLevels));             //Ser till att texten motsvarar spelarens poäng och betyg
            }
            //Följande sätter alla låsta levels aktiva:
            else
            {
                menus[2].transform.GetChild(unlockedLevels).transform.GetChild(1).gameObject.SetActive(true);
            }

        }


    }

    //Settings och Credits:
    public void GeneralIndexButton(int index)
    {
        SetMenusInactive();
        //Menus[1].SetActive(true);
        menus[index].SetActive(true);
    }

    public void Back()
    {
        SetMenusInactive();
        if (inGame)
        {
            menus[6].SetActive(true);
            menus[1].SetActive(true);
        }
        else
        {
            menus[0].SetActive(true);
        }
    }

    public void ResumeGame()
    {
        paused = false;
        SetMenusInactive();
    }

    public void QuitGame()
    {
        menus[9].gameObject.SetActive(true);
    }
    public void SureQuit()
    {
        Application.Quit();
    }
    public void CloseConfirmMeny()
    {
        menus[9].gameObject.SetActive(false);
    }

    public void MainMenuButton()
    {
        inGame = false;
        SetMenusInactive();
        SceneManager.LoadScene("MenuScene");
        menuSound.Play();
    }

    void SetMenusInactive()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            menus[i].SetActive(false);
        }
        if (inGame && paused)
        {
            menus[1].SetActive(true);
        }
    }

    public void ChangeUser()
    {
        SetMenusInactive();
        xmlScript.ActivatePanel(true);
        SceneManager.LoadScene("LogInScene");
    }

    public void Restart()
    {
        SetMenusInactive();
        SceneManager.LoadScene(currentGameScene);
    }
    public void SetMainMenu(bool enabled)
    {
        menus[0].SetActive(enabled);
    }


}

