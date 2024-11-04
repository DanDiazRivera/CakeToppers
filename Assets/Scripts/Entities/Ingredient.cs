using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ingredient : MonoBehaviour
{

	public string signature;
	public AnimationClip placeAnimation;
	public AudioClip placeAudio;

	public static bool Compare(Ingredient A, Ingredient B)
	{
		if (A == null || B == null) return false;
		return A.GetType() == B.GetType() && A.signature == B.signature;
	}

	public Ingredient Instantiate(Transform parent)
	{
		GameObject newIng = Instantiate(gameObject);
		newIng.transform.parent = parent;
		newIng.transform.Reset();
		newIng.SetActive(true);
		this.AlterTransforms();
		return newIng.GetComponent<Ingredient>();
	}
	protected virtual void AlterTransforms() { }

	public override bool Equals(object obj) => obj is Ingredient ingredient && base.Equals(obj) && name == ingredient.name && signature == ingredient.signature;
	public override int GetHashCode() => HashCode.Combine(base.GetHashCode(), name, signature);
}

public interface IStaticIngredient { }
public interface IObjectIngredient { }
public interface IPaintIngredient { }