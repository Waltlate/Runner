using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyParameters : MonoBehaviour
{

    public int Level = 1;
    public int Health = 1;
    //public static int maxHealth = 5;
    //public static int Coins = 0;
    //public static int Score = 0;
    //public static int BestScore = 0;
    //public TextMeshProUGUI hpLabel;
    //public TextMeshProUGUI scoreLabel;
    //public TextMeshProUGUI bestScoreLabel;
    //public TextMeshProUGUI coinsLabel;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Attack")
        {
            Health -= 1;
            Debug.Log(Health);
            if (Health == 0)
            {
                Destroy(this.gameObject);
                PlayerParameters.Coins += 1;
            }
        }

    }
}
