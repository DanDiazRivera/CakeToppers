using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    public GameObject WorldSelectPanel;
    public GameObject WorldPanel;
    public string nextScene;
    
    public void OpenPanel()
    {
        WorldPanel.SetActive(true);
    }
    public void ClosePanel()
    {
        WorldSelectPanel.SetActive(false);
    }
    
    public void SceneSelect()
    {
        SceneManager.LoadScene(nextScene);
    }

    public void Back()
    {
        WorldPanel.SetActive(false);
        WorldSelectPanel.SetActive(true);
    }

    public void BeginLevel(LevelData levelData)
    {
        GameMainManager.BeginGame(levelData);
    }

    public void ReturnToTitle()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public Button[] levelButtons;

    private void Awake()
    {
        for (int i = 1; i < levelButtons.Length; i++) 
            levelButtons[i].interactable = GameMainManager.Get().AllLevels[i - 1].MinComplete();
    }
}
