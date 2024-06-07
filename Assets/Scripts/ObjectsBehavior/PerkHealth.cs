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
            //Debug.Log($"heal {PlayerParameters.health} {PlayerParameters.maxHealth}");
            if (PlayerParameters.health < PlayerParameters.maxHealth * PlayerParameters.archer.Level) {
                PlayerParameters.health += LevelWorld.levelWorld;
                if (PlayerParameters.health > PlayerParameters.maxHealth * PlayerParameters.archer.Level)
                    PlayerParameters.health = PlayerParameters.maxHealth * PlayerParameters.archer.Level;
                //Debug.Log($"heal++ {PlayerParameters.health} {PlayerParameters.maxHealth}");
            }
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(clipHealth, transform.position);
        }
    }
}
