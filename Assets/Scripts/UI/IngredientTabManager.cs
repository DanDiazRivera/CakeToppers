using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class IngredientTabManager : MonoBehaviour
{
	RectTransform[] tabs;
	Button[] tabButtons;

	ScrollRect scroll;

	public void Awake()
	{
		scroll = GetComponent<ScrollRect>();

		Transform tabGroup = transform.Find("Viewport");
		Transform tabButtonGroup = transform.Find("TabGroup");

		tabs = new RectTransform[tabGroup.childCount];
		tabButtons = new Button[tabGroup.childCount];
		for (int i = 0; i < tabs.Length; i++)
        {
			tabs[i] = tabGroup.GetChild(i).GetComponent<RectTransform>();
			tabButtons[i] = tabButtonGroup.GetChild(i).GetComponent<Button>();

			bool hasStuff = false;
            foreach (Transform T in tabs[i])
            {
                if (T.gameObject.activeSelf)
                {
					hasStuff = true;
					break;
                }
            }
			if (!hasStuff) tabButtons[i].gameObject.SetActive(false);

		}
	}

	public void ChangeTab(RectTransform tab)
	{
		scroll.content = tab;
		foreach (var offTab in tabs) offTab.gameObject.SetActive(false);
		tab.gameObject.SetActive(true);
	}
}
