
//Meny-script som hanterar alla(?) menyer och knappar.
//Skapat av Moa.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    GameObject MainMenu;
    GameObject LoadMenu;
    GameObject SettingsMenu;
    GameObject ConfirmQuit;
    List<GameObject> Menus;
    int numberOfSaves;

    void Start()
    {
        //Blir det problem along the way så är det med största sannolikhet följande "satta värden" som ändrats i Unitys hierarki.
        MainMenu = transform.GetChild(1).gameObject;
        LoadMenu = transform.GetChild(2).gameObject;
        SettingsMenu = transform.GetChild(3).gameObject;
        ConfirmQuit = transform.GetChild(5).gameObject;

        Menus = new List<GameObject>() { MainMenu, LoadMenu, SettingsMenu, ConfirmQuit };
        
    }

    //Följande metod är kopplat till OnClick() på knapparna i menyn i Unity.
    //Index är specificerat hos vardera knapp i Unity. 
    public void ClickButtons(int index)
    {
        Menus[0].SetActive(false);

        switch (index)
        {
            //New Game
            case 0:
                SceneManager.LoadScene("DemoScene"); // Ändra det här till den färdiga spelscenen
                break;

            //Load Game
            case 1:

                //Det här bör ändras till att den hämtar värde från vår spar-funktion.
                numberOfSaves = 5;

                for(int i = 0; i < numberOfSaves; i++)
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
                print("credits");
                break;

            //Quit
            case 4:
                ConfirmQuit.SetActive(true);
                print("quit");
                break;
            
            //Back (till MainMenu)
            case 5:
                for(int i = 0; i < Menus.Count; i++)
                {
                    Menus[i].SetActive(false);
                }
                Menus[0].SetActive(true);
                break;
        }
    }
}
