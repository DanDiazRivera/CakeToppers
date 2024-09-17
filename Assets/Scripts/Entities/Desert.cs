using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Desert : MonoBehaviour
{


	public static T Create<T>(Transform parent = null)
	{
		GameObject @object = new("Cake", typeof(T));
		@object.TryGetComponent(out T final);
		if (parent != null) @object.transform.parent = parent;
		@object.transform.localPosition = Vector3.zero;
		@object.transform.localRotation = Quaternion.identity;
		@object.transform.localScale = Vector3.one * 4;
		return final;
	}

	public abstract void AddIngredient(Ingredient ingredient, Vector3 position);

	protected void SetIngredientSlot<T>(ref T slot, T ing) where T : Ingredient
	{
		if(slot != null) Destroy(slot.gameObject);
		slot = ing;
	}

}
