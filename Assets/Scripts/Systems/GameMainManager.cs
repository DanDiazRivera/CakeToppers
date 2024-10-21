using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMainManager : Singleton<GameMainManager>
{
    public new static GameMainManager Get() => InitCreate(true, "Game Manager");

    public enum GameState { Title, Playing, Paused, Results }
    public GameState state;
    public CakeToppersSaveData saveData;
    public float pointsTransfer;
    public LevelData levelData;

    protected override void OnAwake()
    {
        DontDestroyOnLoad(this);
        CakeToppersSaveData.Get(ref saveData);
    }

    public static void BeginGame()
    {
        //if (Get().state != GameState.Title) return;
        SceneManager.LoadScene(1);
    }
    public static void ReturnToTitle()
    {
        //if (Get().state == GameState.Title) return;
        SceneManager.LoadScene(0);
    }
    public static void GoToResults(int currentPoints, int requiredPoints)
    {
        Get().pointsTransfer = currentPoints / requiredPoints;
        SceneManager.LoadScene(2);
    }

}

public class CakeToppersSaveData : SaveData<CakeToppersSaveData>
{
    protected override string FileName() => "CakeToppersData";

    public int cookies;


}