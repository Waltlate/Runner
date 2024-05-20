using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HeroesText : MonoBehaviour
{
    public static HeroesText instance;
    public TextMeshProUGUI backButtonText;
    public TextMeshProUGUI levelUpButtonText;
    public TextMeshProUGUI heroText;
    public TextMeshProUGUI statsText; //параметры
    public TextMeshProUGUI weaponText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI damageText;
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI distanceText;
    public TextMeshProUGUI levelWeaponText;
    public TextMeshProUGUI descriptionText;

    void Start()
    {
        ChangeLanguageAndRefresh();
    }

    void Awake()
    {
        instance = this;
    }

    public void ChangeLanguageAndRefresh()
    {
        backButtonText.text = LanguageSettenings.ls.back;
        levelUpButtonText.text = LanguageSettenings.ls.levelUp;
        heroText.text = LanguageSettenings.ls.hero;
        statsText.text = LanguageSettenings.ls.stats;
        weaponText.text = LanguageSettenings.ls.weapon;
        levelText.text = $" {LanguageSettenings.ls.level}. {PlayerParameters.archer.Level} [{ConvertNumberToString(PlayerParameters.archer.CurrentExp)}/{ConvertNumberToString(PlayerParameters.archer.LevelExp)}]";
        healthText.text = $" {LanguageSettenings.ls.health}: {PlayerParameters.archer.Level * PlayerParameters.archer.Health}";
        damageText.text = $" {LanguageSettenings.ls.damage}: {PlayerParameters.archer.Damage}";
        speedText.text = $" {LanguageSettenings.ls.speed}: {PlayerParameters.archer.Speed}";
        distanceText.text = $" {LanguageSettenings.ls.distance}: {PlayerParameters.archer.Distance}";
        levelWeaponText.text = $" {LanguageSettenings.ls.level}. {PlayerParameters.archer.LevelWeapon} [{ConvertNumberToString(PlayerParameters.archer.CurrentExpWeapon)}/{ConvertNumberToString(PlayerParameters.archer.LevelExpWeapon)}]";
        descriptionText.text = PlayerParameters.archer.Description;
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
}
