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
 *          
 * Skapat av Moa Lindgren med hjälp från Timmy Alvelöv samt Björn Andersson*/
    

public class XmlScript : MonoBehaviour
{
    string filePath, usernameInput;
    int counter, numberOfLevels, score;
    float offset;

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
        offset = 2;
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
            level = doc.CreateElement("level_" + i);
            level.SetAttribute("score", "0");
            level.SetAttribute("grade", "");
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
                    for(int i = 0; i < numberOfLevels; i++)
                    {
                        if (level.Name == "level_" + i)
                        {
                            string tempScore = level.Attributes[0].Value;
                            score = int.Parse(tempScore);
                            scoreList.Add(score);
                            print("level_" + i + ": " + score);
                        }
                    }
                }
            }
        }
    }
    void ChangeStats(string currentPlayer, int levelNumber, int score, int grade)
    {
        foreach (XmlNode player in accounts)
        {
            if (player.FirstChild.InnerText == currentPlayer)
            {
                foreach (XmlNode level in player.FirstChild)
                {
                    if(level.Name == "level_" + levelNumber)
                    {
                        level.Attributes[0].Value = score.ToString();
                        level.Attributes[1].Value = grade.ToString();
                        using (writer)
                        {
                            doc.Save(filePath);
                        }
                    }
                }
            }
        }
    }

    public void TempChangeStats(string currentPlayer)
    {
        int level = 0;
        int score = 300;
        int grade = 1;
        ChangeStats(currentPlayer, level, score, grade);
    }

}
