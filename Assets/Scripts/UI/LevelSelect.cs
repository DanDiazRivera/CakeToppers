using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
}
