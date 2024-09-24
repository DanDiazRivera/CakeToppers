using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewFrostingFlavor", menuName = "Flavor/Frosting")]
public class Fla_Frosting : IngredientFlavor
{
	[ColorUsage(false)]
	public Color color;
}
