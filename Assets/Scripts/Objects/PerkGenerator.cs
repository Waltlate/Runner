using System.Collections;
using UnityEngine;

public class PerkGenerator : MonoBehaviour
{
    public static PerkGenerator instance;
    public static bool exist = false;
    public static float timerPerks = 0f;
    public static bool scoreX2 = false;
    [SerializeField] private float timerScoreX2 = 10f;
    [SerializeField] private GameObject scoreUI;
    public static bool coinX2 = false;
    [SerializeField] private float timerCoinX2 = 10f;
    [SerializeField] private GameObject coinUI;
    public static bool schield = false;
    [SerializeField] private float timerSchield = 10f;
    [SerializeField] private GameObject schieldsHero;
    [SerializeField] private float maxTimePerks = 30f;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (scoreX2 == true)
        {
            StartCoroutine(CoroutineScoreX2());
            scoreX2 = false;
        }

        if (coinX2 == true)
        {
            StartCoroutine(CoroutineCoinX2());
            coinX2 = false;
        }

        if (schield == true)
        {
            StartCoroutine(CoroutineSchield());
        }

        timerPerks += Time.deltaTime;
        if (timerPerks >= maxTimePerks)
        {
            timerPerks = 0f;
            exist = true;
        }
    }

    public void ClearScoreMultiple()
    {
        RoadGenerator.scoreMultiple = 1;
        scoreUI.SetActive(false);
    }

    public void ClearCoinMultiple()
    {
        CoinBehavior.coinMultiple = 1;
        coinUI.SetActive(false);
    }

    IEnumerator CoroutineScoreX2()
    {
        RoadGenerator.scoreMultiple = 2;
        scoreUI.SetActive(true);
        yield return new WaitForSeconds(timerScoreX2);
        ClearScoreMultiple();
    }

    IEnumerator CoroutineCoinX2()
    {
        CoinBehavior.coinMultiple = 2;
        coinUI.SetActive(true);
        yield return new WaitForSeconds(timerCoinX2);
        ClearCoinMultiple();
    }

    IEnumerator CoroutineSchield()
    {
        schieldsHero.SetActive(true);
        yield return new WaitForSeconds(timerSchield);
        schieldsHero.SetActive(false);
        schield = false;
    }
}
