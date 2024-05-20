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
        Description = LanguageSettenings.ls.descriptionArcher;
        Level = 1;
        CurrentExp = 0;
        LevelExp = 50;
        LevelWeapon = 1;
        CurrentExpWeapon = 0;
        LevelExpWeapon = 2;
        Health = 5;
        Damage = Level + LevelWeapon - 1;
        Speed = 1.5f;
        Distance = 2.5f;
    }

    public ArcherClass(int level, int currenExp, int levelExp, int health)
    {
        ClassName = "Archer";
        Description = LanguageSettenings.ls.descriptionArcher;
        Level = level;
        CurrentExp = currenExp;
        LevelExp = levelExp;
        LevelWeapon = 1;
        CurrentExpWeapon = 0;
        LevelExpWeapon = 2;
        Health = health;
        Damage = Level + LevelWeapon - 1;
        Speed = 1.5f;
        Distance = 2.5f;
    }
}
