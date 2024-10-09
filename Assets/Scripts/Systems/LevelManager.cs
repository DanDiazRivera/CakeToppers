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

}
