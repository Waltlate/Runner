using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class PlayerParameters : MonoBehaviour
{
    public static PlayerParameters instance;
    public static BaseClass archer;
    //public static int level = 1;
    //public static int currentExp = 0;
    //public static int levelExp = 0;
    public static int health;
    public static int maxHealth = 5;
    public static float distance;
    public static float speedAttack;
    public static int Coins = 0;
    public static int Score = 0;
    public static int BestScore = 0;

    private int k1 = 25;
    private int k2 = 2;

    public TextMeshProUGUI hpLabel;
    public TextMeshProUGUI levelLabel;
    public TextMeshProUGUI scoreLabel;
    public TextMeshProUGUI bestScoreLabel;
    
    public TextMeshProUGUI coinsLabel;
    
    
    // Start is called before the first frame update
    void Start()
    {
        archer = new WarriorClass(PlayerPrefs.GetInt("WarriorLevel", 1),
                                  PlayerPrefs.GetInt("WarriorCurrentExp", 0),
                                  PlayerPrefs.GetInt("WarriorLevelExp", 50),
                                  PlayerPrefs.GetInt("WarriorHealth", 10));
        Debug.Log(archer.ClassName + "ff");
        maxHealth = archer.Health;
        health = archer.Level * maxHealth;
        Score = 0;
        archer.LevelExp = k1 * BinaryPow(archer.Level + 1, k2) - k1 * (archer.Level + 1);
        levelLabel.text = $" Lvl. {archer.Level} [{ConvertNumberToString(archer.CurrentExp)}/{ConvertNumberToString(archer.LevelExp)}]";
        hpLabel.text = $" HP: {health}";

        scoreLabel.text = $"Score: {Score}";
        bestScoreLabel.text = $"Best Score: {Score}";
        coinsLabel.text = $"Coins: {Coins}";
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
        }
        levelLabel.text = $" Lvl. {archer.Level} [{ConvertNumberToString(archer.CurrentExp)}/{ConvertNumberToString(archer.LevelExp)}]";
        hpLabel.text = $" HP: {health}";
        scoreLabel.text = $"Score: {Score}";
        bestScoreLabel.text = $"Best Score: {BestScore}";
        coinsLabel.text = $"Coins: {Coins}";
    }

    public void Stats()
    {
        archer.Level = archer.Level;
        //maxHealth = archer.Health;
        health = archer.Level * archer.Health;
        archer.CurrentExp = archer.CurrentExp;
        archer.LevelExp = archer.LevelExp;
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
