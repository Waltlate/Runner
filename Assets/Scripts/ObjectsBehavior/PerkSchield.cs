using UnityEngine;

public class PerkSchield : MonoBehaviour
{
    [SerializeField] private AudioClip clipPerk;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hero"))
        {
            Destroy(gameObject);
            PerkGenerator.schield = true;
            AudioSource.PlayClipAtPoint(clipPerk, transform.position);
        }
    }
}
