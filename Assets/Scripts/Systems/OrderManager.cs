using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;


public class OrderManager : Singleton<OrderManager>
{

    private D_Cake currentOrder;
    private Camera orderCam;

    [SerializeField, HideInInspector] public RandomizerData randoData;
    [System.Serializable]
    public struct RandomizerData
    {
        public Ing_CakeBase[] cakeBaseOptions;
        public Ing_Frosting_Cover[] frostingCoverOptions;
        public Ing_Fruit[] fruitOptions;
        public FruitArrangment[] fruitArrangmentOptions;
        public Ing_FrostingDraw[] icingOptions;
        public Texture2D[] icingShapes;

        public void HandleIngredients(LevelData data)
        {
            cakeBaseOptions = (from X in data.ingredients
                               where X is Ing_CakeBase
                               select X as Ing_CakeBase).ToArray();
            frostingCoverOptions = (from X in data.ingredients
                                    where X is Ing_Frosting_Cover
                                    select X as Ing_Frosting_Cover).ToArray();
            fruitOptions = (from X in data.ingredients
                            where X is Ing_Fruit
                            select X as Ing_Fruit).ToArray();
            icingOptions = (from X in data.ingredients
                            where X is Ing_FrostingDraw
                            select X as Ing_FrostingDraw).ToArray();
            fruitArrangmentOptions = data.fruitArrangments.ToArray();
            icingShapes = data.icingShapes.ToArray();

        }



    }


    protected override void OnAwake()
    {
        randoData.HandleIngredients(LevelManager.Get().levelData);
        orderCam = transform.GetChild(0).GetComponent<Camera>();
    }

    private void Start()
    {
        RandomizeDesert();
        orderCam.enabled = false;
    }

    public void RandomizeDesert()
    {
        if (currentOrder != null)
        {
            DestroyImmediate(currentOrder.gameObject);
            currentOrder = null;
        }
        var result = D_Cake.CreateRandom(transform, randoData);
        currentOrder = result;
        currentOrder.transform.localScale = Vector3.one;

        Transform[] children = GetComponentsInChildren<Transform>();
        foreach (Transform tran in children) tran.gameObject.layer = 7;

        orderCam.Render();
    }


    public void SubmitDesert(D_Cake input)
    {
        LevelManager.Get().AddScore(Compare(currentOrder, input));
        Destroy(currentOrder.gameObject);
        RandomizeDesert();
    }


    [SerializeField] private ComparisonDataNew comp = new();
    [System.Serializable]
    private class ComparisonDataNew
    {
        public int noCakeNegative = -20;
        public int cakeValue = 50;
        public int frostingValue = 50;
        public int totalFruitValue = 50;

        public float minIcingSimilarity = .3f;
        public float maxIcingSimilarity = .8f;
        public int maxIcingValue = 50;


        public float minRatioForSound = .6f;

        public SoundFXManager.RandomizedAudio goodSound;
        public SoundFXManager.RandomizedAudio badSound;

    }


    public int Compare(D_Cake orderCake, D_Cake playerCake)
    {
        if (playerCake.cakeBase is null)
        {
            SoundFXManager.Get().PlaySound(comp.badSound);
            return comp.noCakeNegative;
        }
        int finalScore = 0;

        {
            if (Ingredient.Compare(playerCake.cakeBase, orderCake.cakeBase)) finalScore += comp.cakeValue;
            else finalScore += comp.cakeValue / 2;
        } // Cake Base

        {
            if (Ingredient.Compare(playerCake.frostingCover, orderCake.frostingCover)) finalScore += comp.frostingValue;
            else finalScore += comp.frostingValue / 2;
        } // Frosting Cover

        {
            int orderFruitCount = orderCake.fruits.Count;
            int playerFruitCount = orderCake.fruits.Count;

            float indiFruitValue = ((float)comp.totalFruitValue) / orderFruitCount;
            float fruitValueComp = 0;

            float range = 1.3f;

            var overlapResults = new RaycastHit[5];
            for (int i = 0; i < orderFruitCount; i++)
            {
                int count = Physics.SphereCastNonAlloc(
                    playerCake.transform.position + orderCake.fruits[i].transform.localPosition - Vector3.up,
                    range, Vector3.up, overlapResults, 2, ~0);
                Ingredient chosenFruit = null;
                for (int i2 = 0; i2 < count; i2++)
                    if (overlapResults[i2].transform.TryGetComponent(out Ing_Fruit thisFruit) && playerCake.fruits.Contains(thisFruit))
                    {
                        chosenFruit = thisFruit;
                        break;
                    }
                if (chosenFruit != null)
                {
                    bool isClose = Vector2.Distance(chosenFruit.transform.localPosition, orderCake.fruits[i].transform.localPosition) < range / 3f;
                    bool isCorrectType = Ingredient.Compare(chosenFruit, orderCake.fruits[i]);

                    if (isClose && isCorrectType) fruitValueComp += indiFruitValue;
                    else if (!isClose && isCorrectType) fruitValueComp += indiFruitValue / 2;
                    else if (isClose && !isCorrectType) fruitValueComp += indiFruitValue / 3;
                    else if (!isClose && !isCorrectType) fruitValueComp += indiFruitValue / 4;
                }
                else if (i <= playerFruitCount) fruitValueComp += indiFruitValue / 5;
            }
            if (playerFruitCount > orderFruitCount) fruitValueComp -= playerFruitCount - orderFruitCount * indiFruitValue / 2;
            finalScore += (int)fruitValueComp;

        } // Fruit

        if(orderCake.icings.Count > 0)
        {
            // Note, currently only 1 flavor of icing will generate, hence the assumption of 1.

            Ing_FrostingDraw orderIcing = orderCake.icings[0];

            if(playerCake.icings.Count > 0)
            {
                Ing_FrostingDraw playerIcing = playerCake.icings.FirstOrDefault(check => Ingredient.Compare(check, orderIcing));
                if (playerIcing != null) finalScore += CompareIcing(orderIcing, playerIcing);
                else finalScore += CompareIcing(orderIcing, playerCake.icings[0]) / 2;
            }
        } // Icing

        int possibleScore = 
            comp.cakeValue + 
            comp.frostingValue + 
            (orderCake.fruits.Count > 0 ? comp.totalFruitValue : 0) + 
            (orderCake.icings.Count > 0 ? comp.maxIcingValue : 0);

        float ratio = (float)finalScore / (float)possibleScore;
            
        SoundFXManager.Get().PlaySound(ratio >= comp.minRatioForSound ? comp.goodSound : comp.badSound);

        return finalScore;
    }

    private int CompareIcing(Ing_FrostingDraw orderIcing, Ing_FrostingDraw playerIcing)
    {
        float[] orderValues = orderIcing.texture.GetPixels(1).Select(color => color.r).ToArray();
        float[] playerValues = playerIcing.texture.GetPixels(1).Select(color => color.r).ToArray();

        float similarity = 0;
        float total = 0;

        for (int i = 0; i < orderValues.Length; i++)
        {
            //similarity += (1 - Mathf.Abs(orderValues[i] - playerValues[i])) * orderValues[i] * 1.5f * 1.5f;
            similarity += playerValues[i] * orderValues[i] * 1.5f;
            similarity -= playerValues[i] * (1 - orderValues[i]) * 0.5f;
            total += orderValues[i];
        }

        similarity /= total;
        similarity = (similarity - comp.minIcingSimilarity) / (comp.maxIcingSimilarity - comp.minIcingSimilarity);
        similarity = Mathf.Clamp(similarity, 0, 1);

        return (int)(similarity * comp.maxIcingValue * (Ingredient.Compare(playerIcing, orderIcing) ? 1 : 0.5f));
    }



    /*
    #region Old Compare

        [SerializeField, Obsolete] private ComparisonData compareData = new();
    [System.Serializable, Obsolete]
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
        public int minIcingValue;
        public float minIcingSimilarity;
        public float maxIcingSimilarity;
        public int maxIcingValue;

        public int minValueForSound;

        public SoundFXManager.RandomizedAudio goodSound;
        public SoundFXManager.RandomizedAudio badSound;

        public AudioClip Sound(int score) => score > minValueForSound ? goodSound : badSound;
    }

    public int CompareOld(D_Cake orderCake, D_Cake playerCake)
    {
        if (playerCake.cakeBase is null) return compareData.noCakeNegative;
        int finalScore = 0;

        if (Ingredient.Compare(playerCake.cakeBase, orderCake.cakeBase)) finalScore += compareData.rightCake;
        else finalScore += compareData.wrongCake;

        if (playerCake.frostingCover is null) finalScore += 0;
        else if (Ingredient.Compare(playerCake.frostingCover, orderCake.frostingCover)) finalScore += compareData.rightFrost;
        else finalScore += compareData.wrongFrost;

        float range = 1.3f;

        var overlapResults = new Collider[5];
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

        foreach (Ing_FrostingDraw orderIcing in orderCake.icings)
        {
            Ing_FrostingDraw playerIcing = playerCake.icings.FirstOrDefault(check => Ingredient.Compare(check, orderIcing));
            if (playerIcing == null) break;
            //finalScore += compareData.minIcingValue;

            float[] orderValues = orderIcing.texture.GetPixels(1).Select(color => color.r).ToArray();
            float[] playerValues = playerIcing.texture.GetPixels(1).Select(color => color.r).ToArray();

            float similarity = 0;
            float total = 0;

            
            Color[] diffColors = new Color[orderValues.Length];

            for (int i = 0; i < orderValues.Length; i++)
            {
                diffColors[i] = Color.black;
                diffColors[i].r = orderValues[i]; 
                diffColors[i].b = playerValues[i];

            }

            diffTex = new Texture2D(16, 16);
            diffTex.SetPixels(diffColors); diffTex.Apply();
            


            for (int i = 0; i < orderValues.Length; i++)
            {
                //similarity += (1 - Mathf.Abs(orderValues[i] - playerValues[i])) * orderValues[i] * 1.5f * 1.5f;
                similarity += playerValues[i] * orderValues[i] * 1.5f;
                similarity -= playerValues[i] * (1 - orderValues[i]) * 0.5f;
                total += orderValues[i];
            }

            similarity /= total;
            similarity = (similarity - compareData.minIcingSimilarity) / (compareData.maxIcingSimilarity - compareData.minIcingSimilarity);
            similarity = Mathf.Clamp(similarity, 0, 1);

            finalScore += (int)(similarity * compareData.maxIcingValue);
        }


        return finalScore;
    }
    #endregion */
}
