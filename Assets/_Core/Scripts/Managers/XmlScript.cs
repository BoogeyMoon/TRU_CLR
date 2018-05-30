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
    public bool changeLanguage;


    [SerializeField]
    GameObject contentObject, userButtonPrefab, inlogObject, registerAccountObject, loginPage, eventSystem;

    List<int> scoreList;
    List<string> languages;
    [SerializeField]
    List<GameObject> currentTextAssets;
    [SerializeField]
    List<GameObject> textAssetsMainMenu, textAssetsInlog, textAssetsLevel1;
    GameObject[] textComponentsInlog, textComponentsMainMenu, textComponentsLevel1;
    [SerializeField]
    GameObject[] currentComponents;

    public List<int> ScoreList
    {
        get { return scoreList; }
    }
    TextAsset path, languagesPath;
    [SerializeField]
    Text inputText, charIssue_InfoText, takenName_InfoText, space_InfoText;
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
        textAssetsInlog = new List<GameObject>();
        textAssetsMainMenu = new List<GameObject>();
        textAssetsLevel1 = new List<GameObject>();
        currentLanguageIndex = 0;
        changeLanguage = false;
        DontDestroyOnLoad(transform.gameObject);
        DontDestroyOnLoad(eventSystem);
        numberOfLevels = 5;
        currentMenu = "Inlog";
        LoadTexts();
        SetUpPlayerXML();
        InlogPage();
        takenName_InfoText.gameObject.SetActive(false);
        space_InfoText.gameObject.SetActive(false);
        charIssue_InfoText.gameObject.SetActive(false);
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
            charIssue_InfoText.gameObject.SetActive(true);
        }
        else if (usernameInput.Contains(" "))
        {
            validName = false;
            space_InfoText.gameObject.SetActive(true);
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
                            takenName_InfoText.gameObject.SetActive(true);
                        }
                    }
                }
            }
        }
        //Om användarnamnet är godkänt:
        if (validName)
        {
            takenName_InfoText.gameObject.SetActive(false);
            space_InfoText.gameObject.SetActive(false);
            charIssue_InfoText.gameObject.SetActive(false);
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
        takenName_InfoText.gameObject.SetActive(false);
        space_InfoText.gameObject.SetActive(false);
        charIssue_InfoText.gameObject.SetActive(false);
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
    /*Denna metod ska ske varje gång spelaren når följande scener första gången:
     *                                          - InLogScene (dvs. i Start() i XmlScript)
     *                                          - MenuScene (dvs. i Start() i MenuScript)
     *                                          - Level1 (i slutet av LoadingScreen() i MenuScript.
     */
    public void LoadTexts()
    {

        currentComponents = GameObject.FindGameObjectsWithTag("TextAsset");

            switch (currentMenu)
            {
                case "Inlog":
                textAssetsInlog.Clear();
                    foreach (GameObject texts in currentComponents)
                    {
                        textAssetsInlog.Add(texts);
                    }
                    SetLanguage(currentLanguageIndex);
                    return;
                case "MainMenu":
                textAssetsMainMenu.Clear();
                    foreach (GameObject texts in currentComponents)
                    {

                        textAssetsMainMenu.Add(texts);
                    }
                    SetLanguage(currentLanguageIndex);
                    return;

                case "Level1":
                textAssetsLevel1.Clear();
                foreach (GameObject texts in currentComponents)
                    {
                        textAssetsLevel1.Add(texts);
                    }
                    SetLanguage(currentLanguageIndex);
                    return;
            }

       
    }
    //Följande metod är kopplad till settings i MenuScene.
    //Den sparar in det språk som spelaren vill ha sparat på sin användarprofil.
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
        SetLanguage(currentLanguageIndex);
    }
    //Blir kallad på från Play-knappen när man har klickat på en användare i InLogScene.
    //Kollar vilket språk spelaren har sparat på sin användare och tar det språk-indexet och skickar till SetLanguage-metoden, som i sin tur ändrar språk.
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
        SetLanguage(currentLanguageIndex);
    }
    //Följande metod är det som faktiskt sätter språket på alla textkomponenter.
    public void SetLanguage(int languageIndex)
    {
        currentLanguageIndex = languageIndex;
        languageDoc = new XmlDocument();

        //Kollar om xml-dokumentet finns med i projektet.
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

        GetCurrentTexts();

        foreach (XmlNode menu in languagesNodeList)
        {
            foreach (XmlNode language in menu)
            {
                if (language.Name == languages[languageIndex])
                {
                    for (int i = 0; i < language.Attributes.Count; i++)
                    {
                        for (int y = 0; y < currentTextAssets.Count; y++)
                        {
                        
                            if (language.Attributes[i].Name == currentTextAssets[y].name)
                            {
                                Text temp = currentTextAssets[y].GetComponent<Text>();

                                if (temp == null)
                                {
                                    TextMesh tempText = currentTextAssets[y].GetComponent<TextMesh>();
                                    tempText.text = language.Attributes[i].Value;
                                }
                                else
                                {
                                    temp.text = language.Attributes[i].Value;
                                }


                            }
                        }
                    }
                }
            }
        }

    }
    void GetCurrentTexts()
    {
        switch (currentMenu)
        {
            case "Inlog":

                currentTextAssets = textAssetsInlog;
                return;
            case "MainMenu":
                currentTextAssets = textAssetsMainMenu;
                return;
            case "Level1":
                currentTextAssets = textAssetsLevel1;
                for (int i = 0; i < textAssetsMainMenu.Count; i++)
                {
                    currentTextAssets.Add(textAssetsMainMenu[i]);
                }
                return;
        }
    }
}
