using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkScore : MonoBehaviour
{
    public AudioClip clipPerk;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Hero")
        {
            Destroy(gameObject);
            PerkGenerator.scoreX2 = true;
            AudioSource.PlayClipAtPoint(clipPerk, transform.position);
        }
    }
}
