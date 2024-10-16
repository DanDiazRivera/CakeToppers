using EditorAttributes;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using TextMP = TMPro.TMP_Text;

public class LevelManager : Singleton<LevelManager>
{
    #region Config
    [SerializeField] float levelTime;
    [SerializeField] int minWinScore;
    [SerializeField] public List<ListC<GameObject>> ingredientGroups;
    [SerializeField] public List<ListC<bool>> defIngredients;

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
        HandleIngredients(GameMainManager.Get().levelData);
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

    public void HandleIngredients(List<ListC<bool>> levelIngredients)
    {
        if (levelIngredients is null) levelIngredients = defIngredients;
        for (int i1 = 0; i1 < ingredientGroups.Count; i1++)
        {
            bool hasOne = false;
            for (int i2 = 1; i2 < ingredientGroups[i1].Count; i2++)
            {
                if (levelIngredients[i1][i2-1] == true) hasOne = true;
                else ingredientGroups[i1][i2].SetActive(false);
            }
            ingredientGroups[i1][0].SetActive(hasOne);
        }
        
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