using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

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
    public GameObject perk;
    public GameObject[] Objects;
    public GameObject[] Perks;
    private float currentTime = 1;
    private int currentScore = 0;
    public static int scoreMultiple = 1;

    public Button buttonPause;
    public int currentCoins = 0;


    private void Start()
    {
        ResetLevel();
        buttonPause.interactable = false;
        PlayerParameters.BestScore = PlayerPrefs.GetInt("BestScore");
        //StartLevel();
    }

    void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (speed == 0) return;
        foreach(GameObject road in roads)
        {
            road.transform.position -= new Vector3(0, 0, speed * Time.deltaTime);
        }

        if (roads[0].transform.position.z < - 20)
        {
            Destroy(roads[0]);
            roads.RemoveAt(0);
            CreateNextRoad();
        }
        if (Input.GetKeyDown(KeyCode.Escape)) {
            PauseLevel();
        }
    }

    private void FixedUpdate()
    {
        if (speed == 0) return;

        if (Time.timeScale < 4f)
        {
            Time.timeScale += 0.0001f;
            //Debug.Log(Time.timeScale);
        }
        currentScore += 1 * scoreMultiple;
        PlayerParameters.Score = currentScore + (PlayerParameters.Coins - currentCoins) * 10;
    }

    public void StartLevel()
    {
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
    }

    public void ResetLevel()
    {
        //currentCoins = PlayerParameters.Coins;
        menu.SetActive(true);
        perk.SetActive(false);
        PerkGenerator.timerPerks = 0f;
        currentScore = 0;
        speed = 0;
        while(roads.Count > 0)
        {
            Destroy(roads[0]);
            roads.RemoveAt(0);
        }
        for(int i =0; i < maxRoadCount; i++)
        {
            CreateNextRoad();
        }
        SwipeManager.instance.enabled = false;
        if (PlayerParameters.BestScore < PlayerParameters.Score)
        {
            PlayerParameters.BestScore = PlayerParameters.Score;
            PlayerPrefs.SetInt("BestScore", PlayerParameters.BestScore);
        }
        PlayerParameters.Score = 0;
        PlayerParameters.Health = PlayerParameters.maxHealth;
        Time.timeScale = 1;
    }
}
