using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopBehavior : MonoBehaviour
{
    public static ShopBehavior instance;
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
                //foreach (var elem in btn){
                //    elem.interactable = true;
                //}
                //btn.interactable = true;
            }
            else
            {
                SetInteractableBtn(false);
                //foreach (var elem in btn){
                //    Debug.Log(elem.name);
                //    elem.interactable = false;
                //}
                //btn.interactable = false;
            }
        }
    }

    private void SetInteractableBtn(bool state) {
        foreach (var elem in btn)
        {
         //   Debug.Log(elem.name);
            if(elem.name != "BackBtn")
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
                PlayerPrefs.SetInt("Coins", PlayerParameters.Coins);
            }

    }

    public void BuyCardsArcher()
    {
        if (PlayerParameters.instance)
            if (PlayerParameters.Coins >= 1000)
            {
                BuyBase("Archer");
                //PlayerParameters.Coins -= 1000;
                //int currentExpWeapon = PlayerPrefs.GetInt("ArcherCurrentExpWeapon", 0);

                //PlayerPrefs.SetInt("ArcherCurrentExpWeapon", currentExpWeapon + 5);
                //if (PlayerParameters.archer.ClassName == "Archer")
                //{
                //    PlayerParameters.archer.CurrentExpWeapon += 5;
                //}
                //if (HeroesText.instance)
                //{
                //    HeroesText.instance.ChangeLanguageAndRefresh();
                //}
                //if (ShopText.instance)
                //{
                //    ShopText.instance.ChangeLanguageAndRefresh();
                //}
            }
        //WeaponBehavior.instance.ChangeStateButton();
    }

    public void BuyCardsWarrior()
    {
        if (PlayerParameters.instance)
            if (PlayerParameters.Coins >= 1000)
            {
                BuyBase("Warrior");
                //PlayerParameters.Coins -= 1000;
                //int currentExpWeapon = PlayerPrefs.GetInt("WarriorCurrentExpWeapon", 0);

                //PlayerPrefs.SetInt("WarriorCurrentExpWeapon", currentExpWeapon + 5);
                //if (PlayerParameters.archer.ClassName == "Warrior")
                //{
                //    PlayerParameters.archer.CurrentExpWeapon += 5;
                //}
                //if (HeroesText.instance)
                //{
                //    HeroesText.instance.ChangeLanguageAndRefresh();
                //}
                //if (ShopText.instance)
                //{
                //    ShopText.instance.ChangeLanguageAndRefresh();
                //}
            }
        //WeaponBehavior.instance.ChangeStateButton();
    }

    public void BuyCardsMage()
    {
        if (PlayerParameters.instance)
            if (PlayerParameters.Coins >= 1000)
            {
                BuyBase("Mage");
                //PlayerParameters.Coins -= 1000;
                //int currentExpWeapon = PlayerPrefs.GetInt("MageCurrentExpWeapon", 0);

                //PlayerPrefs.SetInt("MageCurrentExpWeapon", currentExpWeapon + 5);
                //if (PlayerParameters.archer.ClassName == "Mage")
                //{
                //    PlayerParameters.archer.CurrentExpWeapon += 5;
                //}
                //if (HeroesText.instance)
                //{
                //    HeroesText.instance.ChangeLanguageAndRefresh();
                //}
                //if (ShopText.instance)
                //{
                //    ShopText.instance.ChangeLanguageAndRefresh();
                //}
                //WeaponBehavior.instance.ChangeStateButton();
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
        PlayerPrefs.SetInt("Coins", PlayerParameters.Coins);
    }
}
