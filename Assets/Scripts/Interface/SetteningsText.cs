using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class SetteningsText : MonoBehaviour
{
    public static SetteningsText instance;
    public TextMeshProUGUI backButtonText;
    public TextMeshProUGUI tutorialText;

    void Start()
    {
        ChangeLanguageAndRefresh();
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
