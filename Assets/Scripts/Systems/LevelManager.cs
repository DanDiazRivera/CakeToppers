using EditorAttributes;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using TextMP = TMPro.TMP_Text;

public class LevelManager : Singleton<LevelManager>
{
    #region Config
    [SerializeField] float levelTime;

    public LevelData levelData;

    #endregion
    #region Components

    [SerializeField] GameObject levelEndCanvas;
    [SerializeField] GameObject winDialogue;
    [SerializeField] GameObject loseDialogue;
    [SerializeField] GameObject pauseCanvas;
    [SerializeField] TextMP currentTimerText;
    [SerializeField] TextMP currentScoreText;

    #endregion
    #region Data

    [SerializeField, ReadOnly]
    float currentLevelTime;
    [SerializeField, ReadOnly]
    int currentScore;

    private bool paused;

    #endregion





    protected override void OnAwake()
    {
        Input.Get();
        currentScore = 0;
        levelData = GameMainManager.Get().levelData ?? levelData;
        HandleIngredients();
        currentLevelTime = levelData.time;
        AddScore(0);
    }

    void Update()
    {
        if (paused) return;
        if (currentLevelTime <= 0) return;
        currentLevelTime -= Time.deltaTime;
        if (currentLevelTime <= 0) EndLevel();
        currentTimerText.text = ((int)currentLevelTime).ToString();
    }

    void EndLevel()
    {
        GameMainManager.Get().levelData ??= levelData;
        GameMainManager.GoToResults(currentScore);
    }

    public void ReturnToTitle() => GameMainManager.ReturnToTitle();

    public void AddScore(int score)
    {
        currentScore += score;
        currentScoreText.text = currentScore.ToString() + "\n/" + levelData.minScore.ToString();
    }

    public void SetPause(bool value)
    {
        paused = value;
        pauseCanvas.SetActive(value);
    }

    public void HandleIngredients()
    {
        if (levelData is null) return;

        IngredientButton[] buttons = FindObjectsOfType<IngredientButton>(true);

        IngredientButton[] disableButtons = (from button in buttons
                                                 where !levelData.ingredients.Contains(button.ingredient)
                                                 select button).ToArray();

        foreach (IngredientButton button in disableButtons) button.gameObject.SetActive(false);

        IngredientTabManager tabMan = FindFirstObjectByType<IngredientTabManager>();
        tabMan.Awake();



    }

}

[System.Serializable]
public struct ListC<T>
{
    [SerializeField] public List<T> List;

    public T this[int i]
    {
        get => List[i];
        set => List[i] = value;
    }
    public int Count => List.Count;
}