using System.Collections;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.Networking;

public class LanguageSettenings : MonoBehaviour
{
    private string json;
    public static LanguageSystem ls = new();
    [SerializeField] private TMP_Dropdown dropdown;
    [SerializeField] private TextMeshProUGUI bestScoreLabel;

    void Awake()
    {
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
        StartCoroutine(ReadFileFromAndroid(path));
#else
        path = Path.Combine(Application.streamingAssetsPath, "Languages", PlayerPrefs.GetString("Languages", "ENG") + ".json");
        json = File.ReadAllText(path);
        ls = JsonUtility.FromJson<LanguageSystem>(json);
        UpdateCanvas();
#endif

    }

    private void UpdateCanvas()
    {
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
        if (FirstScene.instance != null)
        {
            FirstScene.instance.ChangeLanguageAndRefresh();
        }
        if (TopRunText.instance != null)
        {
            TopRunText.instance.ChangeLanguageAndRefresh();
        }
    }

    IEnumerator ReadFileFromAndroid(string path)
    {
        UnityWebRequest www = UnityWebRequest.Get(path);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            json = www.downloadHandler.text;
            ls = JsonUtility.FromJson<LanguageSystem>(json);
            UpdateCanvas();
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
    public string flyTrapText;
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