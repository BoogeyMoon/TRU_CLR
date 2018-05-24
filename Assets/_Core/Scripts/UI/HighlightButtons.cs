using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightButtons : MonoBehaviour
{
    XmlScript xmlScript;
    GameObject defaultButton;

    public void Highlight(string button)
    {
        xmlScript = GameObject.FindGameObjectWithTag("Canvas").GetComponent<XmlScript>();

        for (int i = 0; i < GameObject.FindGameObjectWithTag("LanguageButtons").transform.childCount; i++)
        {
            GameObject.FindGameObjectWithTag("LanguageButtons").transform.GetChild(i).GetChild(0).gameObject.SetActive(false);
        }
        if (button == "LanguageButtons")
        {
            defaultButton = GameObject.Find(button).gameObject;
            defaultButton.SetActive(true);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}
