using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SwitchClass : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public GameObject warrior;
    public GameObject archer;

    public void Switch() {
        if (dropdown.options[dropdown.value].text == "Warrior") {
            PlayerParameters.archer = new WarriorClass();
            archer.SetActive(false);
            warrior.SetActive(true);
        }
        if (dropdown.options[dropdown.value].text == "Archer")
        {
            PlayerParameters.archer = new ArcherClass();
            archer.SetActive(true);
            warrior.SetActive(false);
        }
    }
}
