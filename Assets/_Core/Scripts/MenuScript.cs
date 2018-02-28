
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
    GameObject CreditsMenu;
    List<GameObject> Menus;
    int numberOfSaves;

    void Start()
    {
        //Blir det problem, along the way, så är det med största sannolikhet följande "satta värden" som ändrats i Unitys hierarki.
        MainMenu = transform.GetChild(1).gameObject;
        LoadMenu = transform.GetChild(2).gameObject;
        SettingsMenu = transform.GetChild(3).gameObject;
        CreditsMenu = transform.GetChild(4).gameObject;
        ConfirmQuit = transform.GetChild(5).gameObject;

        Menus = new List<GameObject>() { MainMenu, LoadMenu, SettingsMenu, CreditsMenu, ConfirmQuit };
        
    }

    //Följande metod är kopplat till OnClick() på knapparna i menyn i Unity.
    //Index är specificerat hos vardera knapp i Unity. 
    public void ClickButtons(int index)
    {
        MainMenu.SetActive(false);

        switch (index)
        {
            //New Game
            case 0:
                SceneManager.LoadScene("DemoScene"); // Ändra det här till den färdiga spelscenen
                break;

            //Load Game
            case 1:
                LoadMenu.SetActive(true);

                //Det här bör ändras till att den hämtar värde från vår spar-funktion.
                numberOfSaves = 5;
                for(int i = 0; i < numberOfSaves; i++)
                {
                    LoadMenu.transform.GetChild(i).gameObject.SetActive(true);
                }
                break;

            //Settings
            case 2:
                SettingsMenu.SetActive(true);
                break;

            //Credits
            case 3:
                CreditsMenu.SetActive(true);
                break;

            //Quit
            case 4:
                ConfirmQuit.SetActive(true);
                break;
            
            //Back (till MainMenu)
            case 5:
                for(int i = 0; i < Menus.Count; i++)
                {
                    Menus[i].SetActive(false);
                }
                MainMenu.SetActive(true);
                break;
            
            //Yes-knapp i ConfirmQuit.
            case 6:
                Application.Quit();
                break;
        }
    }
}
