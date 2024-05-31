using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopText : MonoBehaviour
{
    public static ShopText instance;
    public Image image;
    public TextMeshProUGUI backButtonText;
    public TextMeshProUGUI shopText;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI[] buyText;
    private Color color;

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
        shopText.text = LanguageSettenings.ls.shop;
        coinText.text = $"{PlayerParameters.Coins}"; ;

        if(PlayerParameters.Coins >= 1000)
        {
            color = new Color(0, 1, 0);
        } else
        {
            color = new Color(1, 0, 0);
        }

        for (int i = 0; i < buyText.Length; i++)
        {
            buyText[i].color = color;
        }
    }
}
