
//Meny-script som hanterar alla menyer och knappar.
//Skapat av Moa.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    GameObject MainMenuButtons;
    Transform LoadMenu;
    GameObject SettingsMenu;
    [SerializeField]
    int numberOfSaves; //Det här ska vara ett värde som hämtas från eventuell lista av sparade spel-sessioner. Max antal (just nu) är 8 st.

    void Start()
    {
        MainMenuButtons = transform.GetChild(1).gameObject;
        LoadMenu = transform.GetChild(2);
        SettingsMenu = transform.GetChild(3).gameObject;
    }

    //Följande metod är kopplat till OnClick() på knapparna i menyn i Unity.
    //Indexet kommer från vilken knapp användaren trycker på i spelet.
    public void ClickButtons(int index)
    {
        switch(index)
        {
            //ladda ett nytt spel
            case 0:
                SceneManager.LoadScene("GameScene");
                break;

            //öppnar en ny meny med olika sparade spel-sessioner
            case 1:
                MainMenuButtons.SetActive(false);
                for(int i = 0; i < numberOfSaves; i++)
                {
                    LoadMenu.GetChild(i).gameObject.SetActive(true);
                }
                break;

            //öppna en ny meny med inställningar
            case 2:
                MainMenuButtons.SetActive(false);
                SettingsMenu.SetActive(true);
                break;

            //öppna en ny "meny" med lista på alla som gjort spelet
            case 3:
                print("credits");
                MainMenuButtons.SetActive(false);
                break;

            //öppna en "are you sure you want to quit?" ruta.
            case 4:
                print("quit");
                break;
        }
    }

    public void LoadScene(int index)
    {

    }
}
