using EditorAttributes;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;
using TextMP = TMPro.TMP_Text;

public class LevelManager : Singleton<LevelManager>
{
    #region Config
    [SerializeField] float levelTime;
    [SerializeField] int minWinScore;

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
        currentLevelTime = levelTime;
        currentScore = 0;
        AddScore(0);
        var selectLevelData = GameMainManager.Get().levelData;
        levelData = selectLevelData ? selectLevelData : levelData;
        HandleIngredients();
    }

    void Update()
    {
        if (paused) return;
        if (currentLevelTime <= 0) return;
        currentLevelTime -= Time.deltaTime;
        if (currentLevelTime <= 0) EndLevel();
        currentTimerText.text = ((int)currentLevelTime).ToString();
    }

    void EndLevel() => GameMainManager.GoToResults(currentScore, minWinScore);/*
		levelEndCanvas.SetActive(true);
		(currentScore >= minWinScore ? winDialogue : loseDialogue).SetActive(true);
		 */

    public void ReturnToTitle() => GameMainManager.ReturnToTitle();

    public void AddScore(int score)
    {
        currentScore += score;
        currentScoreText.text = currentScore.ToString() + "\n/" + minWinScore.ToString();
    }

    public void SetPause(bool value)
    {
        paused = value;
        pauseCanvas.SetActive(value);
    }

    public void HandleIngredients()
    {
        if (levelData is null) return;

        IngredientButton[] buttons = FindObjectsOfType<IngredientButton>();

        IngredientButton[] disableButtons = (IngredientButton[]) from button in buttons
                                            where !levelData.ingredients.Contains(button.ingredient)
                                            select button;

        foreach(var button in buttons) button.gameObject.SetActive(true);

        var tabMan = FindFirstObjectByType<IngredientTabManager>();
        tabMan.Awake();



    }

}

[System.Serializable]
public struct ListC<T>
{
    [SerializeField] public List<T> List;

    public T this[int i]
    {
        get { return List[i]; }
        set { List[i] = value; }
    }
    public int Count => List.Count;
}