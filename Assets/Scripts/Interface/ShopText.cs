using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopText : MonoBehaviour
{
    public static ShopText instance;
    public Image image;
    private float attitude;
    public TextMeshProUGUI backButtonText;
    public TextMeshProUGUI shopText;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI[] buyText;
    private Color color;

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
