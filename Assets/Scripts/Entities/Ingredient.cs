using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ingredient : MonoBehaviour
{

	public abstract void UpdateFlavor();

	public Ingredient Instantiate(Transform parent)
	{
		GameObject newIng = Instantiate(gameObject);
		newIng.transform.parent = parent;
		newIng.transform.Reset(scale: false);
		newIng.SetActive(true);
		return newIng.GetComponent<Ingredient>();
	}
}
