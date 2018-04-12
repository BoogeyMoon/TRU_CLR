using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;
using UnityEngine.UI;

/*Sparfunktion med xml som stöd. Hämtar och sparar information från/till ett xml-dokument.
 * Scriptet hanterar:
 *          - Att hämta information ang. score hos användare från xml.
 *          - Registrering av nya användare. 
 * Skapat av Moa Lindgren med hjälp från Timmy Alvelöv samt Björn Andersson*/
    

public class XmlScript : MonoBehaviour
{
    string filePath, usernameInput;
    int counter, numberOfLevels, score;
    float offset = 2;

    [SerializeField]
    GameObject contentObject, userButtonPrefab, inlogObject, registerAccountObject;
    [SerializeField]
    List<string> players;
    [SerializeField]
    List<int> scoreList;
    TextAsset path;
    Button buttonPrefab;

    XmlDocument doc;
    XmlElement player, username, level;
    XmlWriter writer;
    XmlNodeList accounts;

    void Start()
    {
        players = new List<string>();
        scoreList = new List<int>();
        numberOfLevels = 3;
        SetUpXML();
        InlogPage();
    }

    void SetUpXML()
    {
        doc = new XmlDocument();
        if (File.Exists(Application.persistentDataPath + "/Players.xml"))
        {
            doc.Load(Application.persistentDataPath + "/Players.xml");
        }
        else
        {
            filePath = Application.dataPath + "/Resources/Players.xml";
            path = Resources.Load("Players") as TextAsset;
            doc.LoadXml(path.text);
        }
        filePath = Application.persistentDataPath + "/Players.xml";
        print(filePath);
        XmlWriterSettings settings = new XmlWriterSettings();
        settings.Indent = true;
        using (writer = XmlWriter.Create(Application.persistentDataPath + "/Players.xml", settings))
        {
            doc.Save(writer);
        }
    }

    public void RegisterAccount(Text inputField)
    {
        usernameInput = inputField.text;
        player = doc.CreateElement("player");
        username = doc.CreateElement("username");
        username.InnerText = usernameInput;
        player.AppendChild(username);
        for (int i = 0; i < numberOfLevels; i++)
        {
            int levelNumber = i + 1;
            level = doc.CreateElement("level_" + levelNumber);
            level.SetAttribute("score", "0");
            username.AppendChild(level);
        }
        doc.DocumentElement.AppendChild(player);

        using (writer)
        {
            doc.Save(filePath);
        }


    }

    public void InlogPage()
    {
        registerAccountObject.SetActive(false);
        inlogObject.SetActive(true);

        accounts = doc.GetElementsByTagName("player");

        foreach (XmlNode player in accounts)
        {
            if (player.Name == "player")
            {
                foreach (XmlNode username in player)
                {
                    if (username.Name == "username")
                    {
                        counter++;
                        players.Add(username.InnerText);
                        GameObject user = Instantiate(userButtonPrefab) as GameObject;
                        user.name = "butt #" + counter;
                        user.SetActive(true);
                        user.transform.SetParent(contentObject.transform, false);
                        user.GetComponentInChildren<Text>().text = username.InnerText;
                    }
                }
            }
        }
    }

    public void RegisterAccountPage()
    {
        for (int i = 0; i < accounts.Count; i++)
        {
            Destroy(contentObject.transform.GetChild(i).gameObject);
        }
        inlogObject.SetActive(false);
        registerAccountObject.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void GetStats(string currentPlayer)
    {
        foreach (XmlNode player in accounts)
        {
            if (player.FirstChild.InnerText == currentPlayer)
            {
                foreach (XmlNode level in player.FirstChild)
                {
                    //Hämtar inte levels här. Kan det ha att göra med att det är attributer inblandat?

                    //print(level.Name);
                    //string tempScore = level.Attributes[0].Value;
                    //score = int.Parse(tempScore);
                    //scoreList.Add(score);
                }
            }

        }

    }

}
