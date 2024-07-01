using UnityEngine;

public class SchieldBehavior : MonoBehaviour
{
    private float euler = 0;

    void Update()
    {
        euler += 1;
        transform.rotation = Quaternion.Euler(0, euler, 0);
    }
}
