using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehavior : MonoBehaviour
{
    private float euler = 0f;
    public float offset = 0.5f;
    public static int coinMultiple = 1;


    public void Start()
    {
        StartCoroutine(Movement());
    }

    IEnumerator Movement()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.005f);
            if (euler > 30f || euler < -30f)
            {
                offset *= -1;
            }
            euler += offset;
            transform.rotation = Quaternion.Euler(90f, euler, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Hero")
        {
                Destroy(gameObject);
                PlayerParameters.Coins += 1 * coinMultiple;
        }
    }
}
