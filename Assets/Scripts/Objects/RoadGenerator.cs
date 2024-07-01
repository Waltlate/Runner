using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class RoadGenerator : MonoBehaviour
{
    public static RoadGenerator instance;
    [SerializeField] private GameObject[] RoadPrefab;
    private List<GameObject> roads = new();
    [SerializeField] private float maxSpeed = 10;
    [HideInInspector] public float speed = 0;
    private float currentTime = 1f;
    [SerializeField] private float maxSpeedGame = 3f;
    [SerializeField] private float differenceSpeedGame = 0.0001f;
    private int maxRoadCount = 6;
    [HideInInspector] public bool isPlaying = false;
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject pause;
    [SerializeField] private GameObject displayGame;
    [SerializeField] private GameObject heroes;
    [SerializeField] private GameObject shop;
    [SerializeField] private GameObject settingsGames;
    [SerializeField] private GameObject perk;
    [SerializeField] private GameObject tutorial;
    [SerializeField] private GameObject[] Objects;
    [SerializeField] private GameObject[] Perks;
    private int currentScore = 0;
    public static int scoreMultiple = 1;

    [SerializeField] private Button buttonPause;
    [SerializeField] private int currentCoins = 0;
    private int countRoad = 0;

    private float countSpeed;
    [SerializeField] private TMP_Dropdown dropdownLanguage;
    public TMP_Dropdown dropdownClass;

    [SerializeField] private GameObject cameraRoad;
    private AudioSource cameraAudio;
    [SerializeField] private AudioClip mainTheme;
    [SerializeField] private AudioClip gameTheme;
    private string mainScene = "MainScene";
    private TopRun[] topRuns = new TopRun[3];

    void Start()
    {
        GetTopRun();
        if (cameraRoad)
            cameraAudio = cameraRoad.GetComponent<AudioSource>();
        if (PlayerPrefs.GetInt("TrigerTutorial", 0) == 1)
            Tutorial.trigerTutorial = true;
        else
            Tutorial.trigerTutorial = false;
        ResetLevel();
    }

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        countSpeed = Time.deltaTime;


        if (speed == 0) return;
        foreach (GameObject road in roads)
        {
            road.transform.position -= new Vector3(0, 0, speed * countSpeed);
        }

        if (roads[0].transform.position.z < -20)
        {
            Destroy(roads[0]);
            roads.RemoveAt(0);
            CreateNextRoad();
        }
        if (Input.GetKeyDown(KeyCode.Escape) && !pause.activeSelf)
        {
            PauseLevel();
        }
    }


    void GetTopRun()
    {
        for (int i = 0; i < 3; i++)
        {
            topRuns[i] = new TopRun();
            topRuns[i].className = PlayerPrefs.GetInt("ClassName" + i, 0);
            topRuns[i].score = PlayerPrefs.GetInt("TopRunScore" + i, 0);
            topRuns[i].date = PlayerPrefs.GetString("TopRunDate" + i, "--.--.--");
        }
    }

    //void PrintTopRun()
    //{
    //    for (int i = 0; i < 3; i++)
    //    {
    //        Debug.Log(topRuns[i].className + topRuns[i].score + topRuns[i].date);
    //    }
    //}

    void FixedUpdate()
    {
        if (speed == 0) return;
        if (Time.timeScale < maxSpeedGame)
        {
            Time.timeScale += differenceSpeedGame;
        }
        currentScore += 1 * scoreMultiple * LevelWorld.levelWorld;
        PlayerParameters.Score = currentScore + (PlayerParameters.Coins - currentCoins) * 10;
    }

    public void StartLevel()
    {
        isPlaying = true;

        if (Tutorial.trigerTutorial == true)
        {
            tutorial.SetActive(true);
            Tutorial.instance.TextLoad();
            if (Tutorial.trigerTutorial)
            {
                Tutorial.instance.OnTutorial();
            }
            else
            {
                Tutorial.instance.OffTutorial();
            }
        }

        currentCoins = PlayerParameters.Coins;
        if (buttonPause.interactable == false)
            buttonPause.interactable = true;
        menu.SetActive(false);
        perk.SetActive(true);
        if (pause.activeSelf)
            pause.SetActive(false);
        speed = maxSpeed;
        Time.timeScale = 1f;
        SwipeManager.instance.enabled = true;

        if (PlayerController.instance)
        {
            PlayerController.instance.animator.SetBool("Moving", true);
            PlayerController.instance.animator.SetFloat("Velocity", 3 / 3f);
            PlayerController.instance.AudioPlay();
        }

        cameraAudio.clip = gameTheme;
        cameraAudio.Play();
    }

    public void ResumeLevel()
    {
        isPlaying = true;
        pause.SetActive(false);
        buttonPause.interactable = true;
        Time.timeScale = currentTime;
        PlayerController.instance.AudioPlay();
    }

    public void RestartLevel()
    {
        PlayerController.instance.ClearSettings();
        ResetLevel();
        StartLevel();
    }

    public void BackMenu()
    {
        PlayerController.instance.ClearSettings();
        ResetLevel();
    }

    public void PauseLevel()
    {
        isPlaying = false;
        buttonPause.interactable = false;
        currentTime = Time.timeScale;
        Time.timeScale = 0;
        pause.SetActive(true);
        PlayerController.instance.AudioStop();
    }

    void CreateNextRoad()
    {
        Vector3 pos = new Vector3(0, 0, -15);
        if (roads.Count > 0)
        {
            pos = roads[roads.Count - 1].transform.position + new Vector3(0, 0, 15);
        }
        int number = 0;
        if (number == 0) number++; else number--;
        GameObject go = Instantiate(RoadPrefab[number], pos, Quaternion.identity);
        go.transform.SetParent(transform);
        int perkRoad = new System.Random().Next(0, 3);
        int distance;
        int numberObject = 0;
        if (roads.Count > 1)
        {
            for (int i = 0; i < 3; i++)
            {
                if (i == 1 && numberObject == 2)
                    numberObject = 5;
                else
                    numberObject = new System.Random().Next(0, Objects.Length);
                GameObject my_object = Objects[numberObject];
                distance = new System.Random().Next(-3, 3);
                my_object.transform.position = new Vector3(-2.5f + 2.5f * i, my_object.transform.position.y, distance);
                Instantiate(my_object, go.transform);
                if (i == perkRoad)
                    if (PerkGenerator.exist == true)
                    {
                        GameObject my_perk = Perks[new System.Random().Next(0, Perks.Length)];
                        if (distance > 0)
                            my_perk.transform.position = new Vector3(my_object.transform.position.x, 0, -6f);
                        else
                            my_perk.transform.position = new Vector3(my_object.transform.position.x, 0, 7f);
                        Instantiate(my_perk, go.transform);
                        PerkGenerator.exist = false;
                    }
            }
        }
        roads.Add(go);
        countRoad++;
        if (countRoad % 25 == 0)
        {
            LevelWorld.levelEnemy++;
        }
    }

    public void ResetLevel()
    {
        isPlaying = false;
        buttonPause.interactable = false;
        countRoad = 0;
        menu.SetActive(true);
        if (MenuText.instance)
            MenuText.instance.ChangeLanguageAndRefresh();

        PerkGenerator.timerPerks = 0f;
        if (perk.activeSelf == true)
        {
            PerkGenerator.instance.ClearCoinMultiple();
            PerkGenerator.instance.ClearScoreMultiple();
        }

        perk.SetActive(false);
        speed = 0;
        ClearRoads();

        if (SwipeManager.instance)
        {
            SwipeManager.instance.enabled = false;
        }

        TopRunUpdate();
        ClearPlayerParameters();

        Time.timeScale = 1f;
        if (ShopText.instance)
        {
            ShopText.instance.ChangeLanguageAndRefresh();
        }
        if (ShopBehavior.instance)
        {
            ShopBehavior.instance.ChangeStateButton();
        }
        if (PlayerController.instance)
        {
            PlayerController.instance.animator.SetBool("Moving", false);
            PlayerController.instance.AudioStop();
        }
        cameraAudio.clip = mainTheme;
        cameraAudio.Play();
    }

    void ClearRoads()
    {
        while (roads.Count > 0)
        {
            Destroy(roads[0]);
            roads.RemoveAt(0);
        }
        for (int i = 0; i < maxRoadCount; i++)
        {
            CreateNextRoad();
        }
    }

    void ClearPlayerParameters()
    {
        if (PlayerParameters.BestScore < PlayerParameters.Score)
        {
            PlayerParameters.BestScore = PlayerParameters.Score;
            PlayerPrefs.SetInt("BestScore", PlayerParameters.BestScore);
        }
        currentScore = 0;
        PlayerParameters.Score = 0;
        if (PlayerParameters.archer != null)
        {
            PlayerPrefs.SetInt(PlayerParameters.archer.ClassName + "Level", PlayerParameters.archer.Level);
            PlayerPrefs.SetInt(PlayerParameters.archer.ClassName + "CurrentExp", PlayerParameters.archer.CurrentExp);
            PlayerPrefs.SetInt(PlayerParameters.archer.ClassName + "LevelExp", PlayerParameters.archer.LevelExp);
            PlayerPrefs.SetInt(PlayerParameters.archer.ClassName + "Health", PlayerParameters.archer.Health);
            PlayerParameters.health = PlayerParameters.maxHealth * PlayerParameters.archer.Level;
        }
        if (PlayerParameters.Coins != 0)
            PlayerPrefs.SetInt("Coins", PlayerParameters.Coins);
    }

    void TopRunUpdate()
    {
        if (topRuns[2].score < PlayerParameters.Score)
        {
            topRuns[2].score = PlayerParameters.Score;
            topRuns[2].className = (int)GetNumberClass(PlayerParameters.archer.ClassName);
            topRuns[2].date = DateTime.Now.ToString("dd/MM/yy");

            Array.Sort(topRuns, (x, y) => y.CompareTo(x));

            for (int i = 0; i < 3; i++)
            {
                PlayerPrefs.SetInt("ClassName" + i, topRuns[i].className);
                PlayerPrefs.SetInt("TopRunScore" + i, topRuns[i].score);
                PlayerPrefs.SetString("TopRunDate" + i, topRuns[i].date);
            }
        }
    }

    TopRun.EClass GetNumberClass(string className)
    {
        if (className == "Warrior") return TopRun.EClass.Warrior;
        if (className == "Archer") return TopRun.EClass.Archer;
        if (className == "Mage") return TopRun.EClass.Mage;
        return 0;
    }
    public void HeroesGames()
    {
        menu.SetActive(false);
        displayGame.SetActive(false);
        heroes.SetActive(true);
        dropdownClass.value = PlayerPrefs.GetInt("NumbersHero", 0);
        SwitchClass.instance.Switch();
        PlayerParameters.instance.Stats();
        HeroesText.instance.ChangeLanguageAndRefresh();
        LevelWorld.instance.ChangeStateButton();
    }

    public void ShopGames()
    {
        menu.SetActive(false);
        displayGame.SetActive(false);
        shop.SetActive(true);
        ShopBehavior.instance.ChangeStateButton();
        ShopBehavior.instance.ClearChests();
        ShopText.instance.ChangeLanguageAndRefresh();
    }

    public void SettingsGame()
    {
        menu.SetActive(false);
        displayGame.SetActive(false);
        settingsGames.SetActive(true);
        if (PlayerPrefs.GetString("Languages", "ENG") == "ENG")
            dropdownLanguage.value = 0;
        else
            dropdownLanguage.value = 1;
        if (ToggleController.instance)
            ToggleController.instance.toggle.isOn = Tutorial.trigerTutorial;
        SetteningsText.instance.ChangeLanguageAndRefresh();
    }

    public void BackButton()
    {
        settingsGames.SetActive(false);
        heroes.SetActive(false);
        shop.SetActive(false);
        displayGame.SetActive(true);
        menu.SetActive(true);
        MenuText.instance.ChangeLanguageAndRefresh();
    }

    public void ExitGame()
    {
        SceneManager.LoadScene(mainScene);
    }
}
