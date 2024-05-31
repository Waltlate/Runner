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
    public TextMeshProUGUI backButtonText;
    public TextMeshProUGUI tutorialText;

    void Start()
    {
        ChangeLanguageAndRefresh();
        image.GetComponent<RectTransform>().transform.localScale = new Vector2(Screen.width / 720f, Screen.height / 1280f);
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
