using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorClass : BaseClass
{
    public WarriorClass() {
        ClassName = "Warrior";
        Description = LanguageSettenings.ls.descriptionWarrior;
        Level = 1;
        CurrentExp = 0;
        LevelExp = 50;
        LevelWeapon = 1;
        CurrentExpWeapon = 0;
        LevelExpWeapon = 2;
        Health = 10;
        Damage = Level + LevelWeapon - 1;
        Speed = 1.5f;
        Distance = 1f;
    }


    public WarriorClass(int level, int currenExp, int levelExp, int health, int levelWeapon, int currenExpWeapon, int levelExpWeapon)
    {
        ClassName = "Warrior";
        Description = LanguageSettenings.ls.descriptionWarrior;
        Level = level;
        CurrentExp = currenExp;
        LevelExp = levelExp;
        LevelWeapon = levelWeapon;
        CurrentExpWeapon = currenExpWeapon;
        LevelExpWeapon = levelExpWeapon;
        Health = health;
        Damage = Level + LevelWeapon - 1;
        Speed = 1.5f;
        Distance = 1f;
    }
}
