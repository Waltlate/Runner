using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;

public class Tutorial : MonoBehaviour
{
    public static Tutorial instance;
    public static bool trigerTutorial = true;
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
    private string[] greatingsText = { "Приветствую, приключенец. Здесь начинается твой забег",
                                   "Перемещайся влево, вправо и вверх, чтобы уклоняться от врагов и ловушек",
                                   "Если не сможешь увернуться, то твое здоровье будет уменьшаться",
                                   "Со временем твой счет будет увеличиваться. Пробеги как можно дальше пока твое здоровье не опустилось до нуля"};
    private string enemyText = "На твоем пути будут встречаться враги. Ты можешь победить врагов, которые ниже твоего уровня своим оружием. Противники посильнее будут наносить урон соответствующий их уровню";
    private string enemyFlyText = "В игре появляются летающие враги. Их уровень равен уровеню мира и они наносят урон в соответствии своему уровню";
    private string coinText = "Собирай монетки, чтобы улучшать героев";
    private string trapText = "Избегай ловушки. Их уровень равен уровеню мира и они наносят урон в соответствии своему уровню";
    private string perkText = "Подбирай бонусы. Они могут улучшить твой счет или подлечить тебя";

    private void Start()
    {
        TextLoad();
        if (trigerTutorial)
        {
            greatings = true;
            enemyTrigger = true;
            enemyFlyTrigger = true;
            coinTrigger = true;
            trapTrigger = true;
            perkTrigger = true;
        }
        headStyle.alignment = TextAnchor.MiddleCenter;
        headStyle.fontSize = 30;
        headStyle.wordWrap = true;
    }

    void TextLoad()
    {
        greatingsText = LanguageSettenings.ls.greatings;
        enemyText = LanguageSettenings.ls.enemyText;
        enemyFlyText = LanguageSettenings.ls.enemyFlyText;
        coinText = LanguageSettenings.ls.coinText;
        trapText = LanguageSettenings.ls.trapText;
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
            StopTime();
        }

        if (Input.anyKeyDown && greatings) {
            countGreatings++;
        }

        if(countGreatings == greatingsText.Length && greatings) {
            greatings = false; 
            Time.timeScale = currentTimeScale;
        }

        if (Input.anyKeyDown && enemyExist && enemyTrigger)
        {
            enemyTrigger = enemyExist = false;
            Time.timeScale = currentTimeScale;
        }

        if (Input.anyKeyDown && enemyFlyExist && enemyFlyTrigger && !enemyExist)
        {
            enemyFlyTrigger = enemyFlyExist = false;
            Time.timeScale = currentTimeScale;
        }

        if (Input.anyKeyDown && coinExist && coinTrigger && !enemyExist && !enemyFlyExist)
        {
            coinTrigger = coinExist = false;
            Time.timeScale = currentTimeScale;
        }

        if (Input.anyKeyDown && trapExist && trapTrigger && !enemyExist && !enemyFlyExist && !coinExist)
        {
            trapTrigger = trapExist = false;
            Time.timeScale = currentTimeScale;
        }

        if (Input.anyKeyDown && perkExist && perkTrigger && !enemyExist && !enemyFlyExist && !coinExist && !trapExist)
        {
            perkTrigger = perkExist = false;
            Time.timeScale = currentTimeScale;
        }

        if (!(greatings || enemyTrigger || enemyFlyTrigger || coinTrigger || trapTrigger || perkTrigger))
        {
            trigerTutorial = false;
            //ToggleController.instance.OffToggle();
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
            GUI.Label(new Rect(Screen.width * 0.1f, Screen.height / 2 - 450, Screen.width * 0.8f, 500), greatingsText[countGreatings], headStyle);

            if(k > 1.05f || k < 0.95f)
            {
                step *= -1;
            }
            k += step;
            if (countGreatings == 2) GUI.Box(new Rect(10 - (180 * (k - 1)) / 2, 180 - (60 * (k - 1)) / 2, 180 * k, 60 * k), "");
            if (countGreatings == 3) GUI.Box(new Rect(Screen.width - 350 - (250 * (k - 1)) / 2, 180 - (60 * (k - 1)) / 2, 250 * k, 60 * k), "");
        }

        if (enemyExist && enemyTrigger)
        {
            if (k > 1.05f || k < 0.95f)
            {
                step *= -1;
            }
            k += step;
            GUI.Box(new Rect(10 - (350 * (k - 1)) / 2, 125 - (60 * (k - 1)) / 2, 350 * k, 60 * k), "");
            GUI.Label(new Rect(Screen.width * 0.1f, Screen.height / 2 - 450, Screen.width * 0.8f, 500), enemyText, headStyle);
            GUI.Box(new Rect(Screen.width / 2 - 150 / 2 + enemyCoordinate * 80,
                             Screen.height / 2  - 150 / 2 + 230,
                             150, 150), "");
        }

        if (enemyFlyExist && enemyFlyTrigger && !enemyExist)
        {
            GUI.Label(new Rect(Screen.width * 0.1f, Screen.height / 2 - 450, Screen.width * 0.8f, 500), enemyFlyText, headStyle);
            GUI.Box(new Rect(Screen.width / 2 - 150 / 2 + enemyFlyCoordinate * 80,
                             Screen.height / 2 - 150 / 2 + 230 - 170,
                             150, 150), "");
        }

        if (coinExist && coinTrigger && !enemyExist && !enemyFlyExist)
        {
            GUI.Label(new Rect(Screen.width * 0.1f, Screen.height / 2 - 450, Screen.width * 0.8f, 500), coinText, headStyle);
            GUI.Box(new Rect(Screen.width / 2 - 150 / 2 + coinCoordinate * 80,
                             Screen.height / 2  - 150 / 2 + 230,
                             150, 150), "");
        }

        if (trapExist && trapTrigger && !enemyExist && !enemyFlyExist && !coinExist)
        {
            GUI.Label(new Rect(Screen.width * 0.1f, Screen.height / 2 - 450, Screen.width * 0.8f, 500), trapText, headStyle);
            GUI.Box(new Rect(Screen.width / 2 - 150 / 2 + trapCoordinate * 80,
                             Screen.height / 2 - 150 / 2 + 230,
                             150, 150), "");
        }

        if (perkExist && perkTrigger && !enemyExist && !enemyFlyExist && !coinExist && !trapExist)
        {
            GUI.Label(new Rect(Screen.width * 0.1f, Screen.height / 2 - 450, Screen.width * 0.8f, 500), perkText, headStyle);
            GUI.Box(new Rect(Screen.width / 2 - 150 / 2 + perkCoordinate * 80,
                             Screen.height / 2 - 150 / 2 + 230,
                             150, 150), "");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.name == "Enemy" || other.gameObject.name == "Enemys") && enemyTrigger)
        {
            enemyExist = true;
            enemyCoordinate = other.gameObject.transform.position.x;
            StopTime();
        }

        if (other.gameObject.name == "EnemyFly" && enemyFlyTrigger)
        {
            enemyFlyExist = true;
            enemyFlyCoordinate = other.gameObject.transform.position.x;
            StopTime();
        }

        if (other.gameObject.name == "Coin" && coinTrigger)
        {
            coinExist = true;
            coinCoordinate = other.gameObject.transform.position.x;
            StopTime();
        }

        if (other.gameObject.name == "Trap" && trapTrigger)
        {
            trapExist = true;
            trapCoordinate = other.gameObject.transform.position.x;
            StopTime();
        }

        if ((other.gameObject.name == "Perk") && perkTrigger)
        {
            perkExist = true;
            perkCoordinate = other.gameObject.transform.position.x;
            StopTime();
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
        perkTrigger = false;
    }

    private void StopTime()
    {
        if ((Time.timeScale - 0) > 0.0000001f)
            currentTimeScale = Time.timeScale;
        Time.timeScale = 0;
    }
}
