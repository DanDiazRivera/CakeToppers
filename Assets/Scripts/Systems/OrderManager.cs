using System.Collections;
using UnityEngine;


public class OrderManager : Singleton<OrderManager>
{

	private D_Cake currentOrder;

	[SerializeField] private RandomizerData randoData;
	[System.Serializable] private struct RandomizerData
	{
		public Ing_CakeBase cakeBasePrefab;
		public Ing_Frosting_Cover frostingPrefab;
		public Ing_Fruit strawberryPrefab;

		public Fla_Cake[] cakeFlavors;
		public Fla_Frosting[] frostingFlavors;
	}


	protected override void OnAwake() => RandomizeDesert();


	public void RandomizeDesert()
	{
		if (currentOrder != null)
		{
			Destroy(currentOrder.gameObject);
			currentOrder = null;
		}
		currentOrder = Desert.Create<D_Cake>(transform);


		currentOrder.cakeBase = Instantiate(randoData.cakeBasePrefab, currentOrder.transform);
		currentOrder.cakeBase.gameObject.SetActive(true);
		int flavor = Random.Range(0, randoData.cakeFlavors.Length);
		currentOrder.cakeBase.flavor = randoData.cakeFlavors[flavor];
		currentOrder.cakeBase.UpdateFlavor();

		currentOrder.frostingCover = Instantiate(randoData.frostingPrefab, currentOrder.transform);
		currentOrder.frostingCover.gameObject.SetActive(true);
		flavor = Random.Range(0, randoData.frostingFlavors.Length);
		currentOrder.frostingCover.flavor = randoData.frostingFlavors[flavor];
		currentOrder.frostingCover.UpdateFlavor();

		currentOrder.fruits.Add(Instantiate(randoData.strawberryPrefab, currentOrder.transform));
		Ing_Fruit fruit = currentOrder.fruits[^1];
		fruit.gameObject.SetActive(true);

		float randoX = Random.Range(-0.4f, 0.4f);
		float randoZ = Random.Range(-0.4f, 0.4f);
		float height = 0.15f;
		//Test in game and figure out correct range.
		fruit.transform.localPosition = new Vector3(randoX, height, randoZ);

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
		if (playerCake.cakeBase == null) return 0;
		int finalScore = 0;

		if (playerCake.cakeBase.flavor == orderCake.cakeBase.flavor) finalScore += 50;
		else finalScore += 5;

		if (playerCake.frostingCover == null) finalScore += 0;
		else if (playerCake.frostingCover.flavor == orderCake.frostingCover.flavor) finalScore += 50;
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
