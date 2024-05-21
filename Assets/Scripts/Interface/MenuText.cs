using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuText : MonoBehaviour
{
    public static MenuText instance;
    public TextMeshProUGUI playButtonText;
    public TextMeshProUGUI heroesButtonText;
    public TextMeshProUGUI shopButtonText;
    public TextMeshProUGUI settenignsButtonText;
    public TextMeshProUGUI exitButtonText;

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
        playButtonText.text = LanguageSettenings.ls.play;
        heroesButtonText.text = LanguageSettenings.ls.heroes;
        shopButtonText.text = LanguageSettenings.ls.shop;
        settenignsButtonText.text = LanguageSettenings.ls.settenigns;
        exitButtonText.text = LanguageSettenings.ls.exit;
    }
}
