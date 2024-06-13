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

        StartCoroutine(SomeCoroutine());
    }

    

    private IEnumerator SomeCoroutine()
    {
    yield return new WaitForSeconds(0.2f);
        if (HeroesText.instance != null)
        {
            HeroesText.instance.ChangeLanguageAndRefresh();
        }
        if (InfoBehavoir.instance != null)
        {
            InfoBehavoir.instance.ChangeLanguageAndRefresh();
        }
        if (ShopText.instance != null)
        {
            ShopText.instance.ChangeLanguageAndRefresh();
        }
        if (MenuText.instance != null)
        {
            MenuText.instance.ChangeLanguageAndRefresh();
        }
        if (SetteningsText.instance != null)
        {
            SetteningsText.instance.ChangeLanguageAndRefresh();
        }
        if (PauseText.instance != null)
        {
            PauseText.instance.ChangeLanguageAndRefresh();
        }
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

    public string play;
    public string startRun;
    public string heroes;
    public string shop;
    public string settenigns;
    public string topRun;
    public string mainMenu;
    public string exit;

    public string levelUp;

    public string hero;
    public string stats;
    public string weapon;

    public string startTutorial;

    public string continueBtn;
    public string restart;
    public string backMenu;

    public string level;
    public string levelWorld;
    public string levelWorldFull;
    public string health;
    public string damage;
    public string speed;
    public string distance;

    public string classNameWarrior;
    public string classNameArcher;
    public string classNameMage;

    public string descriptionWarrior;
    public string descriptionArcher;
    public string descriptionMage;

    public string bestScore;
    public string classText;
    public string score;
    public string dateText;

    public string close;
    public string infoLevelWorld;
    public string infoChestHeroes;
    public string infoChestWarrior;
    public string infoChestArcher;
    public string infoChestMage;

    public string chestHeroes;
    public string chestWarrior;
    public string chestArcher;
    public string chestMage;
}