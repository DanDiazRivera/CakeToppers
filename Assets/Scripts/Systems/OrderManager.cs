using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class OrderManager : Singleton<OrderManager>
{

    private D_Cake currentOrder;

    [SerializeField] private RandomizerData randoData;
    [System.Serializable]
    private struct RandomizerData
    {
        public Ing_CakeBase[] cakeBaseOptions;
        public Ing_Frosting_Cover[] frostingCoverOptions;
        public Ing_Fruit[] fruitOptions;

        public void HandleIngredients(List<Ingredient> levelIngredients)
        {
            cakeBaseOptions = (from check in levelIngredients where check is Ing_CakeBase
                select check as Ing_CakeBase ).ToArray();
            frostingCoverOptions = (from check in levelIngredients where check is Ing_Frosting_Cover
                select check as Ing_Frosting_Cover ).ToArray();
            fruitOptions = (from check in levelIngredients where check is Ing_Fruit
                select check as Ing_Fruit ).ToArray();
        } 



    }


    protected override void OnAwake()
    {
        randoData.HandleIngredients(LevelManager.Get().levelData.ingredients);
        RandomizeDesert();
    }


    public void RandomizeDesert()
    {
        if (currentOrder != null)
        {
            Destroy(currentOrder.gameObject);
            currentOrder = null;
        }
        var result = D_Cake.CreateRandom(transform, randoData.cakeBaseOptions, randoData.frostingCoverOptions, randoData.fruitOptions);
        currentOrder = result;
        currentOrder.transform.localScale = Vector3.one;

    }


    public void SubmitDesert(D_Cake input)
    {
        int score = Compare(currentOrder, input);
        LevelManager.Get().AddScore(score);
        Destroy(currentOrder.gameObject);
        RandomizeDesert();
    }


    [SerializeField] private ComparisonData compareData = new ();
    [System.Serializable]
    private struct ComparisonData
    {
        public int noCakeNegative;
        public int wrongCake;
        public int rightCake;
        public int wrongFrost;
        public int rightFrost;
        public int vagueFruit;
        public int nearFruit;
        public int perfectFruit;
    }

    public int Compare(D_Cake orderCake, D_Cake playerCake)
    {
        if (playerCake.cakeBase is null) return compareData.noCakeNegative;
        int finalScore = 0;

        if (Ingredient.Compare(playerCake.cakeBase, orderCake.cakeBase)) finalScore += compareData.rightCake;
        else finalScore += compareData.wrongCake;

        if (playerCake.frostingCover is null) finalScore += 0;
        else if (Ingredient.Compare(playerCake.frostingCover, orderCake.frostingCover)) finalScore += compareData.rightFrost;
        else finalScore += compareData.wrongFrost;

        float range = 1.3f;

        Collider[] overlapResults = new Collider[5];
        for (int i = 0; i < orderCake.fruits.Count; i++)
        {
            int count = Physics.OverlapSphereNonAlloc(playerCake.transform.position + orderCake.fruits[i].transform.localPosition, range, overlapResults, ~0);
            Transform chosenFruit = null;
            for (int i2 = 0; i2 < count; i2++)
            {
                if (overlapResults[i2]
                    && overlapResults[i2].gameObject.TryGetComponent(out Ing_Fruit thisFruit)
                    && Ingredient.Compare(thisFruit, orderCake.fruits[i]))
                {
                    chosenFruit = overlapResults[i2].transform;
                    break;
                }
            }
            if (chosenFruit == null) continue;

            finalScore += compareData.vagueFruit;

            float distance = Vector2.Distance(chosenFruit.localPosition, orderCake.fruits[i].transform.localPosition);
            if (distance < range / 2f) finalScore += compareData.nearFruit;
            if (distance < range / 4f) finalScore += compareData.perfectFruit;

        }


        return finalScore;
    }






}
