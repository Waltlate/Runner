using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class MageClass : BaseClass
    {
        public MageClass()
        {
            ClassName = "Mage";
            Description = LanguageSettenings.ls.descriptionMage;
            Level = 1;
            CurrentExp = 0;
            LevelExp = 50;
            LevelWeapon = 1;
            CurrentExpWeapon = 0;
            LevelExpWeapon = 2;
            Health = 5;
            Damage = Level + LevelWeapon - 1;
            Speed = 1f;
            Distance = 2.5f;
        }

        public MageClass(int level, int currenExp, int levelExp, int health, int levelWeapon, int currenExpWeapon, int levelExpWeapon)
        {
            ClassName = "Mage";
            Description = LanguageSettenings.ls.descriptionMage;
            Level = level;
            CurrentExp = currenExp;
            LevelExp = levelExp;
            LevelWeapon = levelWeapon;
            CurrentExpWeapon = currenExpWeapon;
            LevelExpWeapon = levelExpWeapon;
            Health = health;
            Damage = Level + LevelWeapon - 1;
            Speed = 1f;
            Distance = 2.5f;
        }
    }
