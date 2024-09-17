using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_Cake : Desert
{
	public Ing_CakeBase cakeBase;
	public Ing_Frosting_Cover frostingCover;
	public List<Ing_Fruit> fruits;

	private void Awake() => fruits = new List<Ing_Fruit>();

	public override void AddIngredient(Ingredient ingredient, Vector3 position)
	{
		if(ingredient is Ing_CakeBase)
		{
			if (frostingCover) return;
			SetIngredientSlot(ref cakeBase, ingredient.Instantiate(transform) as Ing_CakeBase);
		}
		else if(ingredient is Ing_Frosting_Cover)
		{
			if (fruits.Count > 0 || !cakeBase) return;
			SetIngredientSlot(ref frostingCover, ingredient.Instantiate(transform) as Ing_Frosting_Cover);
		}
		else if(ingredient is Ing_Fruit)
		{
			if (!cakeBase || !frostingCover) return;
			fruits.Add(ingredient.Instantiate(transform) as Ing_Fruit);
			fruits[^1].transform.position = position;
		}
	}




}
