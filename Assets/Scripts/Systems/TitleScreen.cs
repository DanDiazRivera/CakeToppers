using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    GameMainManager mainManager;

    [SerializeField] GameObject[] cookies;
    [SerializeField] float cookieOffset;
    [SerializeField] LevelData[] levels;

    private void Awake()
    {
        mainManager = GameMainManager.Get();
        mainManager.AllLevels = levels;
        CakeToppersSaveData.Get(ref mainManager.saveData);

        int trueCookies = levels.Cookies();
        for (int i = 0; i < cookies.Length; i++) if (trueCookies > i) cookies[i].SetActive(true);
    }

    //public void BeginGame() => GameMainManager.BeginGame();

}
