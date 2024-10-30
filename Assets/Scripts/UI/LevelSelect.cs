using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    public GameObject OptionsPanel;
    public GameObject CreditsPanel;
    public string nextScene;
    
    public void OpenOptionsPanel()
    {
        OptionsPanel.SetActive(true);
    }
    public void CloseOptionsPanel()
    {
        OptionsPanel.SetActive(false);
    }
    public void OpenCreditsPanel()
    {
        CreditsPanel.SetActive(true);
    }
    public void CloseCreditsPanel()
    {
        CreditsPanel.SetActive(false);
    }

    public void SceneSelect()
    {
        SceneManager.LoadScene(nextScene);
    }

    public void Back()
    {
        //WorldPanel.SetActive(false);
        //WorldSelectPanel.SetActive(true);
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
