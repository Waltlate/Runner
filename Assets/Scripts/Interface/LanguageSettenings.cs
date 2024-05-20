using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class LanguageSettenings : MonoBehaviour
{
    private string json;
    public static LanguageSystem ls = new LanguageSystem();
    public TMP_Dropdown dropdown;

    void Awake()
    {
        //14:00 haskey

        //PlayerPrefs.SetString("Languages", "ENG");
        LanguageSystemLoad();
    }

    public void Switch()
    {
        if (dropdown.options[dropdown.value].text == "ENG")
        {
            PlayerPrefs.SetString("Languages", "ENG");
        }
        if (dropdown.options[dropdown.value].text == "RU")
        {
            PlayerPrefs.SetString("Languages", "RU");
        }
        LanguageSystemLoad();
    }

    void LanguageSystemLoad()
    {

    #if UNITY_ANDROID && !UNITY_EDITOR
        string path = Path.Combine(Application.streamingAssetsPath + "/Languages/" + PlayerPrefs.GetString("Languages", "ENG") + ".json");
        WWW reader = new WWW(path);
        //while (!reader.isDone) {}
        json = reader.text;
    #endif

    #if UNITY_EDITOR
        json = File.ReadAllText(Application.streamingAssetsPath + "/Languages/" + PlayerPrefs.GetString("Languages", "ENG") + ".json");
        ls = JsonUtility.FromJson<LanguageSystem>(json);
    #endif

        if (MenuText.instance)
        {
            MenuText.instance.ChangeLanguageAndRefresh();
        }
        if (SetteningsText.instance)
        {
            SetteningsText.instance.ChangeLanguageAndRefresh();
        }
        if (DisplayText.instance)
        {
            DisplayText.instance.ChangeLanguageAndRefresh();
        }
        if (PauseText.instance)
        {
            PauseText.instance.ChangeLanguageAndRefresh();
        }
        if (HeroesText.instance)
        {
            HeroesText.instance.ChangeLanguageAndRefresh();
        }
        //if (PlayerParameters.instance)
        //{
        //    if (PlayerParameters.archer.ClassName == "Warrior")
        //    {
        //        PlayerParameters.archer.Description = ls.descriptionWarrior;
        //    }
        //    if (PlayerParameters.archer.ClassName == "Archer")
        //    {
        //        PlayerParameters.archer.Description = ls.descriptionArcher;
        //    }
        //}
    }
}

public class LanguageSystem
{
    public string[] greatings = new string[5];
    public string enemyText;
    public string enemyFlyText;
    public string coinText;
    public string trapText;
    public string perkText;

    public string play;
    public string heroes;
    public string settenigns;
    public string exit;

    public string back;
    public string levelUp;
    public string hero;
    public string stats;
    public string weapon;

    public string startTutorial;

    public string pause;
    public string continueBtn;
    public string restart;

    public string level;
    public string hp;
    public string health;
    public string damage;
    public string speed;
    public string distance;

    public string descriptionWarrior;
    public string descriptionArcher;

    public string coins;
    public string score;
    public string bestScore;
}