using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BaseClass
{
    private string className;
    private string description;
    private int level;
    private int currentExp;
    private int levelExp;
    private int damage;
    private int health;
    private float speed;
    private float distance;
    private int levelWeapon;
    private int currentExpWeapon;
    private int levelExpWeapon;



    public string ClassName
    {
        get { return className; }
        set { className = value; }
    }

    public string Description
    {
        get { return description; }
        set { description = value; }
    }

    public int Level
    {
        get { return level; }
        set { level = value; }
    }

    public int CurrentExp
    {
        get { return currentExp; }
        set { currentExp = value; }
    }

    public int LevelExp
    {
        get { return levelExp; }
        set { levelExp = value; }
    }

    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    public int Health
    {
        get { return health; }
        set { health = value; }
    }

    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    public float Distance
    {
        get { return distance; }
        set { distance = value; }
    }

    public int LevelWeapon
    {
        get { return levelWeapon; }
        set { levelWeapon = value; }
    }

    public int CurrentExpWeapon
    {
        get { return currentExpWeapon; }
        set { currentExpWeapon = value; }
    }

    public int LevelExpWeapon
    {
        get { return levelExpWeapon; }
        set { levelExpWeapon = value; }
    }
}
