using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultsScreen : MonoBehaviour
{

    [SerializeField] GameObject[] cookies = new GameObject[3];
    [SerializeField] GameObject endButton;

    private void Awake()
    {
        float newScore = GameMainManager.Get().pointsTransfer;

        int cookies = (int)(newScore.Max(1.5f) * 2);

        GameMainManager.Get().saveData.cookies += cookies;
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
