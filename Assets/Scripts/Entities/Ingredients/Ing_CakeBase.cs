using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ing_CakeBase : Ingredient
{
	public Fla_Cake flavor;
	public Color color;

	private void Awake()
	{
		color = flavor.color;
	}




}
