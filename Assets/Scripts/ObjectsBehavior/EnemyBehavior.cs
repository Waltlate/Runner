using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    private int level = 1;
    private int health = 1;
    private int damage = 1;

    public TextMeshProUGUI levelLabel;

    public int Level
    {
        get { return level; }
        //set { className = value; }
    }

    public void Start()
    {
        level = LevelWorld.levelWorld + LevelWorld.levelEnemy - 1;
        health = level;
        damage = level;
        if(level > PlayerParameters.archer.Level)
            if(level > PlayerParameters.archer.Level + PlayerParameters.archer.LevelWeapon - 1)
                levelLabel.color = Color.red;
            else
                levelLabel.color = Color.yellow;
        levelLabel.text = $"Lvl. {level}";
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
            PlayerController.instance.ResetGame();
        }
    }
}
