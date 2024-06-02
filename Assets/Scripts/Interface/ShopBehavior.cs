using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopBehavior : MonoBehaviour
{
    public static ShopBehavior instance;
    public GameObject canvasInfo;
    public TextMeshProUGUI textInfo;
    private string textHeroes = "Из сундука героев падает 10 случайных карт оружия героев";
    private string textWarrior = "Из сундука воина падает 5 карт оружия воина";
    private string textArcher = "Из сундука лучника падает 5 карт оружия лучника";
    private string textMage = "Из сундука мага падает 5 карт оружия мага";
    private Button[] btn;

    public void Start()
    {
        ChangeStateButton();
    }

    public void Awake()
    {
        instance = this;
    }

    public void ChangeStateButton()
    {
        btn = GetComponentsInChildren<Button>();
        if (btn.Length != 0)
        {
            if (PlayerParameters.Coins >= 1000)
            {
                SetInteractableBtn(true);
            }
            else
            {
                SetInteractableBtn(false);
            }
        }
    }

    private void SetInteractableBtn(bool state) {
        foreach (var elem in btn)
        {
            if(elem.name == "ButtonChest")
            elem.interactable = state;
        }
    }

    public void BuyCards()
    {
        if (PlayerParameters.instance)
            if (PlayerParameters.Coins >= 1000)
            {
                PlayerParameters.Coins -= 1000;
                int warriorCurrentExpWeapon = PlayerPrefs.GetInt("WarriorCurrentExpWeapon", 0);
                int archerCurrentExpWeapon = PlayerPrefs.GetInt("ArcherCurrentExpWeapon", 0);
                int mageCurrentExpWeapon = PlayerPrefs.GetInt("MageCurrentExpWeapon", 0);

                int[] num = new int[3];
                int j;
                j = new System.Random().Next(0, 3);
                num[j % 3] = new System.Random().Next(0, 11);
                num[(j + 1) % 3] = new System.Random().Next(0, 11 - num[j % 3]);
                num[(j + 2) % 3] = 10 - num[j % 3] - num[(j + 1) % 3];
                Debug.Log($"{num[0] + num[1] + num[2]} - {num[0]}, {num[1]}, {num[2]}");

                PlayerPrefs.SetInt("WarriorCurrentExpWeapon", warriorCurrentExpWeapon + num[0]);
                PlayerPrefs.SetInt("ArcherCurrentExpWeapon", archerCurrentExpWeapon + num[1]);
                PlayerPrefs.SetInt("MageCurrentExpWeapon", mageCurrentExpWeapon + num[2]);

                if (PlayerParameters.archer.ClassName == "Warrior")
                {
                    PlayerParameters.archer.CurrentExpWeapon += num[0];
                }
                if (PlayerParameters.archer.ClassName == "Archer")
                {
                    PlayerParameters.archer.CurrentExpWeapon += num[1];
                }
                if (PlayerParameters.archer.ClassName == "Mage")
                {
                    PlayerParameters.archer.CurrentExpWeapon += num[2];
                }

                if (HeroesText.instance)
                {
                    HeroesText.instance.ChangeLanguageAndRefresh();
                }
                if (ShopText.instance)
                {
                    ShopText.instance.ChangeLanguageAndRefresh();
                }
                if (WeaponBehavior.instance)
                {
                    WeaponBehavior.instance.ChangeStateButton();
                }
                ChangeStateButton();
                if (PlayerParameters.Coins != 0)
                    PlayerPrefs.SetInt("Coins", PlayerParameters.Coins);
            }

    }

    public void BuyCardsArcher()
    {
        if (PlayerParameters.instance)
            if (PlayerParameters.Coins >= 1000)
            {
                BuyBase("Archer");
            }
    }

    public void BuyCardsWarrior()
    {
        if (PlayerParameters.instance)
            if (PlayerParameters.Coins >= 1000)
            {
                BuyBase("Warrior");
            }
    }

    public void BuyCardsMage()
    {
        if (PlayerParameters.instance)
            if (PlayerParameters.Coins >= 1000)
            {
                BuyBase("Mage");
            }
    }

    private void BuyBase(string className) {
        PlayerParameters.Coins -= 1000;
        int currentExpWeapon = PlayerPrefs.GetInt(className + "CurrentExpWeapon", 0);

        PlayerPrefs.SetInt(className + "CurrentExpWeapon", currentExpWeapon + 5);
        if (PlayerParameters.archer.ClassName == className)
        {
            PlayerParameters.archer.CurrentExpWeapon += 5;
        }
        if (HeroesText.instance)
        {
            HeroesText.instance.ChangeLanguageAndRefresh();
        }
        if (ShopText.instance)
        {
            ShopText.instance.ChangeLanguageAndRefresh();
        }
        if (WeaponBehavior.instance)
        {
            WeaponBehavior.instance.ChangeStateButton();
        }
        ChangeStateButton();
        if(PlayerParameters.Coins != 0)
        PlayerPrefs.SetInt("Coins", PlayerParameters.Coins);
    }

    public void InfoChestHeroes()
    {
        canvasInfo.SetActive(true);
        textInfo.text = textHeroes;
    }

    public void InfoChestWarrior()
    {
        canvasInfo.SetActive(true);
        textInfo.text = textWarrior;
    }

    public void InfoChestArcher()
    {
        canvasInfo.SetActive(true);
        textInfo.text = textArcher;
    }

    public void InfoChestMage()
    {
        canvasInfo.SetActive(true);
        textInfo.text = textMage;
    }
}
