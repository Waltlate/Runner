using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorClass : BaseClass
{
    public WarriorClass() {
        ClassName = "Warrior";
        Level = 1;
        CurrentExp = 0;
        LevelExp = 50;
        Health = 10;
        Damage = 1;
        Speed = 1f;
        Distance = 1f;
    }


    public WarriorClass(int level, int currenExp, int levelExp, int health)
    {
        ClassName = "Warrior";
        //Level = PlayerPrefs.GetInt("WarriorLevel", 1);
        //CurrentExp = PlayerPrefs.GetInt("WarriorCurrentExp", 0);
        //LevelExp = PlayerPrefs.GetInt("WarriorLevelExp", 50);
        //Health = PlayerPrefs.GetInt("WarriorHealth", 10);
        Level = level;
        CurrentExp = currenExp;
        LevelExp = levelExp;
        Health = health;
        Damage = 1;
        Speed = 1f;
        Distance = 1f;
    }
}
