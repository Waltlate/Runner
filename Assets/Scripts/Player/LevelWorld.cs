using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEditor;

public class LevelWorld : MonoBehaviour
{
    public static int levelWorld;
    public static int levelEnemy = 1;
    public Button btnPlus;
    public Button btnMinus;
    public GameObject menu;

    public void Start()
    {
        levelWorld = PlayerPrefs.GetInt("LevelWorld", 1);
        ChangeStateButton();
    }

    public void LevelWorldUp()
    {
        int minLevel = Math.Min(Math.Min(PlayerPrefs.GetInt("WarriorLevel", 1), PlayerPrefs.GetInt("ArcherLevel", 1)), PlayerPrefs.GetInt("MageLevel", 1));
        if(levelWorld < minLevel)
        {
            levelWorld++;
            RefreshGame();
        }
    }

    public void LevelWorldDown()
    {
        if (levelWorld > 1)
        {
            levelWorld--;
            RefreshGame();
        }
    }

    public void ChangeStateButton()
    {
        if (levelWorld > 1)
            btnMinus.interactable = true;
        else
            btnMinus.interactable = false;

        int minLevel = Math.Min(Math.Min(PlayerPrefs.GetInt("WarriorLevel", 1), PlayerPrefs.GetInt("ArcherLevel", 1)), PlayerPrefs.GetInt("MageLevel", 1));
        if (levelWorld < minLevel)
            btnPlus.interactable = true;
        else
            btnPlus.interactable = false;
    }

    private void RefreshGame()
    {
        if (HeroesText.instance)
            HeroesText.instance.ChangeLanguageAndRefresh();
        RoadGenerator.instance.ResetLevel();
        menu.SetActive(false);
        PlayerPrefs.SetInt("LevelWorld", levelWorld);
        ChangeStateButton();
    }
}
