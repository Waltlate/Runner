using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SwitchClass : MonoBehaviour
{

    public static SwitchClass instance;
    public TMP_Dropdown dropdown;
    public GameObject archer;
    public Material archerMaterial;
    public Material warriorMaterial;
    public Material mageMaterial;

    public void Awake()
    {
        instance = this;
    }

    public void Switch() {
        if (dropdown.options[dropdown.value].text == "Warrior") {
            PlayerParameters.archer = new WarriorClass(PlayerPrefs.GetInt("WarriorLevel", 1),
                                                        PlayerPrefs.GetInt("WarriorCurrentExp", 0),
                                                        PlayerPrefs.GetInt("WarriorLevelExp", 50),
                                                        PlayerPrefs.GetInt("WarriorHealth", 10),
                                                        PlayerPrefs.GetInt("WarriorLevelWeapon", 1),
                                                        PlayerPrefs.GetInt("WarriorCurrentExpWeapon", 0),
                                                        PlayerPrefs.GetInt("WarriorLevelExpWeapon", 2));
            archer.GetComponent<MeshRenderer>().material = warriorMaterial;
            PlayerParameters.maxHealth = PlayerPrefs.GetInt("WarriorHealth", 10);
            PlayerPrefs.SetInt("NumbersHero", 0);
        }
        if (dropdown.options[dropdown.value].text == "Archer")
        {
            PlayerParameters.archer = new ArcherClass(PlayerPrefs.GetInt("ArcherLevel", 1),
                                                        PlayerPrefs.GetInt("ArcherCurrentExp", 0),
                                                        PlayerPrefs.GetInt("ArcherLevelExp", 50),
                                                        PlayerPrefs.GetInt("ArcherHealth", 5),
                                                        PlayerPrefs.GetInt("ArcherLevelWeapon", 1),
                                                        PlayerPrefs.GetInt("ArcherCurrentExpWeapon", 0),
                                                        PlayerPrefs.GetInt("ArcherLevelExpWeapon", 2));
            archer.GetComponent<MeshRenderer>().material = archerMaterial;
            PlayerParameters.maxHealth = PlayerPrefs.GetInt("ArcherHealth", 5);
            PlayerPrefs.SetInt("NumbersHero", 1);
        }
        if (dropdown.options[dropdown.value].text == "Mage")
        {
            PlayerParameters.archer = new MageClass(PlayerPrefs.GetInt("MageLevel", 1),
                                                        PlayerPrefs.GetInt("MageCurrentExp", 0),
                                                        PlayerPrefs.GetInt("MageLevelExp", 50),
                                                        PlayerPrefs.GetInt("MageHealth", 5),
                                                        PlayerPrefs.GetInt("MageLevelWeapon", 1),
                                                        PlayerPrefs.GetInt("MageCurrentExpWeapon", 0),
                                                        PlayerPrefs.GetInt("MageLevelExpWeapon", 2));
            archer.GetComponent<MeshRenderer>().material = mageMaterial;
            PlayerParameters.maxHealth = PlayerPrefs.GetInt("MageHealth", 5);
            PlayerPrefs.SetInt("NumbersHero", 2);
        }
        PlayerParameters.instance.Stats();
        if(HeroesText.instance)
            HeroesText.instance.ChangeLanguageAndRefresh();
        if(WeaponBehavior.instance)
            WeaponBehavior.instance.ChangeStateButton();
    }
}
