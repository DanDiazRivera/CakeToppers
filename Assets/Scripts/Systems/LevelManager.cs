using System.Collections;
using System.Collections.Generic;
using EditorAttributes;
using UnityEngine;
using TextMP = TMPro.TMP_Text;

public class LevelManager : Singleton<LevelManager>
{
	#region Config
	[SerializeField] float levelTime;
	[SerializeField] int minWinScore;
	[SerializeField] List<List<GameObject>> ingredients;

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
		if(paused) return;
		if (currentLevelTime <= 0) return;
		currentLevelTime -= Time.deltaTime;
		if (currentLevelTime <= 0) EndLevel();
		currentTimerText.text = ((int)currentLevelTime).ToString();
	}

	void EndLevel()
	{
		GameMainManager.GoToResults(currentScore, minWinScore);
		/*
		levelEndCanvas.SetActive(true);
		(currentScore >= minWinScore ? winDialogue : loseDialogue).SetActive(true);
		 */
	}

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

	public void HandleIngredients(List<List<bool>> levelIngredients)
    {
        for (int i1 = 0; i1 < ingredients.Count; i1++)
        {
			bool hasOne = false;
            for (int i2 = 1; i2 < ingredients[i1].Count; i2++)
            {
				if (levelIngredients[i1][i2] == true) hasOne = true;
				else ingredients[i1][i2].SetActive(false);
            }
			ingredients[i1][0].SetActive(hasOne);
        }
    }

}
