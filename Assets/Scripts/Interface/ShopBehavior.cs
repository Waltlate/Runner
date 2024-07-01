using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopBehavior : MonoBehaviour
{
    public static ShopBehavior instance;
    [SerializeField] private GameObject canvasInfo;
    [SerializeField] private GameObject canvasChestOpen;
    [SerializeField] private GameObject[] weaponIcon;
    [SerializeField] private TextMeshProUGUI[] textWeaponCount;
    [SerializeField] private Image currentWeaponImage;
    [SerializeField] private Sprite[] weaponImage;
    [SerializeField] private TextMeshProUGUI textInfo;
    private Button[] btns;
    [SerializeField] private TextMeshProUGUI[] textBuy;
    [SerializeField] private TextMeshProUGUI[] textCount;
    [SerializeField] private Button[] btnPlus;
    [SerializeField] private Button[] btnPlusPlus;
    [SerializeField] private Button[] btnMinus;
    [SerializeField] private Button[] btnMinusMinus;

    public void Start()
    {
        ChangeStateButton();
        ClearChests();
    }

    public void Awake()
    {
        instance = this;
    }

    public void ChangeStateButton()
    {
        btns = GetComponentsInChildren<Button>();
        if (btns.Length != 0)
        {
            if (PlayerParameters.Coins >= 1000)
            {
                SetInteractableBtn(true);
            }
            else
            {
                SetInteractableBtn(false);
            }
        }
    }

    private void SetInteractableBtn(bool state)
    {
        foreach (var elem in btns)
        {
            if (elem.name == "ButtonChest")
                elem.interactable = state;
        }
    }

    public void BuyCards()
    {
        if (PlayerParameters.instance)
            if (PlayerParameters.Coins >= 1000 * Int32.Parse(textCount[0].text))
            {
                PlayerParameters.Coins -= 1000 * Int32.Parse(textCount[0].text);
                int warriorCurrentExpWeapon = PlayerPrefs.GetInt("WarriorCurrentExpWeapon", 0);
                int archerCurrentExpWeapon = PlayerPrefs.GetInt("ArcherCurrentExpWeapon", 0);
                int mageCurrentExpWeapon = PlayerPrefs.GetInt("MageCurrentExpWeapon", 0);

                int[] num = new int[3];
                int[] sumNum = { 0, 0, 0 };
                int j;
                for (int i = 0; i < Int32.Parse(textCount[0].text); i++)
                {
                    j = new System.Random().Next(0, 3);
                    num[j % 3] = new System.Random().Next(0, 11);
                    num[(j + 1) % 3] = new System.Random().Next(0, 11 - num[j % 3]);
                    num[(j + 2) % 3] = 10 - num[j % 3] - num[(j + 1) % 3];
                    Debug.Log($"{num[0] + num[1] + num[2]} - {num[0]}, {num[1]}, {num[2]}");
                    sumNum[0] += num[0];
                    sumNum[1] += num[1];
                    sumNum[2] += num[2];
                }

                Debug.Log($"{sumNum[0] + sumNum[1] + sumNum[2]} - {sumNum[0]}, {sumNum[1]}, {sumNum[2]}");
                PlayerPrefs.SetInt("WarriorCurrentExpWeapon", warriorCurrentExpWeapon + sumNum[0]);
                PlayerPrefs.SetInt("ArcherCurrentExpWeapon", archerCurrentExpWeapon + sumNum[1]);
                PlayerPrefs.SetInt("MageCurrentExpWeapon", mageCurrentExpWeapon + sumNum[2]);

                if (PlayerParameters.archer.ClassName == "Warrior")
                {
                    PlayerParameters.archer.CurrentExpWeapon += sumNum[0];
                }
                if (PlayerParameters.archer.ClassName == "Archer")
                {
                    PlayerParameters.archer.CurrentExpWeapon += sumNum[1];
                }
                if (PlayerParameters.archer.ClassName == "Mage")
                {
                    PlayerParameters.archer.CurrentExpWeapon += sumNum[2];
                }

                if (HeroesText.instance)
                {
                    HeroesText.instance.ChangeLanguageAndRefresh();
                }
                if (ShopText.instance)
                {
                    ShopText.instance.ChangeLanguageAndRefresh();
                }
                if (WeaponBehavior.instance)
                {
                    WeaponBehavior.instance.ChangeStateButton();
                }
                ChangeStateButton();
                if (PlayerParameters.Coins != 0)
                    PlayerPrefs.SetInt("Coins", PlayerParameters.Coins);
                ClearChests();

                canvasChestOpen.SetActive(true);
                weaponIcon[1].SetActive(true);
                weaponIcon[2].SetActive(true);
                textWeaponCount[0].text = ConvertNumberToString(sumNum[1]);
                textWeaponCount[1].text = ConvertNumberToString(sumNum[0]);
                textWeaponCount[2].text = ConvertNumberToString(sumNum[2]);
                currentWeaponImage.sprite = weaponImage[1];
            }
    }

    public void BuyCardsWarrior()
    {
        if (PlayerParameters.instance)
            if (PlayerParameters.Coins >= 1000 * Int32.Parse(textCount[1].text))
            {
                BuyBase("Warrior", Int32.Parse(textCount[1].text));
                currentWeaponImage.sprite = weaponImage[0];
            }
    }

    public void BuyCardsArcher()
    {
        if (PlayerParameters.instance)
            if (PlayerParameters.Coins >= 1000 * Int32.Parse(textCount[2].text))
            {
                BuyBase("Archer", Int32.Parse(textCount[2].text));
                currentWeaponImage.sprite = weaponImage[1];
            }
    }

    public void BuyCardsMage()
    {
        if (PlayerParameters.instance)
            if (PlayerParameters.Coins >= 1000 * Int32.Parse(textCount[3].text))
            {
                BuyBase("Mage", Int32.Parse(textCount[3].text));
                currentWeaponImage.sprite = weaponImage[2];
            }
    }

    private void BuyBase(string className, int count)
    {
        PlayerParameters.Coins -= 1000 * count;
        int currentExpWeapon = PlayerPrefs.GetInt(className + "CurrentExpWeapon", 0);

        PlayerPrefs.SetInt(className + "CurrentExpWeapon", currentExpWeapon + 5 * count);
        if (PlayerParameters.archer.ClassName == className)
        {
            PlayerParameters.archer.CurrentExpWeapon += 5 * count;
        }
        if (HeroesText.instance)
        {
            HeroesText.instance.ChangeLanguageAndRefresh();
        }
        if (ShopText.instance)
        {
            ShopText.instance.ChangeLanguageAndRefresh();
        }
        if (WeaponBehavior.instance)
        {
            WeaponBehavior.instance.ChangeStateButton();
        }
        ChangeStateButton();
        if (PlayerParameters.Coins != 0)
            PlayerPrefs.SetInt("Coins", PlayerParameters.Coins);
        ClearChests();

        canvasChestOpen.SetActive(true);
        weaponIcon[1].SetActive(false);
        weaponIcon[2].SetActive(false);
        textWeaponCount[0].text = ConvertNumberToString(5 * count);
    }

    public void ClearChests()
    {
        for (int i = 0; i < btnPlus.Length; i++)
        {
            textCount[i].text = ConvertNumberToString(1);
            textBuy[i].text = ConvertNumberToString(1000);
        }
        for (int i = 0; i < btnPlus.Length; i++)
        {
            ChangeStateButtonsCount(i);
        }
        for (int i = 0; i < btnPlus.Length; i++)
        {
            ChangeStateButtonsCount(i);
        }
    }

    public void InfoChestHeroes()
    {
        canvasInfo.SetActive(true);
        textInfo.text = LanguageSettenings.ls.infoChestHeroes;
    }

    public void InfoChestWarrior()
    {
        canvasInfo.SetActive(true);
        textInfo.text = LanguageSettenings.ls.infoChestWarrior;
    }

    public void InfoChestArcher()
    {
        canvasInfo.SetActive(true);
        textInfo.text = LanguageSettenings.ls.infoChestArcher;
    }

    public void InfoChestMage()
    {
        canvasInfo.SetActive(true);
        textInfo.text = LanguageSettenings.ls.infoChestMage;
    }

    public void CountChestPlus(int number)
    {
        int maxCount = PlayerParameters.Coins / 1000;
        int count = Int32.Parse(textCount[number].text);
        if (count < maxCount)
        {
            count++;
            textBuy[number].text = ConvertNumberToString(count * 1000);
            textCount[number].text = count.ToString();
        }
        ChangeStateButtonsCount(number);
    }

    public void CountChestPlusPlus(int number)
    {
        int maxCount = PlayerParameters.Coins / 1000;
        int count = Int32.Parse(textCount[number].text);
        if (count < maxCount)
        {
            count += 10;
            if (count > maxCount) count = maxCount;
            textBuy[number].text = ConvertNumberToString(count * 1000);
            textCount[number].text = count.ToString();
        }
        ChangeStateButtonsCount(number);
    }

    public void CountChestMinus(int number)
    {
        int count = Int32.Parse(textCount[number].text);
        if (count > 1)
        {
            count--;
            textBuy[number].text = ConvertNumberToString(count * 1000);
            textCount[number].text = count.ToString();
        }
        ChangeStateButtonsCount(number);
    }

    public void CountChestMinusMinus(int number)
    {
        int count = Int32.Parse(textCount[number].text);
        if (count > 1)
        {
            count -= 10;
            if (count < 1) count = 1;
            textBuy[number].text = ConvertNumberToString(count * 1000);
            textCount[number].text = count.ToString();
        }
        ChangeStateButtonsCount(number);
    }

    public void ChangeStateButtonsCount(int number)
    {
        int maxCount = PlayerParameters.Coins / 1000;
        int count = Int32.Parse(textCount[number].text);
        if (count < maxCount)
        {
            ChangeStateButton(btnPlus[number], Color.green, true);
            ChangeStateButton(btnPlusPlus[number], Color.green, true);
        }
        else
        {
            ChangeStateButton(btnPlus[number], Color.red, false);
            ChangeStateButton(btnPlusPlus[number], Color.red, false);
        }

        if (count > 1)
        {
            ChangeStateButton(btnMinus[number], Color.green, true);
            ChangeStateButton(btnMinusMinus[number], Color.green, true);
        }
        else
        {
            ChangeStateButton(btnMinus[number], Color.red, false);
            ChangeStateButton(btnMinusMinus[number], Color.red, false);
        }
    }

    public void ChangeStateButton(Button btn, Color color, bool state)
    {
        btn.interactable = state;
        btn.gameObject.transform.Find("Sign").gameObject.GetComponent<Image>().color = color;
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
