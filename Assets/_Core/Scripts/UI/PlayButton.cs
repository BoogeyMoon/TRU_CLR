using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayButton : MonoBehaviour
{
    [SerializeField]
    Text userName;
    string currentPlayer;
    XmlScript xmlScript;
    GameObject panel;

    void Awake()
    {
        panel = GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(0).gameObject;
        xmlScript = GameObject.FindGameObjectWithTag("Canvas").GetComponent<XmlScript>();
        GetComponent<Button>().onClick.AddListener(OnClick);
    }
    void OnClick()
    {
        GameObject userButton = transform.parent.gameObject;
        currentPlayer = userButton.GetComponentInChildren<Text>().text;
        xmlScript.GetStats(currentPlayer);
        //panel.SetActive(false);
        xmlScript.ActivatePanel(false);
        if (GameObject.FindGameObjectsWithTag("MenuCanvas").Length != 0)
            GameObject.FindGameObjectWithTag("MenuCanvas").GetComponent<MenuScript>().SetMainMenu(true);
        SceneManager.LoadScene("MenuScene");
        xmlScript.currentMenu = "MainMenu";
        xmlScript.CheckLanguage(currentPlayer);
    }
}
