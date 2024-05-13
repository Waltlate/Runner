using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerksBehanior : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Lose" || other.gameObject.tag == "NotLose")
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 4f);
        }
    }
}
