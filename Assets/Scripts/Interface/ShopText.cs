using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopText : MonoBehaviour
{
    public static ShopText instance;
    [SerializeField] private Image image;
    private float attitude;
    [SerializeField] private TextMeshProUGUI shopText;
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI textChestHeroes;
    [SerializeField] private TextMeshProUGUI textChestWarrior;
    [SerializeField] private TextMeshProUGUI textChestArcher;
    [SerializeField] private TextMeshProUGUI textChestMage;
    [SerializeField] private TextMeshProUGUI[] buyText;
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
