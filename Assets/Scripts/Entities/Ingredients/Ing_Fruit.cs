using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ing_Fruit : Ingredient
{
	public Fla_Fruit flavor;
	public Color color;
	public GameObject model;

	private void Awake()
	{
		color = flavor.color;
		model = Instantiate(flavor.prefab);
	}




}
