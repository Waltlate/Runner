using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseClass
{
    private string className;
    private int level;
    private int damage;
    private int health;
    private float speed;
    private float distance;


    public string ClassName {
        get { return className; }
        set { className = value; }
    }

    public int Level
    {
        get { return level; }
        set { level = value; }
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
}
