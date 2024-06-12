using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBehavior : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Hero")
        {
            if (!PerkGenerator.schield)
            {
                PlayerParameters.health -= LevelWorld.levelWorld;
                PlayerController.instance.SoundDamage();
            }
            else
            {
                PlayerController.instance.SoundBlock();
            }

            //PlayerParameters.health -= LevelWorld.levelWorld;
            //PlayerController.instance.SoundDamage();
        }
        if (PlayerParameters.health <= 0)
        {
            if (PlayerController.instance)
            {
                //PlayerController.instance.SoundDamageStop();
                PlayerController.instance.ResetGame();
            }
        }
    }
}
