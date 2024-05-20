using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayText : MonoBehaviour
{
    public static DisplayText instance;
    public TextMeshProUGUI pauseButtonText;

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
        pauseButtonText.text = LanguageSettenings.ls.pause;
    }
}
