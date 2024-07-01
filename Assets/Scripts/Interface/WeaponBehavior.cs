using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponBehavior : MonoBehaviour
{
    public static WeaponBehavior instance;
    private Button btn;

    void Start()
    {
        ChangeStateButton();
    }

    void Awake()
    {
        instance = this;
    }

    public void UpgradeWeapon()
    {
        if(PlayerParameters.archer.CurrentExpWeapon >= PlayerParameters.archer.LevelExpWeapon)
        {
            if (PlayerParameters.instance)
            {
                PlayerParameters.archer.CurrentExpWeapon -= PlayerParameters.archer.LevelExpWeapon;
                PlayerParameters.archer.LevelWeapon++;
                PlayerParameters.archer.LevelExpWeapon = PlayerParameters.instance.ExpLevelWeapon(PlayerParameters.archer.LevelWeapon);
                PlayerParameters.archer.Damage = PlayerParameters.archer.Level + PlayerParameters.archer.LevelWeapon - 1;
                PlayerPrefs.SetInt(PlayerParameters.archer.ClassName + "CurrentExpWeapon", PlayerParameters.archer.CurrentExpWeapon);
                PlayerPrefs.SetInt(PlayerParameters.archer.ClassName + "LevelWeapon", PlayerParameters.archer.LevelWeapon);
                PlayerPrefs.SetInt(PlayerParameters.archer.ClassName + "LevelExpWeapon", PlayerParameters.archer.LevelExpWeapon);
            }
            if (HeroesText.instance)
            {
                HeroesText.instance.ChangeLanguageAndRefresh();
            }
        }
        ChangeStateButton();
    }

    public void ChangeStateButton() {
        btn = GetComponent<Button>();
        if (btn)
        {
            if (PlayerParameters.archer.CurrentExpWeapon >= PlayerParameters.archer.LevelExpWeapon)
            {
                btn.interactable = true;
                btn.gameObject.transform.Find("LevelUpText").gameObject.GetComponent<TextMeshProUGUI>().color = new Color(0, 1, 0, 1);
            }
            else
            {
                btn.interactable = false;
                btn.gameObject.transform.Find("LevelUpText").gameObject.GetComponent<TextMeshProUGUI>().color = new Color(1, 0, 0, 1);
            }
        }
    }
}
