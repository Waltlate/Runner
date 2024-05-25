using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.Networking;

public class LanguageSettenings : MonoBehaviour
{
    private string json;
    public static LanguageSystem ls = new LanguageSystem();
    public TMP_Dropdown dropdown;
    public TextMeshProUGUI bestScoreLabel;


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

    private void LanguageSystemLoad()
    {
        string path = "";

    #if UNITY_ANDROID && !UNITY_EDITOR
        path = "jar:file://" + Application.dataPath + "!/assets/Languages/" + PlayerPrefs.GetString("Languages", "ENG") + ".json";
        StartCoroutine(ReadFileFromAndroid(path, (result) =>
        {
            json = result;
            ls = JsonUtility.FromJson<LanguageSystem>(json);
        }));
    #else
        path = Path.Combine(Application.streamingAssetsPath, "Languages", PlayerPrefs.GetString("Languages", "ENG") + ".json");
        json = File.ReadAllText(path);
        ls = JsonUtility.FromJson<LanguageSystem>(json);
    #endif



        if (DisplayText.instance)
        {
            DisplayText.instance.ChangeLanguageAndRefresh();
        }
        if (HeroesText.instance)
        {
            HeroesText.instance.ChangeLanguageAndRefresh();
        }
        if (ShopText.instance)
        {
            ShopText.instance.ChangeLanguageAndRefresh();
        }
        if (MenuText.instance)
        {
            MenuText.instance.ChangeLanguageAndRefresh();
        }
        if (SetteningsText.instance)
        {
            SetteningsText.instance.ChangeLanguageAndRefresh();
        }
        if (PauseText.instance)
        {
            PauseText.instance.ChangeLanguageAndRefresh();
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



    IEnumerator ReadFileFromAndroid(string path, System.Action<string> callback)
    {
        UnityWebRequest www = UnityWebRequest.Get(path);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            callback.Invoke(www.downloadHandler.text);
        }
        else
        {
            string error = "Error loading file: " + www.error;
            bestScoreLabel.text = error;
            Debug.LogError(error);
        }
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

    public string play = "Play";
    public string heroes = "Heroes";
    public string shop = "Shop";
    public string settenigns = "Settenigns";
    public string exit = "Exit";

    public string back;
    public string levelUp;
    public string buy;

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