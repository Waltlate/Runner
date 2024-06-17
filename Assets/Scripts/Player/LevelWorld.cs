using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEditor;

public class LevelWorld : MonoBehaviour
{
    public static LevelWorld instance;
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

    void Awake()
    {
        instance = this;
    }
    public void LevelWorldUp()
    {
        int minLevel = Math.Min(Math.Min(PlayerPrefs.GetInt("WarriorLevel", 1), PlayerPrefs.GetInt("ArcherLevel", 1)), PlayerPrefs.GetInt("MageLevel", 1));
        if (levelWorld < minLevel)
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
        {
            btnMinus.interactable = true;
            btnMinus.gameObject.transform.Find("Sign").gameObject.GetComponent<Image>().color = new Color(0, 1, 0, 1);
        }
        else
        {
            btnMinus.interactable = false;
            btnMinus.gameObject.transform.Find("Sign").gameObject.GetComponent<Image>().color = new Color(1, 0, 0, 1);
        }

        int minLevel = Math.Min(Math.Min(PlayerPrefs.GetInt("WarriorLevel", 1), PlayerPrefs.GetInt("ArcherLevel", 1)), PlayerPrefs.GetInt("MageLevel", 1));
        if (levelWorld < minLevel)
        {
            btnPlus.interactable = true;
            btnPlus.gameObject.transform.Find("Sign").gameObject.GetComponent<Image>().color = new Color(0, 1, 0);
        }
        else
        {
            btnPlus.interactable = false;
            btnPlus.gameObject.transform.Find("Sign").gameObject.GetComponent<Image>().color = new Color(1, 0, 0);
        }
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
