using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SwitchClass : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public GameObject archer;
    public Material archerMaterial;
    public Material warriorMaterial;

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
            //PlayerParameters.instance.Stats();
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
        }
        PlayerParameters.instance.Stats();
        HeroesText.instance.ChangeLanguageAndRefresh();
        WeaponBehavior.instance.ChangeStateButton();
    }
}
