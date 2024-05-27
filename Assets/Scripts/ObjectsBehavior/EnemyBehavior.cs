using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
//using System.random;

public class EnemyBehavior : MonoBehaviour
{
    public int level = 1;
    public int health = 1;
    public int damage = 1;

    public TextMeshProUGUI levelLabel;

    public GameObject[] enemies;

    public void Start()
    {
        level = LevelWorld.levelWorld + LevelWorld.levelEnemy - 1;
        health = level;
        damage = level;
        if(PlayerParameters.instance)
        if(level > PlayerParameters.archer.Level)
            if(level > PlayerParameters.archer.Level + PlayerParameters.archer.LevelWeapon - 1)
                levelLabel.color = Color.red;
            else
                levelLabel.color = Color.yellow;
        levelLabel.text = $"Lvl. {level}";

    }

    public void Awake()
    {
        if (enemies.Length != 0)
        {
            int num = new System.Random().Next(0, enemies.Length);
            enemies[num].SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Attack")
        {
            health -= PlayerParameters.archer.Damage;

           
            if (health <= 0)
            {
                //if (level > 1)
                //    Debug.Log("level" + level);
                Destroy(this.gameObject);
                PlayerParameters.Coins += level;
                PlayerParameters.archer.CurrentExp += level;
            }
        }

        if (other.gameObject.tag == "Hero")
        {
            PlayerParameters.health -= damage;
            Destroy(this.gameObject);
        }
        if (PlayerParameters.health <= 0)
        {
            if(PlayerController.instance)
            PlayerController.instance.ResetGame();
        }
    }
}
