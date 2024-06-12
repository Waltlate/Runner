using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PauseText : MonoBehaviour
{
    public static PauseText instance;
    public GameObject phone;
    private float attitude;
    public TextMeshProUGUI continueButtonText;
    public TextMeshProUGUI restartButtonText;
    public TextMeshProUGUI backMenuButtonText;

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
    }

    public void ChangeLanguageAndRefresh()
    {
        continueButtonText.text = LanguageSettenings.ls.continueBtn;
        restartButtonText.text = LanguageSettenings.ls.restart;
        backMenuButtonText.text = LanguageSettenings.ls.backMenu;
    }

}
