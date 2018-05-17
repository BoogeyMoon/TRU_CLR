using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Meny-script som hanterar alla menyer och knappar.
//Skapat av Moa Lindgren.
//Tillägg av Andreas de Freitas && Timmy Alvelöv
public class MenuScript : MonoBehaviour
{
    GameObject mainMenu, loadMenu, settingsMenu, confirmQuit, creditsMenu, pauseMenu, pausePanel, winScreen, loseScreen, areYouSure, loadingScreen;
    [SerializeField]
    GameObject eventSystem;
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
    AudioManager menuSound;

    //Spara Canvas till nästa scen.
    void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("MenuCanvas").Length > 1)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        xmlScript = GameObject.FindGameObjectWithTag("Canvas").GetComponent<XmlScript>();
        menuSound = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();

    }
    //Sätter alla värden
    void Start()
    {
        xmlScript.LoadTexts("MainMenu");
        inGame = false;
        menuSound.Play("S_TRU_CLR_Menu");
        menus = new List<GameObject>() { mainMenu, pausePanel, loadMenu, settingsMenu, creditsMenu, confirmQuit, pauseMenu, winScreen, loseScreen, areYouSure/*, loadingScreen*/ };
        for (int i = 0; i < menus.Count; i++)
        {
            menus[i] = transform.GetChild(i).gameObject;
            menus[i].SetActive(false);
        }
        menus[0].SetActive(true);

    }


    //Öppna och stänga pausmeny.
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape) && inGame)
        {
            if (!menus[7].activeInHierarchy && !menus[8].activeInHierarchy)
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
        menuSound.Stop("S_TRU_CLR_Menu");
        print("YOO!");
        StartCoroutine(LoadingScreen(currentGameScene));
        print("LOOO");
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
                for (int c = 0; c < levelParent.transform.GetChild(i).GetChild(j).childCount - 1; c++)
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

    public void ChangeLanguage(int languageIndex)

    {

        xmlScript.SaveLanguageSettings(languageIndex);

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
        if (!sure)
        {
            menus[9].gameObject.SetActive(true);
        }
        else if (sure)
        {
            Application.Quit();
        }
    }

    public void MainMenuButton()
    {
        paused = false;
        inGame = false;
        SetMenusInactive();
        menuSound.StopAll();
        StartCoroutine(LoadingScreen("MenuScene"));
        StartCoroutine(WaitForSceneLoad());
    }

    IEnumerator WaitForSceneLoad()
    {
        yield return new WaitForEndOfFrame();
        menus[0].gameObject.SetActive(true);
        menuSound.Play("S_TRU_CLR_Menu");
        Time.timeScale = 1;
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
        menuSound.Stop("S_TRU_CLR_Menu");
        SetMenusInactive();
        xmlScript.ActivatePanel(true);
        xmlScript.currentMenu = "Inlog";
        StartCoroutine(LoadingScreen("LogInScene"));
    }

    public void Restart()
    {
        SetMenusInactive();
        if (Time.timeScale == 0)
            Time.timeScale = 1;
        menuSound.StopAll();
        StartCoroutine(LoadingScreen(currentGameScene));
    }

    public void SetMainMenu(bool enabled)
    {
        menus[0].SetActive(enabled);
    }

    //Next Level-knappen i Winscreen anropar följande metod.
    //Metoden adderar ett index till buildsettings ordningen och går vidare till nästa scene
    public void NextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex != UnityEditor.EditorBuildSettings.scenes.Length - 1)
            LoadingScreen(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator LoadingScreen(string name)
    {
        //menus[10].SetActive(true);
        AsyncOperation async = SceneManager.LoadSceneAsync(name);
        while (!async.isDone)
        {
            yield return null;
        }
        //menus[10].SetActive(false);
    }

    IEnumerator LoadingScreen(int index)
    {
        //menus[10].SetActive(true);
        AsyncOperation async = SceneManager.LoadSceneAsync(index);
        while (!async.isDone)
        {
            yield return null;
        }
        //menus[10].SetActive(false);
    }

}

