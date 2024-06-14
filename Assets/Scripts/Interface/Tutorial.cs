using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;

public class Tutorial : MonoBehaviour
{
    public static Tutorial instance;
    public static bool trigerTutorial = false;
    public bool greatings;
    public bool enemyTrigger;
    public bool enemyExist;
    private float enemyCoordinate;
    public bool enemyFlyTrigger;
    public bool enemyFlyExist;
    private float enemyFlyCoordinate;
    public bool trapTrigger;
    public bool trapExist;
    private float trapCoordinate;
    public bool flyTrapTrigger;
    public bool flyTrapExist;
    private float flyTrapCoordinate;
    public bool coinTrigger;
    public bool coinExist;
    private float coinCoordinate;
    public bool perkTrigger;
    public bool perkExist;
    private float perkCoordinate;
    private int countGreatings = 0;
    private float currentTimeScale = 0;
    GUIStyle headStyle = new GUIStyle();
    float k = 1; //koef size box
    float step = 0.0003f;
    private string[] greatingsText;
    private string enemyText;
    private string enemyFlyText;
    private string coinText;
    private string trapText;
    private string flyTrapText;
    private string perkText;
    private float attitude;
    private float screenWight = 720;
    private float screenHeight = 1280;

    private void Start()
    {
        if (((float)Screen.height / 1280f) * 720f < (float)Screen.width)
        {
            attitude = (float)Screen.height / 1280f;
        }
        else
        {
            attitude = (float)Screen.width / 720f;
        }
        TextLoad();

        if (trigerTutorial)
        {
            OnTutorial();
        }

        headStyle.alignment = TextAnchor.MiddleCenter;
        headStyle.fontSize = ((int)(30 * attitude));
        headStyle.wordWrap = true;
        headStyle.normal.textColor = Color.magenta;
        headStyle.fontStyle = FontStyle.Bold;
    }

    public void TextLoad()
    {
        greatingsText = LanguageSettenings.ls.greatings;
        enemyText = LanguageSettenings.ls.enemyText;
        enemyFlyText = LanguageSettenings.ls.enemyFlyText;
        coinText = LanguageSettenings.ls.coinText;
        trapText = LanguageSettenings.ls.trapText;
        flyTrapText = LanguageSettenings.ls.flyTrapText;
        perkText = LanguageSettenings.ls.perkText;
    }

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (greatings || enemyExist)
        {
            StartCoroutine(StopTime());
        }

        if (Input.anyKeyDown && greatings) {
            countGreatings++;
        }

        if(countGreatings == greatingsText.Length && greatings) {
            greatings = false;
            StartCoroutine(RecoveryTime());
        }

        if (Input.anyKeyDown && enemyExist && enemyTrigger)
        {
            enemyTrigger = enemyExist = false;
            StartCoroutine(RecoveryTime());
        }

        if (Input.anyKeyDown && enemyFlyExist && enemyFlyTrigger && !enemyExist)
        {
            enemyFlyTrigger = enemyFlyExist = false;
            StartCoroutine(RecoveryTime());
        }

        if (Input.anyKeyDown && coinExist && coinTrigger && !enemyExist && !enemyFlyExist)
        {
            coinTrigger = coinExist = false;
            StartCoroutine(RecoveryTime());
        }

        if (Input.anyKeyDown && trapExist && trapTrigger && !enemyExist && !enemyFlyExist && !coinExist)
        {
            trapTrigger = trapExist = false;
            StartCoroutine(RecoveryTime());
        }

        if (Input.anyKeyDown && perkExist && perkTrigger && !enemyExist && !enemyFlyExist && !coinExist && !trapExist)
        {
            perkTrigger = perkExist = false;
            StartCoroutine(RecoveryTime());
        }

        if (Input.anyKeyDown && flyTrapExist && flyTrapTrigger && !enemyExist && !enemyFlyExist && !coinExist && !trapExist && !perkExist)
        {
            flyTrapTrigger = flyTrapExist = false;
            StartCoroutine(RecoveryTime());
        }

        if (!(greatings || enemyTrigger || enemyFlyTrigger || coinTrigger || trapTrigger || perkTrigger || flyTrapTrigger))
        {
            trigerTutorial = false;
            PlayerPrefs.SetInt("TrigerTutorial", 0);
        }

        if (!trigerTutorial)
        {
            this.gameObject.SetActive(false);
        }
    }

    public void OnGUI()
    {
        if (greatings && countGreatings < greatingsText.Length)
        {
            GUI.Label(CreateRect(screenWight * 0.1f, screenHeight / 2 - 450, screenWight * 0.8f, 500), greatingsText[countGreatings], headStyle);

            if(k > 1.05f || k < 0.95f)
            {
                step *= -1;
            }
            k += step;
            if (countGreatings == 2) GUI.Box(CreateRect(100 - (200 * (k - 1)) / 2, 210 - (60 * (k - 1)) / 2, 200 * k, 60 * k), "");
            if (countGreatings == 3) GUI.Box(CreateRect(screenWight - 270 - (250 * (k - 1)) / 2, 270 - (60 * (k - 1)) / 2, 250 * k, 60 * k), "");
        }

        if (enemyExist && enemyTrigger)
        {
            if (k > 1.05f || k < 0.95f)
            {
                step *= -1;
            }
            k += step;
            GUI.Box(CreateRect(10 - (350 * (k - 1)) / 2, 125 - (60 * (k - 1)) / 2, 350 * k, 60 * k), "");
            GUI.Label(CreateRect(screenWight * 0.1f, screenHeight / 2 - 450, screenWight * 0.8f, 500), enemyText, headStyle);
            GUI.Box(CreateRect(screenWight / 2 - 150 / 2 + enemyCoordinate * 80,
                             screenHeight / 2  - 150 / 2 + 210,
                             150, 150), "");
        }

        if (enemyFlyExist && enemyFlyTrigger && !enemyExist)
        {
            GUI.Label(CreateRect(screenWight * 0.1f, screenHeight / 2 - 450, screenWight * 0.8f, 500), enemyFlyText, headStyle);
            GUI.Box(CreateRect(screenWight / 2 - 150 / 2 + enemyFlyCoordinate * 80,
                             screenHeight / 2 - 150 / 2 + 230 - 210,
                             150, 150), "");
        }

        if (coinExist && coinTrigger && !enemyExist && !enemyFlyExist)
        {
            GUI.Label(CreateRect(screenWight * 0.1f, screenHeight / 2 - 450, screenWight * 0.8f, 500), coinText, headStyle);
            GUI.Box(CreateRect(screenWight / 2 - 150 / 2 + coinCoordinate * 80,
                             screenHeight / 2  - 150 / 2 + 230,
                             150, 150), "");
        }

        if (trapExist && trapTrigger && !enemyExist && !enemyFlyExist && !coinExist)
        {
            GUI.Label(CreateRect(screenWight * 0.1f, screenHeight / 2 - 450, screenWight * 0.8f, 500), trapText, headStyle);
            GUI.Box(CreateRect(screenWight / 2 - 150 / 2 + trapCoordinate * 80,
                             screenHeight / 2 - 150 / 2 + 230,
                             150, 150), "");
        }

        if (perkExist && perkTrigger && !enemyExist && !enemyFlyExist && !coinExist && !trapExist)
        {
            GUI.Label(CreateRect(screenWight * 0.1f, screenHeight / 2 - 450, screenWight * 0.8f, 500), perkText, headStyle);
            GUI.Box(CreateRect(screenWight / 2 - 150 / 2 + perkCoordinate * 80,
                             screenHeight / 2 - 150 / 2 + 230,
                             150, 150), "");
        }

        if (flyTrapExist && flyTrapTrigger && !enemyExist && !enemyFlyExist && !coinExist && !trapExist && !perkExist)
        {
            GUI.Label(CreateRect(screenWight * 0.1f, screenHeight / 2 - 450, screenWight * 0.8f, 500), flyTrapText, headStyle);
            GUI.Box(CreateRect(screenWight / 2 - 150 / 2 + flyTrapCoordinate * 80,
                             screenHeight / 2 - 150 / 2 + 200,
                             150, 150), "");
        }
    }

    private Rect CreateRect(float x, float y, float width, float height)
    {
        return new Rect(x * attitude + (Screen.width - 720 * attitude) / 2, y * attitude + (Screen.height - 1280 * attitude) / 2, width * attitude, height * attitude);
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.name == "Enemy" || other.gameObject.name == "Enemys") && enemyTrigger)
        {
            enemyExist = true;
            enemyCoordinate = other.gameObject.transform.position.x;
            StartCoroutine(StopTime());
        }

        if (other.gameObject.name == "EnemyFly" && enemyFlyTrigger)
        {
            enemyFlyExist = true;
            enemyFlyCoordinate = other.gameObject.transform.position.x;
            StartCoroutine(StopTime());
        }

        if (other.gameObject.name == "Coin" && coinTrigger)
        {
            coinExist = true;
            coinCoordinate = other.gameObject.transform.position.x;
            StartCoroutine(StopTime());
        }

        if (other.gameObject.name == "Trap" && trapTrigger)
        {
            trapExist = true;
            trapCoordinate = other.gameObject.transform.position.x;
            StartCoroutine(StopTime());
        }

        if ((other.gameObject.name == "Perk") && perkTrigger)
        {
            perkExist = true;
            perkCoordinate = other.gameObject.transform.position.x;
            StartCoroutine(StopTime());
        }

        if ((other.gameObject.name == "FlyTrap") && flyTrapTrigger)
        {
            flyTrapExist = true;
            flyTrapCoordinate = other.gameObject.transform.position.x;
            StartCoroutine(StopTime());
        }
    }

    public void OnTutorial()
    {
        trigerTutorial = true;
        greatings = true;
        enemyTrigger = true;
        enemyFlyTrigger = true;
        coinTrigger = true;
        trapTrigger = true;
        flyTrapTrigger = true;
        perkTrigger = true;
    }

    public void OffTutorial()
    {
        trigerTutorial = false;
        greatings = false;
        enemyTrigger = false;
        enemyFlyTrigger = false;
        coinTrigger = false;
        trapTrigger = false;
        flyTrapTrigger = false;
        perkTrigger = false;
    }

    IEnumerator StopTime()
    {
        if ((Time.timeScale - 0.0f) > 0.0000001f)
        {
            currentTimeScale = Time.timeScale;
            Time.timeScale = 0;
        }
        PlayerController.instance.AudioStop();
        yield return new WaitForSeconds(0.05f);
    }

    IEnumerator RecoveryTime()
    {
        Time.timeScale = currentTimeScale;
        PlayerController.instance.rb.AddForce(Vector3.up * 1f, ForceMode.Impulse);
        PlayerController.instance.AudioPlay();
        yield return new WaitForSeconds(0.05f);
    }
}
