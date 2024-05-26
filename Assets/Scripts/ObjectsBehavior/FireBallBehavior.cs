using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallBehavior : MonoBehaviour
{
    public float onscreenDelay = 3f;
    public GameObject RadiusAttack;

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, onscreenDelay);
    }

    private void OnTriggerEnter(Collider other)
    {
        RadiusAttack.SetActive(true);
        Destroy(gameObject, 0.03f);
    }
}
