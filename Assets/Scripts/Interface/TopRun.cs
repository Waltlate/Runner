using System;

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
        return this.score.CompareTo(other.score);
    }
}
