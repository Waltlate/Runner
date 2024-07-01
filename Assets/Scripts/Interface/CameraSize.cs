using UnityEngine;

public class CameraSize : MonoBehaviour
{
    private float attitude;

    void Start()
    {
        Camera camera = GetComponent<Camera>();
        if (((float)Screen.height / 1280f) * 720f < (float)Screen.width)
        {
            attitude = ((float)Screen.height / 1280f) * 720f / (float)Screen.width;
            camera.rect = new Rect((1 - attitude) / 2f, 0, attitude, 1);
        }
        else
        {
            attitude = (float)Screen.width / 720f * 1280f / (float)Screen.height;
            camera.rect = new Rect(0, (1 - attitude) / 2f, 1, attitude);
        }
    }
}
