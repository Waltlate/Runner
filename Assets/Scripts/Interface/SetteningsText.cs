using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetteningsText : MonoBehaviour
{
    public static SetteningsText instance;
    [SerializeField] private TextMeshProUGUI textTutorial;
    [SerializeField] private TextMeshProUGUI textSettings;
    [SerializeField] private Image image;
    private float attitude;

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
        textTutorial.text = LanguageSettenings.ls.startTutorial;
        textSettings.text = LanguageSettenings.ls.settenigns;
    }
}
