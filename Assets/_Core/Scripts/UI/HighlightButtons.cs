using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightButtons : MonoBehaviour
{
    XmlScript xmlScript;

    public void Highlight(GameObject button)
    {
        xmlScript = GameObject.FindGameObjectWithTag("Canvas").GetComponent<XmlScript>();
        if (button.name == "LanguageButtons")
        {
            button = button.transform.GetChild(xmlScript.currentLanguageIndex).gameObject;
        }
        for (int i = 0; i < GameObject.FindGameObjectWithTag("LanguageButtons").transform.childCount; i++)
        {
            GameObject.FindGameObjectWithTag("LanguageButtons").transform.GetChild(i).GetChild(0).gameObject.SetActive(false);
        }
        button.transform.GetChild(0).gameObject.SetActive(true);
    }
}
