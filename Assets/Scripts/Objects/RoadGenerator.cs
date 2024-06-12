using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Data;
using TMPro;

/*
Add:
--- Music

- add second scene
- perk shield
- speed game and speed difference game - SerializedField
--- time perk - SerializedField
- down key + trap in fly
- add top-3 with score and date record
- style Microsoft


Bugs:
--- lvl enemy update
--- perks in objects
--- exist obects ???
--- in tutorial speed = 0 and size
- tutorisl coroutine attack stoped
--- for read reverse language data
*/

public class RoadGenerator : MonoBehaviour
{
    public static RoadGenerator instance;
    public GameObject[] RoadPrefab;
    private List<GameObject> roads = new List<GameObject>();
    public float maxSpeed = 10;
    [HideInInspector]
    public float speed = 0;
    public int maxRoadCount = 6;
    public GameObject menu;
    public GameObject pause;
    public GameObject displayGame;
    public GameObject heroes;
    public GameObject shop;
    public GameObject settingsGames;
    public GameObject perk;
    public GameObject tutorial;
    public GameObject[] Objects;
    public GameObject[] Perks;
    private float currentTime = 1;
    private int currentScore = 0;
    public static int scoreMultiple = 1;

    public Button buttonPause;
    public int currentCoins = 0;
    private int countRoad = 0;

    private float countSpeed;
    public TMP_Dropdown dropdownLanguage;
    public TMP_Dropdown dropdownClass;

    public GameObject camera;
    private AudioSource cameraAudio;
    public AudioClip mainTheme;
    public AudioClip gameTheme;

    private void Start()
    {
        if(camera)
            cameraAudio = camera.GetComponent<AudioSource>();
        ResetLevel();
    }

    void Awake()
    {
        instance = this;
    }

    private void Update()
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


    //IEnumerator Movement()
    //{
    //    while (true)
    //    {
    //        yield return new WaitForSeconds(0.01f);
    //        //countSpeed = Time.deltaTime;
    //        countSpeed = 0.01f;
    //        //Debug.Log(countSpeed);
    //        if (speed != 0)
    //        {
    //            foreach (GameObject road in roads)
    //            {
    //                road.transform.position -= new Vector3(0, 0, speed * countSpeed);
    //            }

    //            if (roads[0].transform.position.z < -20)
    //            {
    //                Destroy(roads[0]);
    //                roads.RemoveAt(0);
    //                CreateNextRoad();

    //                //Debug.Log("road");//--------

    //            }
    //            if (Input.GetKeyDown(KeyCode.Escape))
    //            {
    //                PauseLevel();
    //            }

    //            Time.timeScale = 3f;

    //            if (Time.timeScale < 3f)
    //            {
    //                Time.timeScale += 0.0001f;
    //                //Debug.Log(Time.timeScale);
    //            }
    //            currentScore += 1 * scoreMultiple;
    //            PlayerParameters.Score = currentScore + (PlayerParameters.Coins - currentCoins) * 10;
    //        }
    //    }
    //}


    private void FixedUpdate()
    {
        if (speed == 0) return;
        if (Time.timeScale < 3f)
        {
            Time.timeScale += 0.0001f;
        }
        currentScore += 1 * scoreMultiple * LevelWorld.levelWorld;
        PlayerParameters.Score = currentScore + (PlayerParameters.Coins - currentCoins) * 10;
    }

    public void StartLevel()
    {
        if(Tutorial.trigerTutorial == true) {
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
        if(buttonPause.interactable == false)
            buttonPause.interactable = true;
        menu.SetActive(false);
        perk.SetActive(true);
        if(pause.activeSelf)
            pause.SetActive(false);
        speed = maxSpeed;
        Time.timeScale = currentTime;
        SwipeManager.instance.enabled = true; //???

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

    public void PauseLevel()
    {
        buttonPause.interactable = false;
        currentTime = Time.timeScale;
        Time.timeScale = 0;
        pause.SetActive(true);
        PlayerController.instance.AudioStop();
    }

    private void CreateNextRoad()
    {
        Vector3 pos = new Vector3(0, 0, -15);
        if(roads.Count > 0)
        {
            pos = roads[roads.Count - 1].transform.position + new Vector3(0, 0, 15);
        }
        int number = 0;
        if (number == 0) number++; else number--;
        GameObject go = Instantiate(RoadPrefab[number], pos, Quaternion.identity);
        go.transform.SetParent(transform);
        int perkRoad = new System.Random().Next(0, 3);
        int distance;
        if (roads.Count > 1)
        for (int i = 0; i < 3; i++)
        {
            GameObject my_object = Objects[new System.Random().Next(0, Objects.Length)];
                distance = new System.Random().Next(-3, 3);
                my_object.transform.position = new Vector3(-2.5f + 2.5f * i, my_object.transform.position.y, distance);
            Instantiate(my_object, go.transform);
            if(i == perkRoad)
                if (PerkGenerator.exist == true)
                {
                    GameObject my_perk = Perks[new System.Random().Next(0, Perks.Length)];
                        if(distance > 0)
                    my_perk.transform.position = new Vector3(my_object.transform.position.x, 0, -6f);
                        else
                    my_perk.transform.position = new Vector3(my_object.transform.position.x, 0, 7f);
                        Instantiate(my_perk, go.transform);
                    PerkGenerator.exist = false;
                }
            }

        roads.Add(go);
        countRoad++;
        if(countRoad % 25 == 0)
        {
            LevelWorld.levelEnemy++;
        }
    }

    public void ResetLevel()
    {
        buttonPause.interactable = false;
        countRoad = 0;
        menu.SetActive(true);
        if(MenuText.instance)
            MenuText.instance.ChangeLanguageAndRefresh();

        PerkGenerator.timerPerks = 0f;
        if (perk.activeSelf == true)
        {
            PerkGenerator.instance.ClearCoinMultiple();
            PerkGenerator.instance.ClearScoreMultiple();
        }
        perk.SetActive(false);

        speed = 0;
        while (roads.Count > 0)
        {
            Destroy(roads[0]);
            roads.RemoveAt(0);
        }
        for (int i = 0; i < maxRoadCount; i++)
        {
            CreateNextRoad();
        }
        if (SwipeManager.instance)
        {
            SwipeManager.instance.enabled = false;
        }

        Debug.Log($"{PlayerParameters.BestScore} - {PlayerParameters.Score} - {currentScore}");
        if (PlayerParameters.BestScore < PlayerParameters.Score)
        {
            PlayerParameters.BestScore = PlayerParameters.Score;
            PlayerPrefs.SetInt("BestScore", PlayerParameters.BestScore);
        }
        currentScore = 0;
        PlayerParameters.Score = 0;
        if (PlayerParameters.archer != null) {
        PlayerPrefs.SetInt(PlayerParameters.archer.ClassName + "Level", PlayerParameters.archer.Level);
        PlayerPrefs.SetInt(PlayerParameters.archer.ClassName + "CurrentExp", PlayerParameters.archer.CurrentExp);
        PlayerPrefs.SetInt(PlayerParameters.archer.ClassName + "LevelExp", PlayerParameters.archer.LevelExp);
        PlayerPrefs.SetInt(PlayerParameters.archer.ClassName + "Health", PlayerParameters.archer.Health);
        PlayerParameters.health = PlayerParameters.maxHealth * PlayerParameters.archer.Level;
        }
        if (PlayerParameters.Coins != 0)
            PlayerPrefs.SetInt("Coins", PlayerParameters.Coins);
        Time.timeScale = 1;
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
        if(ToggleController.instance)
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
        Application.Quit();
    }
}
