using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponBehavior : MonoBehaviour
{
    public static WeaponBehavior instance;
    private Button btn;
    public void Start()
    {
        ChangeStateButton();
    }

    public void Awake()
    {
        instance = this;
    }

    public void UpgradeWeapon()
    {
        if(PlayerParameters.archer.CurrentExpWeapon >= PlayerParameters.archer.LevelExpWeapon)
        {
            if (PlayerParameters.instance)
            {
                PlayerParameters.archer.CurrentExpWeapon -= PlayerParameters.archer.LevelExpWeapon;
                PlayerParameters.archer.LevelWeapon++;
                PlayerParameters.archer.LevelExpWeapon = PlayerParameters.instance.ExpLevelWeapon(PlayerParameters.archer.LevelWeapon);
                PlayerParameters.archer.Damage = PlayerParameters.archer.Level + PlayerParameters.archer.LevelWeapon - 1;
                PlayerPrefs.SetInt(PlayerParameters.archer.ClassName + "CurrentExpWeapon", PlayerParameters.archer.CurrentExpWeapon);
                PlayerPrefs.SetInt(PlayerParameters.archer.ClassName + "LevelWeapon", PlayerParameters.archer.LevelWeapon);
                PlayerPrefs.SetInt(PlayerParameters.archer.ClassName + "LevelExpWeapon", PlayerParameters.archer.LevelExpWeapon);
            }
            if (HeroesText.instance)
            {
                HeroesText.instance.ChangeLanguageAndRefresh();
            }
        }
        ChangeStateButton();
    }

    public void ChangeStateButton() {
        btn = GetComponent<Button>();
        if (btn)
        {
            if (PlayerParameters.archer.CurrentExpWeapon >= PlayerParameters.archer.LevelExpWeapon)
            {
                btn.interactable = true;
            }
            else
            {
                btn.interactable = false;
            }
        }
    }

}
