using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Data;
using TMPro;

public class RoadGenerator : MonoBehaviour
{
    public static RoadGenerator instance;
    public GameObject RoadPrefab;
    private List<GameObject> roads = new List<GameObject>();
    public float maxSpeed = 10;
    private float speed = 0;
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
    public TMP_Dropdown dropdown;

    private void Start()
    {

        ResetLevel();
        
        PlayerParameters.BestScore = PlayerPrefs.GetInt("BestScore");
        //StartLevel();

        //StartCoroutine(Movement());
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
        if (Input.GetKeyDown(KeyCode.Escape))
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

        //Time.timeScale = 3f;

        if (Time.timeScale < 3f)
        {
            Time.timeScale += 0.0001f;
            //Time.timeScale += Time.deltaTime;
            //Debug.Log(Time.timeScale);
        }
        currentScore += 1 * scoreMultiple;
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
        if(pause.activeSelf) pause.SetActive(false);
        speed = maxSpeed;
        Time.timeScale = currentTime;
        //Time.timeScale = 1;

        SwipeManager.instance.enabled = true;
    }

    public void ResumeLevel()
    {
        pause.SetActive(false);
        Time.timeScale = currentTime;
       
    }

    public void RestartLevel()
    {
        ResetLevel();
        StartLevel();
    }

    public void PauseLevel()
    {
        currentTime = Time.timeScale;
        Time.timeScale = 0;
        pause.SetActive(true);
    }

    private void CreateNextRoad()
    {
        Vector3 pos = new Vector3(0,0,-15);
        if(roads.Count > 0)
        {
            pos = roads[roads.Count - 1].transform.position + new Vector3(0,0,15);
        }
        GameObject go = Instantiate(RoadPrefab, pos, Quaternion.identity);
        go.transform.SetParent(transform);
        int perkRoad = new System.Random().Next(0, 3);
        if (roads.Count > 1)
        for (int i = 0; i < 3; i++)
        {
            GameObject my_object = Objects[new System.Random().Next(0, Objects.Length)];
            my_object.transform.position = new Vector3(-2.5f + 2.5f * i, my_object.transform.position.y, -3f + new System.Random().Next(0, 10));
            Instantiate(my_object, go.transform);
            if(i == perkRoad)
                if (PerkGenerator.exist == true)
                {
                    GameObject my_perk = Perks[new System.Random().Next(0, Perks.Length)];
                    my_perk.transform.position = new Vector3(my_object.transform.position.x, 0, my_object.transform.position.z - 5f);
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
        //MenuText.instance.ChangeLanguageAndRefresh();

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

        //Debug.Log($"{PlayerParameters.BestScore} - {PlayerParameters.Score} - {currentScore}");
        if (PlayerParameters.BestScore < currentScore)
        {
            PlayerParameters.BestScore = currentScore;
            PlayerPrefs.SetInt("BestScore", PlayerParameters.BestScore);
        }
        currentScore = 0;
        if (PlayerParameters.archer != null) {
        PlayerPrefs.SetInt(PlayerParameters.archer.ClassName + "Level", PlayerParameters.archer.Level);
        PlayerPrefs.SetInt(PlayerParameters.archer.ClassName + "CurrentExp", PlayerParameters.archer.CurrentExp);
        PlayerPrefs.SetInt(PlayerParameters.archer.ClassName + "LevelExp", PlayerParameters.archer.LevelExp);
        PlayerPrefs.SetInt(PlayerParameters.archer.ClassName + "Health", PlayerParameters.archer.Health);
        PlayerParameters.Score = 0;
        PlayerParameters.health = PlayerParameters.maxHealth * PlayerParameters.archer.Level;
        }
        Time.timeScale = 1;
    }

    public void HeroesGames()
    {
        menu.SetActive(false);
        displayGame.SetActive(false);
        heroes.SetActive(true);
    }

    public void ShopGames()
    {
        menu.SetActive(false);
        displayGame.SetActive(false);
        shop.SetActive(true);
    }

    public void SettingsGame()
    {
        menu.SetActive(false);
        displayGame.SetActive(false);
        settingsGames.SetActive(true);
        if (PlayerPrefs.GetString("Languages", "ENG") == "ENG")
            dropdown.value = 0;
        else
            dropdown.value = 1;
        if(ToggleController.instance)
        ToggleController.instance.toggle.isOn = Tutorial.trigerTutorial;
    }

    public void BackButton()
    {
        settingsGames.SetActive(false);
        heroes.SetActive(false);
        shop.SetActive(false);
        displayGame.SetActive(true);
        menu.SetActive(true);
        
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
