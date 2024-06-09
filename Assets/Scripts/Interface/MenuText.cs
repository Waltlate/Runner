using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuText : MonoBehaviour
{
    public static MenuText instance;
    public GameObject phone;
    private float attitude;
    public TextMeshProUGUI playButtonText;
    public TextMeshProUGUI heroesButtonText;
    public TextMeshProUGUI shopButtonText;
    public TextMeshProUGUI settenignsButtonText;
    public TextMeshProUGUI exitButtonText;

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
        phone.transform.localScale = new Vector2(attitude, attitude);
    }

    void Awake()
    {
        instance = this;
        ChangeLanguageAndRefresh();
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
