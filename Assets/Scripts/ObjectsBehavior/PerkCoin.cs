using UnityEngine;

public class PerkCoin : MonoBehaviour
{
    [SerializeField] private AudioClip clipPerk;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hero"))
        {
            Destroy(gameObject);
            PerkGenerator.coinX2 = true;
            AudioSource.PlayClipAtPoint(clipPerk, transform.position);
        }
    }
}
