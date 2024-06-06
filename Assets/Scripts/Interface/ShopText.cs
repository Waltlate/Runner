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
    public TextMeshProUGUI shopText;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI textChestHeroes;
    public TextMeshProUGUI textChestWarrior;
    public TextMeshProUGUI textChestArcher;
    public TextMeshProUGUI textChestMage;
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
        shopText.text = LanguageSettenings.ls.shop;
        coinText.text = ConvertNumberToString(PlayerParameters.Coins);
        textChestHeroes.text = LanguageSettenings.ls.chestHeroes;
        textChestWarrior.text = LanguageSettenings.ls.chestWarrior;
        textChestArcher.text = LanguageSettenings.ls.chestArcher;
        textChestMage.text = LanguageSettenings.ls.chestMage;

        if (PlayerParameters.Coins >= 1000)
        {
            color = Color.green;
        }
        else
        {
            color = Color.red;
        }

        for (int i = 0; i < buyText.Length; i++)
        {
            buyText[i].color = color;
        }
    }

    string ConvertNumberToString(float number)
    {
        const string k = "k";
        const string m = "m";
        const string b = "b";

        if (number < 9999)
        {
            return number.ToString("###0.");
        }
        else if (number < 1000000)
        {
            return (number / 1000f).ToString("0.##") + k;
        }
        else if (number < 1000000000)
        {
            return (number / 1000000f).ToString("0.###") + m;
        }
        else
        {
            return (number / 1000000000f).ToString("0.####") + b;
        }
    }
}
