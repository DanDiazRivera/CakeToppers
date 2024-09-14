using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ing_CakeBase : Ingredient
{
	public Fla_Cake flavor;
	public Color color;

	Material material;

	private void Awake()
	{
		material = GetComponent<MeshRenderer>().material;
		UpdateFlavor();
	}

	public override void UpdateFlavor()
	{
		color = flavor.color;
		material.color = color;
	}


}