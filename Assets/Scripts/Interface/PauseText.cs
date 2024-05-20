using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PauseText : MonoBehaviour
{
    public static PauseText instance;
    public TextMeshProUGUI continueButtonText;
    public TextMeshProUGUI restartButtonText;

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
        continueButtonText.text = LanguageSettenings.ls.continueBtn;
        restartButtonText.text = LanguageSettenings.ls.restart;
    }
}
