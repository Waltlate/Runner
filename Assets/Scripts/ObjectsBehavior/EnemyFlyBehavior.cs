using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyFlyBehavior : MonoBehaviour
{
    public int level;
    public int health;

    public TextMeshProUGUI levelLabel;
    public AudioClip clipDeath;

    public void Start()
    {
        level = LevelWorld.levelWorld;
        health = level;
        levelLabel.text = $"Lvl. {level}";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Attack")
        {
            health -= PlayerParameters.archer.Damage;
            //Debug.Log(Health);
            if (health <= 0)
            {
                Destroy(this.gameObject);
                PlayerParameters.Coins += level;
                PlayerParameters.archer.CurrentExp += level;
                AudioSource.PlayClipAtPoint(clipDeath, transform.position);
            }
        }
        if (other.gameObject.tag == "Hero")
        {
            PlayerParameters.health -= level;
        }
        if (PlayerParameters.health <= 0)
        {
            PlayerController.instance.ResetGame();
        }
    }
}
