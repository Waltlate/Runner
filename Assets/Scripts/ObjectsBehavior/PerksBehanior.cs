using UnityEngine;

public class PerksBehanior : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Lose") || other.gameObject.CompareTag("NotLose"))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 4f);
        }
    }
}
