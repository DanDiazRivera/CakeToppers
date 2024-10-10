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
		int score = currentOrder.Compare(input);
		LevelManager.Get().AddScore(score);
		Destroy(currentOrder.gameObject);
		RandomizeDesert();
	}

}
