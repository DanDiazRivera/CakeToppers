using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript1 : MonoBehaviour
{
	public int V = 0;

	private void Awake()
	{
		//Input.Drag.Active += _ => V++;
		Input.Hold.performed += _ => { V++; };
	}
}
