using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Desert : MonoBehaviour
{


	public static T Create<T>(string name = "NewDesert", Transform parent = null)
	{
		GameObject @object = new(name, typeof(T));
		@object.TryGetComponent(out T final);
		if (parent != null) @object.transform.parent = parent;
		@object.transform.Reset();
		return final;
	}

	public abstract Ingredient AddIngredient(Ingredient ingredient, Vector3 position);

	protected void SetIngredientSlot<T>(ref T slot, T ing) where T : Ingredient
	{
		if(slot != null) Destroy(slot.gameObject);
		slot = ing;
	}

}
