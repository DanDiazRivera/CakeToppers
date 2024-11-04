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
    public int pointsTransfer;
    public LevelData levelData;
    [HideInInspector] public LevelData[] AllLevels;

    protected override void OnAwake()
    {
        DontDestroyOnLoad(this);
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 1;
        //CakeToppersSaveData.Get(ref saveData);
    }

    public static void BeginGame(LevelData levelData)
    {
        Get().levelData = levelData;
        SceneManager.LoadScene(1);
    }
    public static void ReturnToTitle()
    {
        SceneManager.LoadScene(0);
    }
    public static void GoToResults(int currentPoints)
    {
        Get().pointsTransfer = currentPoints;
        SceneManager.LoadScene(2);
    }

}

[System.Serializable]
public class CakeToppersSaveData : SaveData<CakeToppersSaveData>
{
    protected override string FileName() => "CakeToppersData";

    [SerializeField] private int[] levelScores;

    protected override void BeforeSave()
    {
        LevelData[] levels = GameMainManager.Get().AllLevels;

        levelScores = new int[levels.Length];
        for (int i = 0; i < levelScores.Length; i++) 
            levelScores[i] = levels[i].highScore;
    }
    protected override void AfterLoad()
    {
        LevelData[] levels = GameMainManager.Get().AllLevels;

        for (int i = 0; i < levelScores.Length; i++)
            levels[i].highScore = levelScores[i];

    }

}