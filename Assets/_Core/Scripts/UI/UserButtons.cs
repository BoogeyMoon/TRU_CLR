using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserButtons : MonoBehaviour
{
    [SerializeField]
    Text username;
    string currentPlayer;
    XmlScript xmlScript;

    void Awake()
    {
        xmlScript = GameObject.FindGameObjectWithTag("Canvas").GetComponent<XmlScript>();
        GetComponent<Button>().onClick.AddListener(GetName);
    }
    void GetName()
    {
        currentPlayer = username.text;
        xmlScript.GetStats(currentPlayer);
    }

}
