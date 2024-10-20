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

        public void HandleIngredients(List<ListC<GameObject>> levelIngredients)
        {
            cakeBaseOptions = (
                from check in levelIngredients[0].List 
                where check.activeSelf && check.TryGetComponent(out IngredientButton check2) && check2.ingredient is Ing_CakeBase
                select check.GetComponent<IngredientButton>().ingredient as Ing_CakeBase ).ToArray();
            frostingCoverOptions = (
                from check in levelIngredients[1].List
                where check.activeSelf && check.TryGetComponent(out IngredientButton check2) && check2.ingredient is Ing_Frosting_Cover
                select check.GetComponent<IngredientButton>().ingredient as Ing_Frosting_Cover ).ToArray();
            fruitOptions = (
                from check in levelIngredients[2].List
                where check.activeSelf && check.TryGetComponent(out IngredientButton check2) && check2.ingredient is Ing_Fruit
                select check.GetComponent<IngredientButton>().ingredient as Ing_Fruit ).ToArray();
        } 



    }


    protected override void OnAwake()
    {
        randoData.HandleIngredients(LevelManager.Get().ingredientGroups);
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
        int score = currentOrder.Compare(input);
        LevelManager.Get().AddScore(score);
        Destroy(currentOrder.gameObject);
        RandomizeDesert();
    }

}
