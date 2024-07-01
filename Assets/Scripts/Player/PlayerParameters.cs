using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerParameters : MonoBehaviour
{
    public static PlayerParameters instance;
    public static BaseClass archer;
    [SerializeField] private Image expFill;
    public static int health;
    public static int maxHealth = 5;
    [SerializeField] private Image hpFill;
    public static float distance;
    public static float speedAttack;
    public static int Coins = 0;
    public static int Score = 0;
    public static int BestScore = 0;

    private readonly int expDifferense = 25;
    private readonly int expLevel = 2;

    [SerializeField] private TextMeshProUGUI levelLabel;
    [SerializeField] private TextMeshProUGUI levelWorldLabel;
    [SerializeField] private TextMeshProUGUI currentLevelWorldLabel;
    [SerializeField] private TextMeshProUGUI hpLabel;
    [SerializeField] private TextMeshProUGUI scoreLabel;
    [SerializeField] private TextMeshProUGUI bestScoreLabel;
    [SerializeField] private TextMeshProUGUI coinsLabel;

    [SerializeField] private GameObject roadGenerator;

    void Start()
    {
        BestScore = PlayerPrefs.GetInt("BestScore", 0);
        Coins = PlayerPrefs.GetInt("Coins", 0);
        archer = new WarriorClass(PlayerPrefs.GetInt("WarriorLevel", 1),
                                  PlayerPrefs.GetInt("WarriorCurrentExp", 0),
                                  PlayerPrefs.GetInt("WarriorLevelExp", 50),
                                  PlayerPrefs.GetInt("WarriorHealth", 10),
                                  PlayerPrefs.GetInt("WarriorLevelWeapon", 1),
                                  PlayerPrefs.GetInt("WarriorCurrentExpWeapon", 0),
                                  PlayerPrefs.GetInt("WarriorLevelExpWeapon", 2));
        maxHealth = archer.Health;
        health = archer.Level * maxHealth;
        archer.LevelExp = expDifferense * BinaryPow(archer.Level + 1, expLevel) - expDifferense * (archer.Level + 1);
        archer.LevelExpWeapon = ExpLevelWeapon(archer.LevelWeapon);
        Score = 0;
        LoadText();
        RoadGenerator.instance.dropdownClass.value = PlayerPrefs.GetInt("NumbersHero", 0);
        if (SwitchClass.instance)
            SwitchClass.instance.Switch();
    }

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (archer.CurrentExp >= archer.LevelExp)
        {
            archer.CurrentExp -= archer.LevelExp;
            archer.Level += 1;
            health = archer.Level * maxHealth;
            archer.LevelExp = expDifferense * BinaryPow(archer.Level + 1, expLevel) - expDifferense * (archer.Level + 1);
            archer.Damage = archer.Level + archer.LevelWeapon - 1;
            EnemyUpdate();
        }
        LoadText();
    }

    void LoadText()
    {
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
        health = archer.Level * archer.Health;
        archer.CurrentExp = archer.CurrentExp;
        archer.LevelExp = archer.LevelExp;
    }

    public int ExpLevelWeapon(int level)
    {
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

    void EnemyUpdate()
    {
        EnemyBehavior[] enemies = roadGenerator.GetComponentsInChildren<EnemyBehavior>();

        foreach (var elem in enemies)
        {
            elem.ColorLabelUpdate();
        }
    }
}
