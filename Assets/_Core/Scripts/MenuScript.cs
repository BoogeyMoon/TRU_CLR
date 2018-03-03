
//Meny-script som hanterar alla menyer och knappar.
//Skapat av Moa Lindgren.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* En hel del ändringar har gjorts i unity-hierarkin. Därför fungerar basically ingenting atm. 
 * Behöver fixa alla index för att fixa problemet*/


public class MenuScript : MonoBehaviour
{
    GameObject Panel,               //  
               MainMenu,            //  <- Huvudmeny 
            
               LoadMenu,            //
               SettingsMenu,        //  
               ConfirmQuit,         //  <- Panel 
               CreditsMenu,         //
               Background,          //  <- borde gå att ändra det här på något sätt. vad tillhör bakgrunden? panel eller huvudmeny?

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

    //Sätter alla värden
    void Start()
    {
        Panel = transform.GetChild(0).gameObject;
        gameScene = "Moa_DemoScene"; //Ändra det här till den färdiga spel-scenen.
        inGame = false;

        //Listan nedan måste fixas. De är inte placerade rätt. Behövs alla dessa ens? Lös problemet med Panel
        Menus = new List<GameObject>() { MainMenu, LoadMenu, SettingsMenu,  CreditsMenu, ConfirmQuit, PauseMenu };

        for (int i = 0; i < Menus.Count; i++)
        {
            Menus[i] = Panel.transform.GetChild(i).gameObject;
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
                print("hej");
                Panel.SetActive(true);
                Menus[5].SetActive(true);
            }
            else
            {
                Panel.SetActive(false);
                Menus[5].SetActive(false);
            }
        }
    }

    //Följande metod är kopplat till OnClick() på knapparna i menyn i Unity.
    //Index är specificerat hos vardera knapp i Unity. (*******Och det behöver som sagt fixas!!*****)
    public void ClickButtons(int index)
    {
        Menus[0].SetActive(false);
        Menus[1].SetActive(false);

        switch (index)
        {
            //New Game
            case 0:
                inGame = true;
                Panel.SetActive(false);
                SceneManager.LoadScene(gameScene); 
                break;

            //Load Game
            case 1:
                Menus[1].SetActive(true);

                //Det här bör ändras till att den hämtar värde från vår spar-funktion.
                numberOfSaves = 5;
                if(numberOfSaves == 0)
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

            //Settings
            case 2:
                Menus[2].SetActive(true);
                break;

            //Credits
            case 3:
                Menus[3].SetActive(true);
                break;

            //Confirm Quit
            case 4:
                Menus[4].SetActive(true);
                break;

            //Back, till MainMenu on inGame är false, och till Pausmenyn om inGame är true.
            case 5:
                //Funderingar: En if-sats som kollar om man är in-game eller inte. (Do you want to save current game?)
                //             En till if-sats som kollar om man har sparat eller inte ??? Så det inte dyker upp om man redan har sparat. 
                for (int i = 0; i < Menus.Count; i++)
                {
                    Menus[i].SetActive(false);
                }
                Menus[0].SetActive(true);
             //   Menus[5].SetActive(true);
                break;

            //Yes-knapp i ConfirmQuit.
            case 6:
                Application.Quit();
                break;
        }
    }
}
