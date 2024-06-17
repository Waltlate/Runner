using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBehavior : MonoBehaviour
{
    public float onscreenDelay = 3f;

    void Update()
    {
        Destroy(gameObject, onscreenDelay);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
