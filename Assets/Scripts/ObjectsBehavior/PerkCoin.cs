using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkCoin : MonoBehaviour
{
    public AudioClip clipPerk;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Hero")
        {
            Destroy(gameObject);
            PerkGenerator.coinX2 = true;
            AudioSource.PlayClipAtPoint(clipPerk, transform.position);
        }
    }
}
