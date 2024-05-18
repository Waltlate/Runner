using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ArcherClass : BaseClass
{
    public ArcherClass()
    {
        ClassName = "Archer";
        Level = 1;
        CurrentExp = 0;
        LevelExp = 50;
        Health = 5;
        Damage = 1;
        Speed = 1.5f;
        Distance = 2.5f;
    }

    public ArcherClass(int level, int currenExp, int levelExp, int health)
    {
        ClassName = "Archer";
        //Level = PlayerPrefs.GetInt("ArcherLevel", 1);
        //CurrentExp = PlayerPrefs.GetInt("ArcherCurrentExp", 0);
        //LevelExp = PlayerPrefs.GetInt("ArcherLevelExp", 50);
        //Health = PlayerPrefs.GetInt("ArcherHealth", 5);
        Level = level;
        CurrentExp = currenExp;
        LevelExp = levelExp;
        Health = health;
        Damage = 1;
        Speed = 1.5f;
        Distance = 2.5f;
    }
}
