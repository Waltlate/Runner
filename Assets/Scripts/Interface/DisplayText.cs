using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayText : MonoBehaviour
{
    public static DisplayText instance;
    public GameObject phone;
    private float attitude;
    public TextMeshProUGUI pauseButtonText;

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
    }

    public void ChangeLanguageAndRefresh()
    {
        pauseButtonText.text = LanguageSettenings.ls.pause;
    }
}
