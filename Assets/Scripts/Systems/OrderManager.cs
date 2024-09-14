using System.Collections;
using UnityEngine;
using static UnityEditor.PlayerSettings;


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
		currentOrder = D_Cake.Create(transform);


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


	public void SubmitDesert()
	{
		LevelManager.Get().AddScore(100);
		Destroy(currentOrder.gameObject);
		RandomizeDesert();
	}

	protected override void OnDestroyed()
	{

	}

}
