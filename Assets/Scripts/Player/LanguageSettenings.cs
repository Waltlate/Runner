using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LanguageSettenings : MonoBehaviour
{
    private string json;
    public static LanguageSystem ls = new LanguageSystem();

    void Awake()
    {
        //14:00 haskey
        PlayerPrefs.SetString("Languages", "ENG");
        LanguageSystemLoad();
    }

    void LanguageSystemLoad()
    {
        json = File.ReadAllText(Application.streamingAssetsPath + "/Languages/"+ PlayerPrefs.GetString("Languages", "ENG") +".json");
        ls = JsonUtility.FromJson<LanguageSystem>(json);
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
}