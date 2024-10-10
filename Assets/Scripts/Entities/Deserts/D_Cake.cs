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

		int fruitCount = Random.Range(1, 6);

		for (int i = 0; i < fruitCount; i++)
		{
            Ing_Fruit thisNewFruit = result.AddIngredient(fruits.Random(), Vector3.zero) as Ing_Fruit;
            thisNewFruit.transform.localPosition = new(Random.Range(-1.65f, 1.65f), 1.35f, Random.Range(-1.65f, 1.65f));
        }

		/*
		int fruitAmount = Random.Range(1, 4);
		for (int i = 1; i <= fruitAmount; i++)
		{
			result.AddIngredient(frostingCovers.Random(), Vector3.zero);
		}
		 */

		return result;
	}

    public int Compare(D_Cake playerCake)
    {
        if (playerCake.cakeBase is null) return 0;
        int finalScore = 0;

        if (Ingredient.Compare(playerCake.cakeBase, cakeBase)) finalScore += 50;
        else finalScore += 5;

        if (playerCake.frostingCover is null) finalScore += 0;
        else if (Ingredient.Compare(playerCake.frostingCover, frostingCover)) finalScore += 50;
        else finalScore += 10;

		float range = 1.3f;

		Collider[] overlapResults = new Collider[5];
        for (int i = 0; i < fruits.Count; i++)
		{
			int count = Physics.OverlapSphereNonAlloc(playerCake.transform.position + fruits[i].transform.localPosition, range, overlapResults, ~0);
			Transform chosenFruit = null;
			for (int i2 = 0; i2 < count; i2++)
			{
				if (overlapResults[i2] 
					&& overlapResults[i2].gameObject.TryGetComponent(out Ing_Fruit thisFruit) 
					&& Ingredient.Compare(thisFruit, fruits[i]))
				{
                    chosenFruit = overlapResults[i2].transform;
					break; 
                }
			}
			if (chosenFruit == null) continue;

            finalScore += 10;

            float distance = Vector2.Distance(chosenFruit.localPosition, fruits[i].transform.localPosition);
			Debug.Log(distance);
			if (distance < range / 2f) finalScore += 25;
			if (distance < range / 4f) finalScore += 25;

        }
		/*
        if (playerCake.fruits.Count < 1) finalScore += 0;
        else
        {
		 
            finalScore += 10;

            Vector2 inputStrawbPos = new(playerCake.fruits[0].transform.localPosition.x, playerCake.fruits[0].transform.localPosition.z);
            Vector2 orderStrawbPos = new(fruits[0].transform.localPosition.x, fruits[0].transform.localPosition.z);

            float distance = Vector2.Distance(inputStrawbPos, orderStrawbPos);

            finalScore += distance switch
            {
                < 0.2192031f => 50,
                < 0.4384062f => 25,
                _ => 0
            };

            finalScore += playerCake.fruits.Count - 1;
        }*/

        return finalScore;
    }

}
