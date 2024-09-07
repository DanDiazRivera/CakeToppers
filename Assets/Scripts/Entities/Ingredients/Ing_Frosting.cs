using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ing_Frosting : Ingredient
{
	public Fla_Frosting flavor;
	public Color color;

	private void Awake()
	{
		color = flavor.color;
	}




}
