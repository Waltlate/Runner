using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchieldBehavior : MonoBehaviour
{
    private float euler = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        euler += 1;
        transform.rotation = Quaternion.Euler(0, euler, 0);
    }
}
