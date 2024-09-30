using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_Cake : Desert
{
	public Ing_CakeBase cakeBase;
	public Ing_Frosting_Cover frostingCover;
	public List<Ing_Fruit> fruits;

	private void Awake() => fruits = new List<Ing_Fruit>();

	public override Ingredient AddIngredient(Ingredient ingredient, Vector3 position)
	{
		if(ingredient is Ing_CakeBase)
		{
			if (Ingredient.Compare(ingredient, cakeBase)) return null;
			if (frostingCover) return null;
			SetIngredientSlot(ref cakeBase, ingredient.Instantiate(transform) as Ing_CakeBase);
			return cakeBase;
		}
		else if(ingredient is Ing_Frosting_Cover)
		{
			if (Ingredient.Compare(ingredient, frostingCover)) return null;
			if (fruits.Count > 0 || !cakeBase) return null;
			SetIngredientSlot(ref frostingCover, ingredient.Instantiate(transform) as Ing_Frosting_Cover);
			return frostingCover;
		}
		else if(ingredient is Ing_Fruit)
		{
			if (!cakeBase || !frostingCover) return null;
			fruits.Add(ingredient.Instantiate(transform) as Ing_Fruit);
			fruits[^1].transform.position = position;
			return fruits[^1];
		}
		return null; 
	}

	public static D_Cake CreateRandom(Transform parent ,Ing_CakeBase[] bases, Ing_Frosting_Cover[] frostingCovers, Ing_Fruit[] fruits)
	{
		D_Cake result = Create<D_Cake>("Order Cake", parent);

		result.AddIngredient(bases.Random(), Vector3.zero);
		result.AddIngredient(frostingCovers.Random(), Vector3.zero);

		Ing_Fruit fruit = result.AddIngredient(fruits[0], Vector3.zero) as Ing_Fruit;
		fruit.transform.localPosition = new (Random.Range(-1.65f, 1.65f), 1.35f, Random.Range(-1.65f, 1.65f)); 


		/*
		int fruitAmount = Random.Range(1, 4);
		for (int i = 1; i <= fruitAmount; i++)
		{
			result.AddIngredient(frostingCovers.Random(), Vector3.zero);
		}
		 */

		return result;
	}


}
