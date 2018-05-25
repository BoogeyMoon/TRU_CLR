using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightButtons : MonoBehaviour
{
    XmlScript xmlScript;
    [SerializeField]
    GameObject parent;

    public void Highlight(int languageindex)
    {
        if(languageindex == -1)
        {
            xmlScript = GameObject.FindGameObjectWithTag("Canvas").GetComponent<XmlScript>();
            languageindex = xmlScript.currentLanguageIndex;
        }
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            parent.transform.GetChild(i).GetChild(0).gameObject.SetActive(false);
        }
        parent.transform.GetChild(languageindex).GetChild(0).gameObject.SetActive(true);
    }
}
