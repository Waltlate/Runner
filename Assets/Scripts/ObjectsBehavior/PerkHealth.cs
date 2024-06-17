using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerkHealth : MonoBehaviour
{
    public AudioClip clipHealth;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Hero")
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
