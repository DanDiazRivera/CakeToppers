using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    GameMainManager mainManager;

    [SerializeField] GameObject cookiePrefab;
    [SerializeField] Transform cookiePlacement;
    [SerializeField] float cookieOffset;


    private void Awake()
    {
        mainManager = GameMainManager.Get();

        int cookieCount = mainManager.saveData.cookies;

        for (int i = 0; i < cookieCount; i++)
        {
            GameObject thisCookie = Instantiate(cookiePrefab, cookiePlacement);
            thisCookie.transform.localPosition = Vector3.up * cookieOffset * i;
        }

    }

    public void BeginGame() => GameMainManager.BeginGame();

}
