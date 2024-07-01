using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [SerializeField] private float onscreenDelay = 3f;

    void Update()
    {
        Destroy(gameObject, onscreenDelay);
    }

    void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
