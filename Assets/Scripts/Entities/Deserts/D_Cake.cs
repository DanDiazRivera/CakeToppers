using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static OrderManager;

public class D_Cake : Desert
{
    public Ing_CakeBase cakeBase;
    public Ing_Frosting_Cover frostingCover;
    public List<Ing_Fruit> fruits;
    public List<Ing_FrostingDraw> icings;


    private void Awake()
    {
        fruits = new List<Ing_Fruit>();
        icings = new List<Ing_FrostingDraw>();
    }

    public override Ingredient AddIngredient(Ingredient ingredient, Vector3 position)
    {
        if (ingredient is Ing_CakeBase)
        {
            if (Ingredient.Compare(ingredient, cakeBase)) return null;
            if (frostingCover) return null;
            SetIngredientSlot(ref cakeBase, ingredient.Instantiate(transform) as Ing_CakeBase);
            return cakeBase;
        }
        else if (ingredient is Ing_Frosting_Cover)
        {
            if (Ingredient.Compare(ingredient, frostingCover)) return null;
            if (fruits.Count > 0 || !cakeBase) return null;
            SetIngredientSlot(ref frostingCover, ingredient.Instantiate(transform) as Ing_Frosting_Cover);
            return frostingCover;
        }
        else if (ingredient is Ing_Fruit)
        {
            if (!cakeBase || !frostingCover) return null;
            fruits.Add(ingredient.Instantiate(transform) as Ing_Fruit);
            fruits[^1].transform.position = position;
            return fruits[^1];
        }
        else if (ingredient is Ing_FrostingDraw)
        {
            if (!cakeBase || !frostingCover) return null;

            Ingredient ret = icings.FirstOrDefault<Ingredient>(check => Ingredient.Compare(ingredient, check));
            if (ret != null) return ret;
            icings.Add(ingredient.Instantiate(transform) as Ing_FrostingDraw);
            return icings[^1];
        }
        return null;
    }

    public static D_Cake CreateRandom(Transform parent, RandomizerData data)
    {
        D_Cake result = Create<D_Cake>("Order Cake", parent);

        result.AddIngredient(data.cakeBaseOptions.Random(), Vector3.zero);
        result.AddIngredient(data.frostingCoverOptions.Random(), Vector3.zero);

        int fruitCount = Random.Range(1, 6);

        for (int i = 0; i < fruitCount; i++)
        {
            var thisNewFruit = result.AddIngredient(data.fruitOptions.Random(), Vector3.zero) as Ing_Fruit;
            thisNewFruit.transform.localPosition = new(Random.Range(-1.65f, 1.65f), 1.20f, Random.Range(-1.65f, 1.65f));
        }

        if(data.icingOptions.Length > 0)
        {
            result.AddIngredient(data.icingOptions.Random(), Vector3.zero);
            result.icings[^1].SetTexture(data.icingShapes.Random());

        }


        return result;
    }
}
