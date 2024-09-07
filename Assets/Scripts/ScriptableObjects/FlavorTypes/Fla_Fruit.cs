using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewFruit", menuName ="Flavor/Fruit")]
public class Fla_Fruit : IngredientFlavor
{
	[ColorUsage(false)]
	public Color color;
	public GameObject prefab;
}
