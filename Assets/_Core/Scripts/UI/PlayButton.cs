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

    void Awake()
    {
        xmlScript = GameObject.FindGameObjectWithTag("Canvas").GetComponent<XmlScript>();
        GetComponent<Button>().onClick.AddListener(OnClick);
    }
    void OnClick()
    {
        GameObject userButton = transform.parent.gameObject;
        currentPlayer = userButton.GetComponentInChildren<Text>().text;
        print(currentPlayer);
        xmlScript.GetStats(currentPlayer);
        SceneManager.LoadScene("MenuScene");
    }
}
