using TMPro;
using UnityEngine;

public class MenuText : MonoBehaviour
{
    public static MenuText instance;
    [SerializeField] private GameObject phone;
    private float attitude;
    [SerializeField] private TextMeshProUGUI playButtonText;
    [SerializeField] private TextMeshProUGUI heroesButtonText;
    [SerializeField] private TextMeshProUGUI shopButtonText;
    [SerializeField] private TextMeshProUGUI settenignsButtonText;
    [SerializeField] private TextMeshProUGUI mainMenuButtonText;

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
        playButtonText.text = LanguageSettenings.ls.startRun;
        heroesButtonText.text = LanguageSettenings.ls.heroes;
        shopButtonText.text = LanguageSettenings.ls.shop;
        settenignsButtonText.text = LanguageSettenings.ls.settenigns;
        mainMenuButtonText.text = LanguageSettenings.ls.mainMenu;
    }
}
