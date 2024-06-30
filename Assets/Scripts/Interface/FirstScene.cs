using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstScene : MonoBehaviour
{
    public static FirstScene instance;
    [SerializeField] private GameObject phone;
    private float attitude;
    [SerializeField] private TextMeshProUGUI playButtonText;
    [SerializeField] private TextMeshProUGUI topRunButtonText;
    [SerializeField] private TextMeshProUGUI exitButtonText;
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject topRun;
    private string gameScene = "GameScene";

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
        playButtonText.text = LanguageSettenings.ls.play;
        topRunButtonText.text = LanguageSettenings.ls.topRun;
        exitButtonText.text = LanguageSettenings.ls.exit;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(gameScene);
    }
    public void TopRunGame()
    {
        menu.SetActive(false);
        topRun.SetActive(true);
    }

    public void BackButton()
    {
        topRun.SetActive(false);
        menu.SetActive(true);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
