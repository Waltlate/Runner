using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class PlayerParameters : MonoBehaviour
{
    //public static PlayerParameters instance;
    public static BaseClass archer = new WarriorClass();
    public int Level = 1;
    public static int Health;
    public static int maxHealth = 5;
    public static float distance;
    public static float speedAttack;
    public static int Coins = 0;
    public static int Score = 0;
    public static int BestScore = 0;

    public TextMeshProUGUI hpLabel;
    public TextMeshProUGUI scoreLabel;
    public TextMeshProUGUI bestScoreLabel;
    public TextMeshProUGUI coinsLabel;
    

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(archer.ClassName);
        maxHealth = archer.Health;
        distance = archer.Distance;
        speedAttack = archer.Speed;
        Health = maxHealth;
        Score = 0;
        hpLabel.text = $"HP: {Health}";
        scoreLabel.text = $"Score: {Score}";
        bestScoreLabel.text = $"Best Score: {Score}";
        coinsLabel.text = $"Coins: {Coins}";
    }

    // Update is called once per frame
    void Update()
    {
        hpLabel.text = $"HP: {Health}";
        scoreLabel.text = $"Score: {Score}";
        bestScoreLabel.text = $"Best Score: {BestScore}";
        coinsLabel.text = $"Coins: {Coins}";
    }
}
