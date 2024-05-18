using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SwitchClass : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    //public GameObject warrior;
    public GameObject archer;
    public Material archerMaterial;
    public Material warriorMaterial;

    public void Switch() {
        if (dropdown.options[dropdown.value].text == "Warrior") {
            PlayerParameters.archer = new WarriorClass(PlayerPrefs.GetInt("WarriorLevel", 1),
                                                        PlayerPrefs.GetInt("WarriorCurrentExp", 0),
                                                        PlayerPrefs.GetInt("WarriorLevelExp", 50),
                                                        PlayerPrefs.GetInt("WarriorHealth", 10));
            archer.GetComponent<MeshRenderer>().material = warriorMaterial;
            PlayerParameters.instance.Stats();
            //archer.SetActive(false);
            //warrior.SetActive(true);
        }
        if (dropdown.options[dropdown.value].text == "Archer")
        {
            PlayerParameters.archer = new ArcherClass(PlayerPrefs.GetInt("ArcherLevel", 1),
                                                        PlayerPrefs.GetInt("ArcherCurrentExp", 0),
                                                        PlayerPrefs.GetInt("ArcherLevelExp", 50),
                                                        PlayerPrefs.GetInt("ArcherHealth", 5));
            archer.GetComponent<MeshRenderer>().material = archerMaterial;
            PlayerParameters.instance.Stats();
            //archer.SetActive(true);
            //warrior.SetActive(false);
        }
    }
}
