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

    public Image[] cookies;

    private void Awake()
    {
        GameMainManager.Get();

        int i = 0, i2 = 0;
        for (; i < levelButtons.Length; i++)
        {
            if(i != 0) levelButtons[i].interactable = GameMainManager.Get().AllLevels[i - 1].MinComplete();

            int cook = GameMainManager.Get().AllLevels[i].Cookies();
            cookies[i2++].gameObject.SetActive(cook > 0);
            cookies[i2++].gameObject.SetActive(cook > 1);
            cookies[i2++].gameObject.SetActive(cook > 2);
        }
            
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
