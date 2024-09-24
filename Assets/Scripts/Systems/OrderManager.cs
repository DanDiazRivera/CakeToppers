using System.Collections;
using UnityEngine;


public class OrderManager : Singleton<OrderManager>
{

	private D_Cake currentOrder;

	[SerializeField] private RandomizerData randoData;
	[System.Serializable] private struct RandomizerData
	{
		public Ing_CakeBase[] cakeBaseOptions;
		public Ing_Frosting_Cover[] frostingCoverOptions;
		public Ing_Fruit[] fruitOptions;
	}


	protected override void OnAwake() => RandomizeDesert();


	public void RandomizeDesert()
	{
		if (currentOrder != null)
		{
			Destroy(currentOrder.gameObject);
			currentOrder = null;
		}
		D_Cake result = D_Cake.CreateRandom(transform, randoData.cakeBaseOptions, randoData.frostingCoverOptions, randoData.fruitOptions);
		currentOrder = result;
		currentOrder.transform.localScale = Vector3.one;

	}


	public void SubmitDesert(D_Cake input)
	{
		int score = CompareCakes(input, currentOrder);
		LevelManager.Get().AddScore(score);
		Destroy(currentOrder.gameObject);
		RandomizeDesert();
	}

	private int CompareCakes(D_Cake playerCake, D_Cake orderCake)
	{
		if (playerCake.cakeBase is null) return 0;
		int finalScore = 0;

		if (Ingredient.Compare(playerCake.cakeBase, orderCake.cakeBase)) finalScore += 50;
		else finalScore += 5;

		if (playerCake.frostingCover is null) finalScore += 0;
		else if (Ingredient.Compare(playerCake.frostingCover, orderCake.frostingCover)) finalScore += 50;
		else finalScore += 10;

		if (playerCake.fruits.Count < 1) finalScore += 0;
		else
		{
			finalScore += 10;

			Vector2 inputStrawbPos = new (playerCake.fruits[0].transform.localPosition.x, playerCake.fruits[0].transform.localPosition.z);
			Vector2 orderStrawbPos = new(orderCake.fruits[0].transform.localPosition.x, orderCake.fruits[0].transform.localPosition.z);

			float distance = Vector2.Distance(inputStrawbPos, orderStrawbPos);

			finalScore += distance switch
			{
				< 0.2192031f => 50,
				< 0.4384062f => 25,
				_ => 0
			};

			finalScore += playerCake.fruits.Count - 1;
		}
			
		return finalScore;
	}
}
