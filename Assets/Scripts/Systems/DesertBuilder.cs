using System.Collections;
using System.Collections.Generic;
using EditorAttributes;
using UnityEngine;
using UnityEngine.EventSystems;

public class DesertBuilder : Singleton<DesertBuilder>
{
	#region Config
	public Ing_CakeBase cakeBasePrefab;
	public Ing_Frosting_Cover frostingPrefab;
	public Ing_Fruit strawberryPrefab;

	#endregion
	#region Components

	//private Desert desert;
	private D_Cake cake;

	#endregion
	#region Data

	private int phase = 0;

	#endregion

	protected override void OnAwake()
	{
		BeginCake();
	}

	private void BeginCake()
	{
		cake = Desert.Create<D_Cake>(transform);
		phase = 0;
	}

	public void SubmitDesert()
	{
		if (cake == null) return;
		OrderManager.Get().SubmitDesert(cake);
		ClearCurrentDesert();
	}

	public void ClearCurrentDesert()
	{
		Destroy(cake.gameObject);
		BeginCake();
	}


	public void ClickIngredient(Ingredient ingredient)
	{
		cake.AddIngredient(ingredient, Vector3.zero);
	}
	public void DragIngredient(Ingredient ingredient, Vector2 pos)
	{
		Ray raymond = Camera.main.ScreenPointToRay(Input.Position);

		if (Physics.Raycast(raymond, out RaycastHit hitInfo))
			cake.AddIngredient(ingredient, hitInfo.point);
		
	}












}
