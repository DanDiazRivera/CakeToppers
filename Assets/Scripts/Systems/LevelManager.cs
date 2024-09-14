using System.Collections;
using System.Collections.Generic;
using EditorAttributes;
using UnityEngine;
using TextMP = TMPro.TextMeshProUGUI;

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
	[SerializeField] TextMP currentTimerText;
	[SerializeField] TextMP currentScoreText;

	#endregion
	#region Data

	[SerializeField, ReadOnly]
	float currentLevelTime;
	[SerializeField, ReadOnly]
	float currentScore;

	#endregion





	protected override void OnAwake()
	{
		Input.Get();
		currentLevelTime = levelTime;
		currentScore = 0;
		currentScoreText.text = currentScore.ToString();
	}

	void Update()
	{
		if (currentLevelTime <= 0) return;
		currentLevelTime -= Time.deltaTime;
		if (currentLevelTime <= 0) EndLevel();
		currentTimerText.text = ((int)currentLevelTime).ToString();
	}

	void EndLevel()
	{
		levelEndCanvas.SetActive(true);
		(currentScore >= minWinScore ? winDialogue : loseDialogue).SetActive(true);
	}

	public void AddScore(int score)
	{
		currentScore += score;
		currentScoreText.text = currentScore.ToString();
	}

}
