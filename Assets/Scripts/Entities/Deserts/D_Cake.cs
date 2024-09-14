using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_Cake : Desert
{
	public Ing_CakeBase cakeBase;
	public Ing_Frosting_Cover frostingCover;
	public List<Ing_Fruit> fruits;

	private void Awake()
	{
		fruits = new List<Ing_Fruit>();
	}
}
