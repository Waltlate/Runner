using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerParameters : MonoBehaviour
{
    public static PlayerParameters instance;
    public static BaseClass archer;
    public Image expFill;
    public static int health;
    public static int maxHealth = 5;
    public Image hpFill;
    public static float distance;
    public static float speedAttack;
    public static int Coins = 0;
    public static int Score = 0;
    public static int BestScore = 0;

    private int k1 = 25;
    private int k2 = 2;

    public TextMeshProUGUI levelLabel;
    public TextMeshProUGUI levelWorldLabel;
    public TextMeshProUGUI currentLevelWorldLabel;
    public TextMeshProUGUI hpLabel;
    public TextMeshProUGUI scoreLabel;
    public TextMeshProUGUI bestScoreLabel;
    public TextMeshProUGUI coinsLabel;
    
    
    // Start is called before the first frame update
    void Start()
    {
        BestScore = PlayerPrefs.GetInt("BestScore", 0);
        Coins = PlayerPrefs.GetInt("Coins", 0);
        //Coins = 10000;
        archer = new WarriorClass(PlayerPrefs.GetInt("WarriorLevel", 1),
                                  PlayerPrefs.GetInt("WarriorCurrentExp", 0),
                                  PlayerPrefs.GetInt("WarriorLevelExp", 50),
                                  PlayerPrefs.GetInt("WarriorHealth", 10),
                                  PlayerPrefs.GetInt("WarriorLevelWeapon", 1),
                                  PlayerPrefs.GetInt("WarriorCurrentExpWeapon", 0),
                                  PlayerPrefs.GetInt("WarriorLevelExpWeapon", 2));
        maxHealth = archer.Health;
        health = archer.Level * maxHealth;
        archer.LevelExp = k1 * BinaryPow(archer.Level + 1, k2) - k1 * (archer.Level + 1);
        archer.LevelExpWeapon = ExpLevelWeapon(archer.LevelWeapon);
        Score = 0;
        LoadText();
        RoadGenerator.instance.dropdownClass.value = PlayerPrefs.GetInt("NumbersHero", 0);
        if(SwitchClass.instance)
        SwitchClass.instance.Switch();
    }

    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(archer.CurrentExp >= archer.LevelExp) {
            archer.CurrentExp -= archer.LevelExp;
            archer.Level += 1;
            health = archer.Level * maxHealth;
            archer.LevelExp = k1 * BinaryPow(archer.Level + 1, k2) - k1 * (archer.Level + 1);
            archer.Damage = archer.Level + archer.LevelWeapon - 1;
        }
        LoadText();
    }

    private void LoadText() {
        levelLabel.text = $"{LanguageSettenings.ls.level}. {archer.Level} [{ConvertNumberToString(archer.CurrentExp)}/{ConvertNumberToString(archer.LevelExp)}]";
        levelWorldLabel.text = $"{LanguageSettenings.ls.levelWorld}";
        currentLevelWorldLabel.text = $"{LevelWorld.levelWorld}";
        hpLabel.text = $"{health}/{maxHealth * archer.Level}";
        hpFill.fillAmount = (float)health / (maxHealth * archer.Level);
        expFill.fillAmount = (float)archer.CurrentExp / archer.LevelExp;
        scoreLabel.text = $"{Score}";
        bestScoreLabel.text = $"{LanguageSettenings.ls.bestScore}: {BestScore}";
        coinsLabel.text = $"{Coins}";
    }

    public void Stats()
    {
        archer.Level = archer.Level;
        //maxHealth = archer.Health;
        health = archer.Level * archer.Health;
        archer.CurrentExp = archer.CurrentExp;
        archer.LevelExp = archer.LevelExp;
    }

    public int ExpLevelWeapon(int level) {
        int result = 1;

        result *= BinaryPow(10, level / 3);
        if (level % 3 == 1) result *= 2;
        if (level % 3 == 2) result *= 5;
        return result;
    }

    string ConvertNumberToString(float number)
    {
        const string k = "k";
        const string m = "m";
        const string b = "b";

        if (number < 1000)
        {
            return number.ToString("###0.");
        }
        else if (number < 1000000)
        {
            return (number / 1000f).ToString("0.##") + k;
        }
        else if (number < 1000000000)
        {
            return (number / 1000000f).ToString("0.###") + m;
        }
        else
        {
            return (number / 1000000000f).ToString("0.####") + b;
        }
    }

    int BinaryPow(int baseNumber, int exponent)
    {
        int result = 1;
        while (exponent > 0)
        {
            if ((exponent & 1) == 1)
                result *= baseNumber;
            baseNumber *= baseNumber;
            exponent >>= 1;
        }
        return result;
    }

}
