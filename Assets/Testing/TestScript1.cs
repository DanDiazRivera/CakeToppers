using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript1 : MonoBehaviour
{
	public int V1 = 0;
	public int V2 = 0;

	private void Awake()
	{
		Input.Drag.Active += _ => V1++;
	}

	private void Update()
	{
		if (Input.IsDragged) V2++;
	}
}
