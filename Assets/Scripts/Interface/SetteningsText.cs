using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
//using UnityEngine.UIElements;
using UnityEngine.UI;

public class SetteningsText : MonoBehaviour
{
    public static SetteningsText instance;
    public Image image;
    private float attitude;
    public TextMeshProUGUI backButtonText;
    public TextMeshProUGUI tutorialText;

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
        backButtonText.text = LanguageSettenings.ls.back;
        tutorialText.text = LanguageSettenings.ls.startTutorial;
    }
}
