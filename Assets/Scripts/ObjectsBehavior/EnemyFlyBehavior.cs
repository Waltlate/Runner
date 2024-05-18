using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyBehavior : MonoBehaviour
{
    public int level = 1;
    public int health = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Attack")
        {
            health -= 1;
            //Debug.Log(Health);
            if (health == 0)
            {
                Destroy(this.gameObject);
                PlayerParameters.Coins += 1;
                PlayerParameters.archer.CurrentExp += level;
            }
        }
        if (other.gameObject.tag == "Hero")
        {
            PlayerParameters.health -= 1;
        }
        if (PlayerParameters.health == 0)
        {
            Debug.Log(0);
            PlayerController.instance.ResetGame();
        }
    }
}
