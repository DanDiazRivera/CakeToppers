using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientButton : MonoBehaviour
{
	public Ingredient ingredient;

	private Button button;

	private void Awake()
	{
		TryGetComponent(out button);


	}


}
