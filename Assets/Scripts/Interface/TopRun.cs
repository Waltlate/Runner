using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopRun : IComparable<TopRun>
{
    public string className;
    public int score;
    public string date;

    public TopRun()
    {
        className = "---";
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

        // Сравниваем объекты по значению score
        return this.score.CompareTo(other.score);
    }
}
