using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkHealth : MonoBehaviour
{
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
        }
    }
}
