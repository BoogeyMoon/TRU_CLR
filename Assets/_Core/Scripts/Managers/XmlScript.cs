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
    string filePath, usernameInput, currentPlayer, languagesfilePath, currentLanguage;
    int counter, score;
    public int currentLanguageIndex;
    public int numberOfLevels;
    public string currentMenu;
    bool validName;

    [SerializeField]
    GameObject contentObject, userButtonPrefab, inlogObject, registerAccountObject, loginPage, eventSystem, languageButtonsParent;

    List<int> scoreList;
    List<string> languages;
    [SerializeField]
    List<Text> textAssetsInlog, textAssetsMainMenu;
    GameObject[] textComponentsInlog, textComponentsMainMenu;

    public List<int> ScoreList
    {
        get { return scoreList; }
    }
    TextAsset path, languagesPath;
    [SerializeField]
    Text infoText, inputText;
    [SerializeField]
    Font textFont;


    XmlDocument playerDoc, languageDoc;
    XmlElement player, username, level, language;
    XmlWriter writer, languageWriter;
    XmlNodeList playerNodeList, languagesNodeList;


    void Start()
    {
        if (GameObject.FindGameObjectsWithTag("Canvas").Length > 1)
        {
            Destroy(gameObject);
        }
        languages = new List<string> { "English", "German", "Japanese", "Russian", "Spanish", "Swedish" };
        textAssetsInlog = new List<Text>();
        textAssetsMainMenu = new List<Text>();
        currentLanguageIndex = 0;

        DontDestroyOnLoad(transform.gameObject);
        DontDestroyOnLoad(eventSystem);
        numberOfLevels = 5;
        LoadTexts("Inlog");
        SetUpPlayerXML();
        InlogPage();
    }

    //Laddar och "plockar fram" samt sparar xml-dokumentet som ska användas för att hämta eller registrera användare.
    void SetUpPlayerXML()
    {
        playerDoc = new XmlDocument();
        if (File.Exists(Application.persistentDataPath + "/Players.xml"))
        {
            playerDoc.Load(Application.persistentDataPath + "/Players.xml");
        }
        else
        {
            filePath = Application.dataPath + "/Resources/Players.xml";
            path = Resources.Load("Players") as TextAsset;
            playerDoc.LoadXml(path.text);
        }
        filePath = Application.persistentDataPath + "/Players.xml";
        XmlWriterSettings settings = new XmlWriterSettings();
        settings.Indent = true;
        using (writer = XmlWriter.Create(Application.persistentDataPath + "/Players.xml", settings))
        {
            playerDoc.Save(writer);
        }
        print(filePath);
    }

    //Om spelaren går in i ingloggnings-menyn så gör följande metod att samtliga användare som registrerats visas i en lista.
    public void InlogPage()
    {
        registerAccountObject.SetActive(false);
        inlogObject.SetActive(true);
        playerNodeList = playerDoc.GetElementsByTagName("player");

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
    //Vid klick på "Create Account" så tar följande metod in den text som skrivits i inputField och går igenom ifall det är ett godkänt namn att skapa.
    public void CheckIfValid(InputField inputField)
    {
        playerNodeList = playerDoc.GetElementsByTagName("player");
        usernameInput = inputField.text;

        //Sätter validName till true för att ha en utgångspunkt, 
        //men efter kommande if-satser kan den sättas till false, om inte så är användarnamnet godkänt.
        validName = true;

        //Om spelaren skrivit in ett användarnamn som har färre än 3 eller längre än 20 karaktärer:
        if (usernameInput.Length < 3 || usernameInput.Length > 20)
        {
            validName = false;
            infoText.text = "Username must be within 3 to 20 characters long";
            infoText.gameObject.SetActive(true);
        }
        else if (usernameInput.Contains(" "))
        {
            validName = false;
            infoText.text = "Username can't contain SPACE";
            infoText.gameObject.SetActive(true);
        }
        //Om användarnamnet har godkänd längd men är inte det första registrerade kontot, så kollar följande så det inte redan finns ett konto med samma namn:
        else if (playerNodeList.Count > 0)
        {
            foreach (XmlNode playerNode in playerNodeList)
            {
                if (playerNode.Name == "player")
                {
                    foreach (XmlNode usernameNode in playerNode)
                    {
                        if (usernameNode.Name == "username" && usernameNode.InnerText == usernameInput)
                        {
                            validName = false;
                            infoText.text = "Username taken";
                            infoText.gameObject.SetActive(true);
                        }
                    }
                }
            }
        }
        //Om användarnamnet är godkänt:
        if (validName)
        {
            infoText.gameObject.SetActive(false);
            CreateAccount();
            inputField.text = "";
        }
    }
    //Om namnet som ska registreras är godkänt så skickas det till följande metod som alltså skapar kontot i xml-dokumentet.
    void CreateAccount()
    {
        player = playerDoc.CreateElement("player");
        username = playerDoc.CreateElement("username");
        username.InnerText = usernameInput;
        username.SetAttribute("language", languages[currentLanguageIndex]);
        player.AppendChild(username);

        for (int i = 0; i < numberOfLevels; i++)
        {
            level = playerDoc.CreateElement("level_" + i);
            level.SetAttribute("score", "0");
            level.SetAttribute("grade", "0");
            username.AppendChild(level);
        }
        playerDoc.DocumentElement.AppendChild(player);

        using (writer)
        {
            playerDoc.Save(filePath);
        }
        InlogPage();
    }
    //Om man går från ingloggnings-menyn till "skapa konto"-menyn så sätts menyerna inaktiva,
    //samt alla "konto-knappar" i listan förstörs (så nya kan skapas om listan ändras).
    public void RegisterAccountPage()
    {
        for (int i = 0; i < playerNodeList.Count; i++)
        {
            Destroy(contentObject.transform.GetChild(i).gameObject);
        }

        inlogObject.SetActive(false);
        registerAccountObject.SetActive(true);
    }
    //Sätter info-texten inaktiv om något nytt skrivs i inputfield. Anropas från komponenten InputField (OnValueChanged()).
    public void InfoTextInActive()
    {
        infoText.gameObject.SetActive(false);
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
                            playerDoc.Save(filePath);
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
    public int GetGrade(int level) //Återlämnar betyget för en given level
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
    public void Quit() // Avslutar spelet
    {
        Application.Quit();
    }
    public void ActivatePanel(bool enabled) //Sätter på och stänger av bakgrundsbilden
    {
        for (int i = 0; i < 2; i++)
        {
            transform.GetChild(i).gameObject.SetActive(enabled);
        }


    }

    //Sätter in alla textkomponenter i en lista så att det senare blir lättare att ändra texten på dom alla i ChangeLanguage metoden.
    public void LoadTexts(string menu)
    {
        //textAssets.Clear();
        currentMenu = menu;
        if (menu == "Inlog")
        {
            textComponentsInlog = GameObject.FindGameObjectsWithTag("TextAsset");

            foreach (GameObject texts in textComponentsInlog)
            {
                textAssetsInlog.Add(texts.GetComponent<Text>());
            }
        }
        if (menu == "MainMenu")
        {
            textComponentsMainMenu = GameObject.FindGameObjectsWithTag("TextAsset");

            foreach (GameObject texts in textComponentsMainMenu)
            {
                textAssetsMainMenu.Add(texts.GetComponent<Text>());
            }
        }

        ChangeLanguage(currentLanguageIndex);
        //ChangeHighlight();
    }
    //Följande metod är kopplad till settings.
    //Den sparar in det språk som spelaren vill ha sparat på sitt konto.
    public void SaveLanguageSettings(int languageIndex)
    {
        currentLanguageIndex = languageIndex;

        foreach (XmlNode player in playerNodeList)
        {
            if (player.FirstChild.InnerText == currentPlayer)
            {
                player.FirstChild.Attributes[0].Value = languages[languageIndex];

                using (writer)
                {
                    playerDoc.Save(filePath);
                }
            }
        }
        ChangeLanguage(currentLanguageIndex);

    }
    public void CheckLanguage(string currentPlayer)
    {
        foreach (XmlNode player in playerNodeList)
        {
            if (player.FirstChild.InnerText == currentPlayer)
            {
                currentLanguage = player.FirstChild.Attributes[0].Value;
            }
        }
        for (int i = 0; i < languages.Count; i++)
        {
            if (languages[i] == currentLanguage)
            {
                currentLanguageIndex = i;
            }
        }
        ChangeLanguage(currentLanguageIndex);
        //ChangeHighlight();
    }

    public void ChangeHighlight()
    {
        if (currentMenu == "Inlog")
        {
            languageButtonsParent = transform.GetChild(1).GetChild(2).gameObject;
        }
        if (currentMenu == "MainMenu")
        {
            languageButtonsParent = GameObject.FindGameObjectWithTag("MenuCanvas").transform.GetChild(3).GetChild(2).gameObject;
        }
        for (int i = 0; i < languageButtonsParent.transform.childCount; i++)
        {
            languageButtonsParent.transform.GetChild(i).GetChild(0).gameObject.SetActive(false);
            if (i == currentLanguageIndex)
            {
                languageButtonsParent.transform.GetChild(i).GetChild(0).gameObject.SetActive(true);
            }
        }
    }
    //Följande metod är det som faktiskt ändrar språket på alla textkomponenter.
    public void ChangeLanguage(int languageIndex)
    {

        currentLanguageIndex = languageIndex;
        print(currentMenu);
        languageDoc = new XmlDocument();

        if (File.Exists(Application.persistentDataPath + "/Languages.xml"))
        {
            languageDoc.Load(Application.persistentDataPath + "/Languages.xml");
        }
        else
        {
            languagesfilePath = Application.dataPath + "/Resources/Languages.xml";
            languagesPath = Resources.Load("Languages") as TextAsset;
            languageDoc.LoadXml(languagesPath.text);
        }

        languagesNodeList = languageDoc.GetElementsByTagName(currentMenu);

        foreach (XmlNode menu in languagesNodeList)
        {

            foreach (XmlNode language in menu)
            {
                if (language.Name == languages[languageIndex])
                {
                    for (int i = 0; i < language.Attributes.Count; i++)
                    {
                        if (currentMenu == "Inlog")
                        {
                            for (int y = 0; y < textAssetsInlog.Count; y++)
                            {
                                if (language.Attributes[i].Name == textAssetsInlog[y].name)
                                {
                                    textAssetsInlog[y].text = language.Attributes[i].Value;
                                }
                            }
                        }
                        if (currentMenu == "MainMenu")
                        {
                            for (int y = 0; y < textAssetsMainMenu.Count; y++)
                            {
                                if (language.Attributes[i].Name == textAssetsMainMenu[y].name)
                                {
                                    print(language.Attributes[i].Value);
                                    textAssetsMainMenu[y].text = language.Attributes[i].Value;
                                }
                            }
                        }

                    }
                }
            }
        }

        ChangeHighlight();
    }
}
