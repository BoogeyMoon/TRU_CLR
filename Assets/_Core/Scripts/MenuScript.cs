
//Meny-script som hanterar alla menyer och knappar.
//Skapat av Moa Lindgren.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    GameObject MainMenu,            //
               LoadMenu,            //
               SettingsMenu,        //  <- Huvudmeny
               ConfirmQuit,         //
               CreditsMenu,         //
               Background,          //

               PauseMenu;           //  <- Pausmeny

    [SerializeField]
    List<GameObject> Menus;

    int numberOfSaves;
    string gameScene;

    bool paused, 
         inGame;

    //Spara Canvas till nästa scen.
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    //Sätt alla värden
    void Start()
    {
        gameScene = "Moa_DemoScene"; //Ändra det här till den färdiga spel-scenen.
        inGame = false;
        Menus = new List<GameObject>() { Background, LoadMenu, SettingsMenu, CreditsMenu, ConfirmQuit, MainMenu, PauseMenu };

        for (int i = 0; i < Menus.Count; i++)
        {
            Menus[i] = transform.GetChild(i).gameObject;
        }
    }

    //Öppna och stänga pausmeny.
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && inGame)
        {
            paused = !paused;

            if(paused)
            {
                Menus[6].SetActive(true);
            }
            else
            {
                Menus[6].SetActive(false);
            }
        }
    }

    //Följande metod är kopplat till OnClick() på knapparna i menyn i Unity.
    //Index är specificerat hos vardera knapp i Unity. 
    public void ClickButtons(int index)
    {
        Menus[5].SetActive(false);

        switch (index)
        {
            //New Game
            case 0:
                inGame = true;
                Menus[0].SetActive(false);
                SceneManager.LoadScene(gameScene); 

                break;

            //Load Game
            case 1:

                Menus[1].SetActive(true);

                //Det här bör ändras till att den hämtar värde från vår spar-funktion.
                numberOfSaves = 5;
                for (int i = 0; i < numberOfSaves; i++)
                {
                    Menus[1].transform.GetChild(i).gameObject.SetActive(true);
                }
                break;

            //Settings
            case 2:
                Menus[2].SetActive(true);
                break;

            //Credits
            case 3:
                Menus[3].SetActive(true);
                break;

            //Quit
            case 4:
                Menus[4].SetActive(true);
                break;

            //Back (till MainMenu)
            case 5:
                //Funderingar: En if-sats som kollar om man är in-game eller inte. (Do you want to save current game?)
                //             En till if-sats som kollar om man har sparat eller inte ??? Så det inte dyker upp om man redan har sparat. 
                for (int i = 0; i < Menus.Count; i++)
                {
                    Menus[i].SetActive(false);
                }
                Menus[0].SetActive(true);
                Menus[5].SetActive(true);
                break;

            //Yes-knapp i ConfirmQuit.
            case 6:
                Application.Quit();
                break;
        }
    }
}
