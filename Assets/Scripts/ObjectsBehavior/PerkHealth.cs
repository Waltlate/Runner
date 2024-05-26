using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkHealth : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Hero")
        {
            Destroy(gameObject);
            if (PlayerParameters.health < PlayerParameters.maxHealth) {
                PlayerParameters.health += LevelWorld.levelWorld;
            }
        }
    }
}
