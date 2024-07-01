using UnityEngine;

public class FireBallBehavior : MonoBehaviour
{
    [SerializeField] private float onscreenDelay = 3f;
    [SerializeField] private GameObject RadiusAttack;

    void Update()
    {
        Destroy(gameObject, onscreenDelay);
    }

    void OnTriggerEnter(Collider other)
    {
        RadiusAttack.SetActive(true);
        Destroy(gameObject, 0.03f);
    }
}
