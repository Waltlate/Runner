using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkSchield : MonoBehaviour
{
    public AudioClip clipPerk;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Hero")
        {
            Destroy(gameObject);
            PerkGenerator.schield = true;
            AudioSource.PlayClipAtPoint(clipPerk, transform.position);
        }
    }
}
