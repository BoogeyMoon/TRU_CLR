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
    int counter, score;
    public int numberOfLevels;
    bool validName;

    [SerializeField]
    GameObject contentObject, userButtonPrefab, inlogObject, registerAccountObject, loginPage, eventSystem;

    List<int> scoreList;
    public List<int> ScoreList
    {
        get { return scoreList; }
    }
    TextAsset path;
    string currentPlayer;

    XmlDocument doc;
    XmlElement player, username, level;
    XmlWriter writer;
    XmlNodeList playerNodeList;

    void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
        DontDestroyOnLoad(eventSystem);
        numberOfLevels = 5;
        SetUpXML();
        InlogPage();
    }

    //Laddar och "plockar fram" samt sparar xml-dokumentet som ska användas för att hämta eller registrera användare.
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
        XmlWriterSettings settings = new XmlWriterSettings();
        settings.Indent = true;
        using (writer = XmlWriter.Create(Application.persistentDataPath + "/Players.xml", settings))
        {
            doc.Save(writer);
        }
    }

    //Om spelaren går in i ingloggnings-menyn så gör följande metod att samtliga användare som registrerats visas i en lista.
    public void InlogPage()
    {
        registerAccountObject.SetActive(false);
        inlogObject.SetActive(true);

        playerNodeList = doc.GetElementsByTagName("player");

        foreach (XmlNode player in playerNodeList)
        {
            if (player.Name == "player")
            {
                foreach (XmlNode username in player)
                {
                    if (username.Name == "username")
                    {
                        counter++;
                        GameObject user = Instantiate(userButtonPrefab) as GameObject;
                        user.SetActive(true);
                        user.transform.SetParent(contentObject.transform, false);
                        user.GetComponentInChildren<Text>().text = username.InnerText;
                    }
                }
            }
        }
    }

    //Vid klick på "Register Account" så tar följande metod in den text som skrivits i inputField och går igenom ifall det är ett godkänt namn att registrera.
    public void RegisterAccount(Text inputField)
    {
        playerNodeList = doc.GetElementsByTagName("player");
        usernameInput = inputField.text;
        validName = true;

        if (playerNodeList.Count == 0)
        {
            if (usernameInput.Length < 3)
            {
                validName = false;
                print("Name too short");
            }

            if (usernameInput.Length > 20)
            {
                validName = false;
                print("Name too long");
            }
            else if (validName)
            {
                ValidName();
            }
        }
        else
        {
            foreach (XmlNode playerNode in playerNodeList)
            {
                if (playerNode.Name == "player")
                {
                    foreach (XmlNode usernameNode in playerNode)
                    {
                        if (usernameNode.Name == "username")
                        {
                            if (usernameNode.InnerText == usernameInput)
                            {
                                validName = false;
                                print("Name taken");
                            }
                            else if (usernameInput.Length < 3)
                            {
                                validName = false;
                                print("Name too short");
                            }
                            else if (usernameInput.Length > 20)
                            {
                                validName = false;
                                print("Name too long");
                            }

                        }
                    }

                }
            }
            if (validName)
            {
                ValidName();
            }
        }


    }

    //Om namnet som ska registreras är godkänt så skickas det till följande metod som alltså registrerar namnet i xml-dokumentet.
    void ValidName()
    {
        player = doc.CreateElement("player");
        username = doc.CreateElement("username");
        username.InnerText = usernameInput;
        player.AppendChild(username);
        for (int i = 0; i < numberOfLevels; i++)
        {
            level = doc.CreateElement("level_" + i);
            level.SetAttribute("score", "0");
            level.SetAttribute("grade", "0");
            username.AppendChild(level);
        }
        doc.DocumentElement.AppendChild(player);

        using (writer)
        {
            doc.Save(filePath);
        }
        InlogPage();
    }

    //Om man går från ingloggnings-menyn till "registrera konto"-menyn så sätts menyerna inaktiva,
    //samt alla användares knappar förstörs (så nya kan skapas om användar-listan uppdateras).
    public void RegisterAccountPage()
    {
        for (int i = 0; i < playerNodeList.Count; i++)
        {
            Destroy(contentObject.transform.GetChild(i).gameObject);
        }
        inlogObject.SetActive(false);
        registerAccountObject.SetActive(true);
    }

    public void GetStats(string currentPlayer)
    {
        scoreList = new List<int>();
        this.currentPlayer = currentPlayer;
        loginPage.SetActive(false);
        foreach (XmlNode player in playerNodeList)
        {

            if (player.FirstChild.InnerText == currentPlayer)
            {

                foreach (XmlNode level in player.FirstChild)
                {
                    for (int i = 0; i < numberOfLevels; i++)
                    {
                        if (level.Name == "level_" + i)
                        {
                            string tempScore = level.Attributes[0].Value;
                            score = int.Parse(tempScore);
                            scoreList.Add(score);
                        }
                    }
                }
            }
        }
    }

    public void ChangeStats(int levelNumber, int score, int grade)
    {
        foreach (XmlNode player in playerNodeList)
        {
            if (player.FirstChild.InnerText == currentPlayer)
            {
                foreach (XmlNode level in player.FirstChild)
                {
                    if (level.Name == "level_" + levelNumber)
                    {
                        level.Attributes[0].Value = score.ToString();
                        level.Attributes[1].Value = grade.ToString();
                        scoreList[levelNumber] = score;
                        using (writer)
                        {
                            doc.Save(filePath);
                        }
                    }
                }
            }
        }
    }

    public int GetScore(int level) //Återlämnar poängen för en given level
    {
        foreach (XmlNode player in playerNodeList)
        {
            if (player.FirstChild.InnerText == currentPlayer)
            {
                foreach (XmlNode lvl in player.FirstChild)
                {
                    if (lvl.Name == "level_" + level)
                    {
                        string scoreText = lvl.Attributes[0].Value;
                        return int.Parse(scoreText);
                    }
                }
            }
        }
        return -1;
    }
    public int GetGrade(int level)//Återlämnar betyget för en given level
    {
        foreach (XmlNode player in playerNodeList)
        {
            if (player.FirstChild.InnerText == currentPlayer)
            {
                foreach (XmlNode lvl in player.FirstChild)
                {
                    if (lvl.Name == "level_" + level)
                    {
                        string gradeText = lvl.Attributes[1].Value;
                        int grade = int.Parse(gradeText);

                        return grade;
                    }
                }
            }
        }
        return -1;
    }

    public void Quit()
    {
        Application.Quit();
    }
    public void ActivatePanel(bool enabled)
    {
        transform.GetChild(0).gameObject.SetActive(enabled);
    }
}
