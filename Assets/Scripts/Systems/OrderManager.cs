using System.Collections;
using UnityEngine;


public class OrderManager : Singleton<OrderManager>
{

	protected override void OnAwake()
	{

	}

	public void SubmitDesert()
	{
		LevelManager.Get().AddScore(100);
	}
}
