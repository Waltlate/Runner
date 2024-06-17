using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TopRunText : MonoBehaviour
{
    public static TopRunText instance;
    public TextMeshProUGUI textTopRun;
    public TextMeshProUGUI textClass;
    public TextMeshProUGUI textScore;
    public TextMeshProUGUI textDate;
    public TextMeshProUGUI[] classNames;
    public TextMeshProUGUI[] scores;
    public TextMeshProUGUI[] dates;
    public Image image;
    private float attitude;
    private TopRun[] topRuns = new TopRun[3];

    void Start()
    {
        ChangeLanguageAndRefresh();

        if (((float)Screen.height / 1280f) * 720f < (float)Screen.width)
        {
            attitude = (float)Screen.height / 1280f;
        }
        else
        {
            attitude = (float)Screen.width / 720f;
        }
        image.GetComponent<RectTransform>().transform.localScale = new Vector2(attitude, attitude);
    }

    void Awake()
    {
        instance = this;
    }

    public void ChangeLanguageAndRefresh()
    {
        textTopRun.text = LanguageSettenings.ls.topRun;
        textClass.text = LanguageSettenings.ls.classText;
        textScore.text = LanguageSettenings.ls.score;
        textDate.text = LanguageSettenings.ls.dateText;
        GetTopRun();
        if (classNames.Length == 3)
        {
            for (int i = 0; i < 3; i++)
            {
                classNames[i].text = GetClassName(topRuns[i].className);
                scores[i].text = topRuns[i].score.ToString();
                dates[i].text = topRuns[i].date;
            }
        }
    }

    private string GetClassName(int numberClass)
    {
        if (numberClass == 1) return LanguageSettenings.ls.classNameWarrior;
        if (numberClass == 2) return LanguageSettenings.ls.classNameArcher;
        if (numberClass == 3) return LanguageSettenings.ls.classNameMage;
        return "---";
    }

    private void GetTopRun()
    {
        for (int i = 0; i < 3; i++)
        {
            topRuns[i] = new TopRun();
            topRuns[i].className = PlayerPrefs.GetInt("ClassName" + i, 0);
            topRuns[i].score = PlayerPrefs.GetInt("TopRunScore" + i, 0);
            topRuns[i].date = PlayerPrefs.GetString("TopRunDate" + i, "--.--.--");
        }
    }
}
