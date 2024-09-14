using System.Collections;
using System.Collections.Generic;
using EditorAttributes;
using UnityEngine;
using UnityEngine.EventSystems;

public class DesertBuilder : MonoBehaviour
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

	private void Awake()
	{
		BeginCake();
	}

	private void BeginCake()
	{
		cake = new GameObject("Cake", typeof(D_Cake)).GetComponent<D_Cake>();
		cake.transform.parent = transform;
		cake.transform.localPosition = Vector3.zero;
		cake.transform.localRotation = Quaternion.identity;
		cake.transform.localScale = Vector3.one * 4;
		phase = 0;
	}

	public void InputIngredient(Ingredient ing) => InputIngredient(ing, null); 
	public void InputIngredient(Ingredient ing, Vector3? pos)
	{
		if(phase < 2 && ing.GetType() == typeof(Ing_CakeBase))
		{
			if (cake.cakeBase != null) Destroy(cake.cakeBase.gameObject);
			cake.cakeBase = (Ing_CakeBase)Instantiate(ing, cake.transform);
			cake.cakeBase.gameObject.SetActive(true);
			phase = 1;
		}
		else if(phase < 3 && ing.GetType() == typeof(Ing_Frosting_Cover))
		{
			if (cake.frostingCover != null) Destroy(cake.frostingCover.gameObject);
			cake.frostingCover = (Ing_Frosting_Cover)Instantiate(ing, cake.transform);
			cake.frostingCover.gameObject.SetActive(true);
			phase = 2;
		}
		else if(phase > 1 && ing.GetType() == typeof(Ing_Fruit))
		{
			cake.fruits.Add((Ing_Fruit)Instantiate(ing, cake.transform)); 
			cake.fruits[^1].transform.position = pos.Value;
			cake.fruits[^1].gameObject.SetActive(true);
			phase = 3;
		}
	}

	public void AddStrawberry()
	{
		Ray raymond = Camera.main.ScreenPointToRay(Input.Position);
		
		if(Physics.Raycast(raymond, out RaycastHit hitInfo))
		{
			InputIngredient(strawberryPrefab, hitInfo.point);
		}
		
		 
	}

	public void SubmitDesert()
	{
		OrderManager.Get().SubmitDesert();
		ClearCurrentDesert();
	}

	public void ClearCurrentDesert()
	{
		Destroy(cake.gameObject);
		BeginCake();
	}
















}
