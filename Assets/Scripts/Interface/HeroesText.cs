using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroesText : MonoBehaviour
{
    
    public static HeroesText instance;
    public GameObject canvasInfo;
    public TextMeshProUGUI textInfo;
    public Image image;
    private float attitude;
    public TextMeshProUGUI levelUpButtonText;
    public TextMeshProUGUI heroText;
    public TextMeshProUGUI statsText; 
    public TextMeshProUGUI weaponText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI levelWorldText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI damageText;
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI distanceText;
    public TextMeshProUGUI levelWeaponText;
    public TextMeshProUGUI descriptionText;
    public TMP_Dropdown dropdown;

    void Start()
    {
        ChangeLanguageAndRefresh();

        if (((float)Screen.height / 1280f) * 720f < (float)Screen.width)
        {
            attitude = (float)Screen.height / 1280f;
        }
        else
        {
            attitude = (float)Screen.width / 720f;
        }
        image.GetComponent<RectTransform>().transform.localScale = new Vector2(attitude, attitude);
    }

    void Awake()
    {
        instance = this;
    }

    public void ChangeLanguageAndRefresh()
    {
        //backButtonText.text = LanguageSettenings.ls.back;
        levelUpButtonText.text = LanguageSettenings.ls.levelUp;
        heroText.text = LanguageSettenings.ls.hero;
        statsText.text = LanguageSettenings.ls.stats;
        weaponText.text = LanguageSettenings.ls.weapon;
        levelText.text = $" {LanguageSettenings.ls.level}. {PlayerParameters.archer.Level} [{ConvertNumberToString(PlayerParameters.archer.CurrentExp)}/{ConvertNumberToString(PlayerParameters.archer.LevelExp)}]";
        levelWorldText.text = $"{LanguageSettenings.ls.levelWorldFull} {LevelWorld.levelWorld}";
        healthText.text = $" {LanguageSettenings.ls.health}: {PlayerParameters.archer.Level * PlayerParameters.archer.Health}";
        damageText.text = $" {LanguageSettenings.ls.damage}: {PlayerParameters.archer.Damage}";
        speedText.text = $" {LanguageSettenings.ls.speed}: {PlayerParameters.archer.Speed}";
        distanceText.text = $" {LanguageSettenings.ls.distance}: {PlayerParameters.archer.Distance}";
        levelWeaponText.text = $" {LanguageSettenings.ls.level}. {PlayerParameters.archer.LevelWeapon} [{ConvertNumberToString(PlayerParameters.archer.CurrentExpWeapon)}/{ConvertNumberToString(PlayerParameters.archer.LevelExpWeapon)}]";
        descriptionText.text = PlayerParameters.archer.Description;
        dropdown.options[0].text = LanguageSettenings.ls.classNameWarrior;
        dropdown.options[1].text = LanguageSettenings.ls.classNameArcher;
        dropdown.options[2].text = LanguageSettenings.ls.classNameMage;
        dropdown.captionText.text = dropdown.options[dropdown.value].text;
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

    public void InfoLevelWorld()
    {
        canvasInfo.SetActive(true);
        textInfo.text = LanguageSettenings.ls.infoLevelWorld;
    }
}
