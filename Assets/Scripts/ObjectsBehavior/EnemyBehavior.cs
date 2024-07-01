using TMPro;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [HideInInspector] public int level = 1;
    private int health = 1;
    private int damage = 1;

    [SerializeField] private TextMeshProUGUI levelLabel;

    [SerializeField] private GameObject[] enemies;
    [SerializeField] private AudioClip clipDeath;


    void Start()
    {
        level = LevelWorld.levelWorld + LevelWorld.levelEnemy - 1;
        health = level;
        damage = level;
        ColorLabelUpdate();
        levelLabel.text = $"Lvl. {level}";
    }

    void Awake()
    {
        if (enemies.Length != 0)
        {
            int num = new System.Random().Next(0, enemies.Length);
            enemies[num].SetActive(true);
        }
    }


    public void ColorLabelUpdate()
    {
        if (PlayerParameters.instance)
            if (level > PlayerParameters.archer.Level)
            {
                if (level > PlayerParameters.archer.Level + PlayerParameters.archer.LevelWeapon - 1)
                    levelLabel.color = Color.red;
                else
                    levelLabel.color = Color.yellow;
            }
            else
            {
                levelLabel.color = Color.green;
            }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Attack"))
        {
            health -= PlayerParameters.archer.Damage;


            if (health <= 0)
            {
                Destroy(this.gameObject);
                PlayerParameters.Coins += level;
                PlayerParameters.archer.CurrentExp += level;
                AudioSource.PlayClipAtPoint(clipDeath, transform.position);
            }
        }

        if (other.gameObject.CompareTag("Hero"))
        {
            if (!PerkGenerator.schield)
            {
                PlayerParameters.health -= damage;
                PlayerController.instance.SoundDamage();
            }
            else
            {
                PlayerController.instance.SoundBlock();
            }
            Destroy(this.gameObject);
        }
        if (PlayerParameters.health <= 0)
        {
            if (PlayerController.instance)
            {
                PlayerController.instance.ResetGame();
            }
        }
    }
}
