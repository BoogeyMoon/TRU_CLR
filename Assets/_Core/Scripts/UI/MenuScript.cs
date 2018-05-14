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
    AudioSource tempMaster; 

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
        menuSound.Play();
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
            if(!menus[7].activeInHierarchy && !menus[8].activeInHierarchy  )
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
        Transform levelParent = menus[2].transform.GetChild(0);
        numberOfLevels = xmlScript.numberOfLevels;
        for (int i = 0; i < levelParent.childCount; i++) //Stänger av allt så att inget är kvar från förra sparningen
        {
            for (int j = 0; j < levelParent.transform.GetChild(i).childCount; j++)
            {
                for (int c = 0; c < levelParent.transform.GetChild(i).GetChild(j).childCount-1; c++)
                {
                    levelParent.transform.GetChild(i).GetChild(j).GetChild(c).gameObject.SetActive(false);
                }
                levelParent.transform.GetChild(i).GetChild(j).gameObject.SetActive(false);
            }
        }
        for (int i = 0; i < numberOfLevels; i++)
        {
            levelParent.GetChild(i).gameObject.SetActive(true); //Sätter parent aktiv.
            int tempScore = xmlScript.ScoreList[i];
            levelParent.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
            levelParent.GetChild(0).transform.GetChild(0).GetComponent<Level_Button_Script>().ChangeText(xmlScript.GetScore(0), xmlScript.GetGrade(0));
            unlockedLevels = i + 1; //+1 för att nästa level ska låsas upp när en level är avklarad.
            if (tempScore > 0) //bör vara grade istället.
            {
                //Följande sätter alla upplåsta levels aktiva:
                levelParent.GetChild(unlockedLevels).transform.GetChild(0).gameObject.SetActive(true);
                levelParent.GetChild(unlockedLevels).transform.GetChild(0).GetComponent<Level_Button_Script>().
                ChangeText(xmlScript.GetScore(unlockedLevels), xmlScript.GetGrade(unlockedLevels));             //Ser till att texten motsvarar spelarens poäng och betyg
            }
            //Följande sätter alla låsta levels aktiva:
            else
            {
                levelParent.GetChild(unlockedLevels).transform.GetChild(1).gameObject.SetActive(true);
            }

        }


    }

    //Settings och Credits:
    public void GeneralIndexButton(int index)
    {
        SetMenusInactive();
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

    public void Quit(bool sure)
    {
        if(!sure)
        {
            menus[9].gameObject.SetActive(true);
        }
        else if(sure)
        {
            Application.Quit();
        }
    }

    public void MainMenuButton()
    {
        paused = false;
        inGame = false;
        SetMenusInactive();
        menus[0].gameObject.SetActive(true);
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

    //Backar tillbaka till login-scene.
    public void ChangeUser()
    {
        menuSound.Stop();
        SetMenusInactive();
        xmlScript.ActivatePanel(true);
        SceneManager.LoadScene("LogInScene");
    }

    public void Restart()
    {
        SetMenusInactive();
        if (Time.timeScale == 0)
            Time.timeScale = 1;
        SceneManager.LoadScene(currentGameScene);
    }

    public void SetMainMenu(bool enabled)
    {
        menus[0].SetActive(enabled);
    }

    //Next Level-knappen i Winscreen anropar följande metod.
    //Metoden adderar 1 till den scen vi befinner oss i just nu för att sedan ladda nästa scen.
    public void NextLevel()
    {
        string lastChar = currentGameScene.Substring(currentGameScene.Length - 1, 1);
        int nextLevel = int.Parse(lastChar) + 1;
        string nextLevelString = nextLevel.ToString();
        currentGameScene = currentGameScene.TrimEnd(currentGameScene[currentGameScene.Length -1]);
        string nextGameScene = currentGameScene + nextLevelString;
        SetMenusInactive();
        SceneManager.LoadScene(nextGameScene);
    }


}

