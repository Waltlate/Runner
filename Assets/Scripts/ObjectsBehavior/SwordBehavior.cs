using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBehavior : MonoBehaviour
{
    public float onscreenDelay = 3f;

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, onscreenDelay);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("destroy");
        Destroy(gameObject);
    }
}
