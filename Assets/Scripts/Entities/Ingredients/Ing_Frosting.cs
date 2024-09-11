using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ing_Frosting : Ingredient
{
	public Fla_Frosting flavor;
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
