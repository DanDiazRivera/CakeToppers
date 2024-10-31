using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    public GameObject CreditsPanel;
    public GameObject OptionsPanel;
    public string nextScene;

    public void SceneSelect() => SceneManager.LoadScene(nextScene);

    public void BeginLevel(LevelData levelData)
    {
        GameMainManager.BeginGame(levelData);
    }

    public void ReturnToTitle() => SceneManager.LoadScene("MainMenu");

    public Button[] levelButtons;

    public RectTransform[] worlds;

    private void Awake()
    {
        GameMainManager.Get();

        for (int i = 1; i < levelButtons.Length; i++)
            levelButtons[i].interactable = GameMainManager.Get().AllLevels[i - 1].MinComplete();
    }

    public void ShowWorld(RectTransform thisWorld)
    {
        foreach (var world in worlds) world.gameObject.SetActive(world == thisWorld);
    }
    
    public void OpenCreditPanel()
    {
        CreditsPanel.SetActive(true);
    }
    public void OpenOptionsPanel()
    {
        OptionsPanel.SetActive(true);
    }
    public void CloseCreditPanel()
    {
        CreditsPanel.SetActive(false);
    }
    public void CloseOptionsPanel()
    {
        OptionsPanel.SetActive(false);
    }
}
