using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientTabManager : MonoBehaviour
{
	RectTransform[] tabs;

	ScrollRect scroll;

	private void Awake()
	{
		scroll = GetComponent<ScrollRect>();
		Transform tabGroup = transform.Find("Viewport");
		tabs = new RectTransform[tabGroup.childCount];
		for (int i = 0; i < tabGroup.childCount; i++)
			tabs[i] = tabGroup.GetChild(i).GetComponent<RectTransform>();
	}

	public void ChangeTab(RectTransform tab)
	{
		scroll.content = tab;
		foreach (var offTab in tabs) offTab.gameObject.SetActive(false);
		tab.gameObject.SetActive(true);
	}
}
