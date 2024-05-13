using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkScore : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Hero")
        {
            Destroy(gameObject);
            PerkGenerator.scoreX2 = true;
        }
    }
}
