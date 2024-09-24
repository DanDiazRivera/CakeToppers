using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class IngredientButtonDraggable : EventTrigger
{
	[SerializeField] private UnityEvent<Vector2> output;

	public override void OnEndDrag(PointerEventData eventData)
	{
		base.OnEndDrag(eventData);
		output.Invoke(eventData.position);
	}
}
