using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_Cake : Desert
{
	public Ing_CakeBase cakeBase;
	public Ing_Frosting_Cover frostingCover;
	public List<Ing_Fruit> fruits;

	private void Awake() => fruits = new List<Ing_Fruit>();



	public static D_Cake Create(Transform parent = null)
	{
		GameObject @object = new("Cake", typeof(D_Cake));
		@object.TryGetComponent(out D_Cake final);
		if(parent != null) @object.transform.parent = parent;
		@object.transform.localPosition = Vector3.zero;
		@object.transform.localRotation = Quaternion.identity;
		@object.transform.localScale = Vector3.one * 4;
		return final;
	}


}
