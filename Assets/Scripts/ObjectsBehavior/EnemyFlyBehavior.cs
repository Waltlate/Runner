using TMPro;
using UnityEngine;

public class EnemyFlyBehavior : MonoBehaviour
{
    [HideInInspector] public int level;
    private int health;

    [SerializeField] private TextMeshProUGUI levelLabel;
    [SerializeField] private AudioClip clipDeath;

    void Start()
    {
        level = LevelWorld.levelWorld;
        health = level;
        levelLabel.text = $"Lvl. {level}";
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
                PlayerParameters.health -= level;
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
