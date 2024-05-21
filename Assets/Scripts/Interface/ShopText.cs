using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopText : MonoBehaviour
{
    public static ShopText instance;
    public TextMeshProUGUI backButtonText;
    public TextMeshProUGUI shopText;
    public TextMeshProUGUI[] buyText;

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
        shopText.text = LanguageSettenings.ls.shop;
        for(int i = 0; i < buyText.Length; i++)
        {
            buyText[i].text = LanguageSettenings.ls.buy;
        }
    }
}
