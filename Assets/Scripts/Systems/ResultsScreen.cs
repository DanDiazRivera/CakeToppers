using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultsScreen : MonoBehaviour
{

    [SerializeField] GameObject[] cookies = new GameObject[3];
    [SerializeField] GameObject endButton;

    private void Awake()
    {
        int newScore = GameMainManager.Get().pointsTransfer;
        LevelData level = GameMainManager.Get().levelData;

        int cookies = 0;

        if (newScore >= level.minScore) cookies++;
        if (newScore >= level.cookie2Score) cookies++;
        if (newScore >= level.cookie3Score) cookies++;

        if (level.highScore < newScore) level.highScore = newScore;
        GameMainManager.Get().saveData.Save();

        StartCoroutine(CookieShowEnum(cookies));
    }

    public void ReturnToTitle() => GameMainManager.ReturnToTitle();

    IEnumerator CookieShowEnum(int count)
    {
        yield return new WaitForSeconds(1f);
        int currentCookie = 0;
        while(currentCookie < count)
        {
            cookies[currentCookie].SetActive(true);
            currentCookie++;
            yield return new WaitForSeconds(0.3f);
        }

        endButton.SetActive(true);
    }
}
