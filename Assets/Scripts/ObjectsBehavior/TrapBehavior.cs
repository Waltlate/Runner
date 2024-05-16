using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBehavior : MonoBehaviour
{
    public int Level = 1;
    public int Health = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Hero")
        {
            PlayerParameters.Health -= 1;
        }
        if (PlayerParameters.Health == 0)
        {
            Debug.Log(0);
            PlayerController.instance.ResetGame();
        }
    }
}
