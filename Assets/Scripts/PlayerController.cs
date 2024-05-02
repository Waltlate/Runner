using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector3 startGamePosition;
    Quaternion startGameRotation;
    Vector3 targetPos;
    float laneOffset = 2.5f;
    float laneChangeSpeed = 15;


    // Start is called before the first frame update
    void Start()
    {
        startGamePosition = transform.position;
        startGameRotation = transform.rotation;
        targetPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && targetPos.x > -laneOffset)
        {
            targetPos = new Vector3(targetPos.x - laneOffset, transform.position.y, transform.position.z);
        }
        if (Input.GetKeyDown(KeyCode.D) && targetPos.x < laneOffset)
        {
            targetPos = new Vector3(targetPos.x + laneOffset, transform.position.y, transform.position.z);
        }
        transform.position = Vector3.MoveTowards(transform.position, targetPos, laneChangeSpeed * Time.deltaTime);
    }
}
