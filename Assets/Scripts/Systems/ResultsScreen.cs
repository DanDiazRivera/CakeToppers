using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultsScreen : MonoBehaviour
{

    [SerializeField] GameObject[] cookies = new GameObject[3];
    [SerializeField] GameObject buttons;
    [SerializeField] Slider progress;
    [SerializeField] TMPro.TextMeshProUGUI scoreText;
    [SerializeField] RectTransform[] points;

    private void Awake()
    {
        var manager = GameMainManager.Get();
        int newScore = manager.pointsTransfer;
        LevelData level = manager.levelData;

        if (level.highScore < newScore) level.highScore = newScore;
        if (manager.AllLevels.Contains(manager.levelData)) manager.saveData.Save();

        //Show Positions
        {
            float min = -208.12f, max = 208.12f;

            points[0].anchoredPosition = new Vector2(Mathf.Lerp(min, max, (float)level.minScore / (float)level.cookie3Score), points[0].anchoredPosition.y);
            points[1].anchoredPosition = new Vector2(Mathf.Lerp(min, max, (float)level.cookie2Score / (float)level.cookie3Score), points[1].anchoredPosition.y);
            points[2].anchoredPosition = new Vector2(max, points[2].anchoredPosition.y);

        }
        scoreText.text = 0.ToString();

        StartCoroutine(CookieShowEnum(newScore, level));
    }

    IEnumerator CookieShowEnum(int score, LevelData level)
    {
        yield return new WaitForSeconds(0.5f);

        int scoreCounter = 0;
        int maxScore = level.cookie3Score;

        while (scoreCounter < score && scoreCounter < maxScore)
        {
            yield return null;
            scoreCounter++;

            if (scoreCounter == level.minScore) ShowCookie(0);
            if (scoreCounter == level.cookie2Score) ShowCookie(1);
            if (scoreCounter == level.cookie3Score) ShowCookie(2);

            progress.value = (float)scoreCounter / (float)maxScore;
            scoreText.text = scoreCounter.ToString();
        }
        scoreText.text = score.ToString();

        yield return new WaitForSeconds(0.5f);

        buttons.SetActive(true);
    }

    void ShowCookie(int i) => cookies[i].SetActive(true);

    public void LoadScene(string value) => SceneManager.LoadScene(value);

    public void SkipToNextLevel()
    {
        var main = GameMainManager.Get();

        for (int i = 0; i < main.AllLevels.Length; i++) 
            if (main.AllLevels[i] == main.levelData)
            {
                main.levelData = main.AllLevels[i + 1];
                break;
            }
    }
}
