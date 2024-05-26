using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBehavior : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Hero")
        {
            PlayerParameters.health -= LevelWorld.levelWorld;
        }
        if (PlayerParameters.health <= 0)
        {
            PlayerController.instance.ResetGame();
        }
    }
}
