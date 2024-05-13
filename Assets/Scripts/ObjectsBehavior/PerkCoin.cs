using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkCoin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Hero")
        {
            Destroy(gameObject);
            PerkGenerator.coinX2 = true;
        }
    }
}
