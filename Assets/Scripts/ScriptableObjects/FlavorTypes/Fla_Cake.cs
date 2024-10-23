using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCakeFlavor", menuName = "Flavor/Cake")]
public class Fla_Cake : IngredientFlavor
{
	[ColorUsage(false)]
	public Color color;
}
