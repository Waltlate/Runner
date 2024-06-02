using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSize : MonoBehaviour
{
    private float attitude;
    // Start is called before the first frame update
    void Start()
    {
        Camera camera = GetComponent<Camera>();
        if (((float)Screen.height / 1280f) * 720f < (float)Screen.width)
        {
            attitude = ((float)Screen.height / 1280f) * 720f / (float)Screen.width;
            Debug.Log(attitude);
            camera.rect = new Rect((1 - attitude) / 2f, 0, attitude, 1);
            //camera.rect = new Rect(0, 0, 9f/16f, 1);
        }
        else
        {
            attitude = (float)Screen.width / 720f * 1280f / (float)Screen.height;
            Debug.Log(attitude);
            camera.rect = new Rect(0, (1 - attitude) / 2f, 1, attitude);
            //camera.rect = new Rect(0, (1- attitude) / 2, 1, attitude);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
