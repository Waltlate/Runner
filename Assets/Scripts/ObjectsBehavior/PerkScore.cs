using UnityEngine;

public class PerkScore : MonoBehaviour
{
    [SerializeField] private AudioClip clipPerk;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hero"))
        {
            Destroy(gameObject);
            PerkGenerator.scoreX2 = true;
            AudioSource.PlayClipAtPoint(clipPerk, transform.position);
        }
    }
}
