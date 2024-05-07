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
    //public float startSpeed = 5;
    private float speed = 0;
    public int maxRoadCount = 6;
    //internal static object instance;
    public GameObject menu;
    public GameObject[] Objects;
    private float currentTime = 1;
    public GameObject pause;
    public Button buttonPause;

    private void Start()
    {
        ResetLevel();
        buttonPause.interactable = false;
        PlayerParameters.BestScore = PlayerPrefs.GetInt("BestScore");
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

            //GameObject first = roads[0];
            //for (int i = 1; i < roads.Count; i++) {
            //    roads[i - 1] = roads[i];
            //}

            //roads[roads.Count - 1] = first;
            //roads[roads.Count - 1].transform.position += new Vector3(0, 0, maxRoadCount * 15);
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
        PlayerParameters.Score += 1;
    }

    public void StartLevel()
    {
        if(buttonPause.interactable == false)
            buttonPause.interactable = true;
        menu.SetActive(false);
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
        if(roads.Count > 1)
        for (int i = 0; i < 3; i++)
        {
            GameObject my_object = Objects[new System.Random().Next(0, Objects.Length)];
            my_object.transform.position = new Vector3(-2.5f + 2.5f * i, my_object.transform.position.y, -3f + new System.Random().Next(0, 10));
            Instantiate(my_object, go.transform);
        }
        roads.Add(go);
    }

    public void ResetLevel()
    {
        menu.SetActive(true);
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
