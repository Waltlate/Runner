using UnityEngine;

public class PerkHealth : MonoBehaviour
{
    [SerializeField] private AudioClip clipHealth;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hero"))
        {
            if (PlayerParameters.health < PlayerParameters.maxHealth * PlayerParameters.archer.Level)
            {
                PlayerParameters.health += LevelWorld.levelWorld;
                if (PlayerParameters.health > PlayerParameters.maxHealth * PlayerParameters.archer.Level)
                    PlayerParameters.health = PlayerParameters.maxHealth * PlayerParameters.archer.Level;
            }
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(clipHealth, transform.position);
        }
    }
}
