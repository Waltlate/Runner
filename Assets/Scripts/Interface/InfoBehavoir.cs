using TMPro;
using UnityEngine;

public class InfoBehavoir : MonoBehaviour
{
    public static InfoBehavoir instance;
    [SerializeField] private TextMeshProUGUI closeText;

    void Start()
    {
        ChangeLanguageAndRefresh();
    }

    void Awake()
    {
        instance = this;
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void ChangeLanguageAndRefresh()
    {
        closeText.text = LanguageSettenings.ls.close;
    }
}
