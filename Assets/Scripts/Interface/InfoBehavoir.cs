using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoBehavoir : MonoBehaviour
{
    public static InfoBehavoir instance;
    public TextMeshProUGUI closeText;

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
