using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkGenerator : MonoBehaviour
{
    public static bool exist = false;
    public static float timerPerks = 0f;
    public static bool scoreX2 = false;
    public static float timerScoreX2 = 5f;
    public GameObject scoreUI;
    public static bool coinX2 = false;
    public static float timerCoinX2 = 5f;
    public GameObject coinUI;
    private float maxTimePerks = 30f;

    // Update is called once per frame
    void Update()
    {
        timerPerks += Time.deltaTime; // Увеличиваем время каждый кадр
        //Debug.Log(timer);
        if (timerPerks >= maxTimePerks) // Проверяем, прошло ли уже 30 секунд
        {
            timerPerks = 0f; // Сбрасываем таймер
            exist = true;
        }

        if(scoreX2 == true) {
            StartCoroutine(CoroutineScoreX2());
            scoreX2 = false;
        }

        if (coinX2 == true)
        {
            StartCoroutine(CoroutineCoinX2());
            coinX2 = false;
        }
    }

    IEnumerator CoroutineScoreX2()
    {
        RoadGenerator.scoreMultiple = 2;
        scoreUI.SetActive(true);
        yield return new WaitForSeconds(5);
        RoadGenerator.scoreMultiple = 1;
        scoreUI.SetActive(false);
    }

    IEnumerator CoroutineCoinX2()
    {
        CoinBehavior.coinMultiple = 2;
        coinUI.SetActive(true);
        yield return new WaitForSeconds(5);
        CoinBehavior.coinMultiple = 1;
        coinUI.SetActive(false);
    }
}
