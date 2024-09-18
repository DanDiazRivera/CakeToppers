using System.Collections;
using System.Collections.Generic;
using EditorAttributes;
using UnityEngine;
using UnityEngine.EventSystems;

public class DesertBuilder : Singleton<DesertBuilder>
{
	#region Config
	//public Ing_CakeBase cakeBasePrefab;
	//public Ing_Frosting_Cover frostingPrefab;
	//public Ing_Fruit strawberryPrefab;
	public LayerMask stationLayerMask;

	#endregion
	#region Components

	//private Desert desert;
	private D_Cake cake;

	#endregion
	#region Data

	Plane plane = new (Vector3.up, Vector3.zero);
	Vector3 cursorPosition;
	private int mode; // 0 = Normal, 1 = Multi Object Placement, 2 = Drawing
	private Ingredient activeModeIngredient;
	private IngredientButton activeModeIngredientButton;

	#endregion

	protected override void OnAwake()
	{
		BeginCake();
		Input.Tap.performed += _ => Tap();
	}

	private void BeginCake()
	{
		cake = Desert.Create<D_Cake>("New Cake", transform);
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

	bool GetMousePosition()
	{
		Ray mouseRay = Camera.main.ScreenPointToRay(Input.Position);
		if (Physics.Raycast(mouseRay, out RaycastHit hitInfo, 1000f, stationLayerMask))
		{
			cursorPosition = hitInfo.point;
			return true;
		}
		else
		{
			cursorPosition = Vector3.zero;
			return false;
		}
	}

	public void ClickIngredient(Ingredient ingredient, IngredientButton button)
	{
		if(ingredient is IStaticIngredient) cake.AddIngredient(ingredient, Vector3.zero);
		else if (ingredient is IObjectIngredient) BeginActiveMode(ingredient, button);
		else if (ingredient is IPaintIngredient) BeginActiveMode(ingredient, button);
		else cake.AddIngredient(ingredient, Vector3.zero);
	}
	public void DragIngredient(Ingredient ingredient, IngredientButton button)
	{
		if (!GetMousePosition()) return;

		if (ingredient is IStaticIngredient) cake.AddIngredient(ingredient, Vector3.zero);
		else if (ingredient is IObjectIngredient) cake.AddIngredient(ingredient, cursorPosition);
		else if (ingredient is IPaintIngredient) BeginActiveMode(ingredient, button);
		else cake.AddIngredient(ingredient, Vector3.zero);
	}

	void BeginActiveMode(Ingredient ingredient, IngredientButton button)
	{
		activeModeIngredient = ingredient;
		activeModeIngredientButton = button;
		button.BeginActiveMode();
		mode = 1;

		if (ingredient is IPaintIngredient)
		{
			mode = 2;
			activeModeIngredient = cake.AddIngredient(ingredient, Vector3.zero);
			if (activeModeIngredient == null) EndActiveMode();

			//////////////////////////////////////////PUT FUNCTIONALITY FOR PAINT INGREDIENTS HERE.//////////////////////////////////////////
		}

	}
	void EndActiveMode()
	{
		activeModeIngredient = null;
		if(activeModeIngredientButton)activeModeIngredientButton.EndActiveMode();
		activeModeIngredientButton = null;

	}



	void Tap()
	{
		if (mode == 0) return;
		if (!GetMousePosition())
		{
			EndActiveMode();
			return;
		}

		if (mode == 1 && activeModeIngredient is IObjectIngredient)
			if (cake.AddIngredient(activeModeIngredient, cursorPosition) == null) 
				EndActiveMode();
	}








}
