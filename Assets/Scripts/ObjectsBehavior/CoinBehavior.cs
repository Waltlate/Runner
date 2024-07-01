using System.Collections;
using TMPro;
using UnityEngine;

public class CoinBehavior : MonoBehaviour
{
    private float euler = 0f;
    [SerializeField] private float offset = 0.5f;
    public static int coinMultiple = 1;
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private AudioClip clipCoin;


    void Start()
    {
        StartCoroutine(Movement());
        coinText.text = $"{LevelWorld.levelWorld}";
    }

    IEnumerator Movement()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.005f);
            if (euler > 30f || euler < -30f)
            {
                offset *= -1f;
            }
            euler += offset;
            transform.rotation = Quaternion.Euler(90f, euler, 0);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hero"))
        {
            Destroy(gameObject);
            PlayerParameters.Coins += LevelWorld.levelWorld * coinMultiple;
            AudioSource.PlayClipAtPoint(clipCoin, transform.position);
        }
    }
}
