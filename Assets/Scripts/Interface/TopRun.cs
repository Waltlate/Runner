using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopRun : IComparable<TopRun>
{
    public int className;
    public int score;
    public string date;

    public enum EClass { Warrior = 1, Archer, Mage };
    public TopRun()
    {
        className = 0;
        score = 0;
        date = "--.--.--";
    }

    public int CompareTo(TopRun other)
    {
        if (other == null)
        {
            return 1;
        }
        Debug.Log("here");
        return this.score.CompareTo(other.score);
    }
}
