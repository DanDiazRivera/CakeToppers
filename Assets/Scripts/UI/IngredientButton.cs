using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class IngredientButton : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{

	public Ingredient ingredient;

	public Image highlight;
	//public bool onlyDrag;

	Input input;
	ScrollRect scrollRect;

	const float holdThreshold = 0.8f;
	int holdState;
	float holdTimer;
	bool dragging;
	[HideInInspector] public bool Draggable = true;

	private void Awake()
	{
		Input.TryGet(out input);
		scrollRect = FindFirstObjectByType<IngredientTabManager>().GetComponent<ScrollRect>();
	}

	private void Update()
	{
		if(holdState == 1)
		{
			holdTimer += Time.deltaTime;
			if (holdTimer >= holdThreshold)
			{
				holdState = 2;
				holdTimer = 0;
				BeginIngredientDrag();
			}
		}
	}

	public void OnPointerClick(PointerEventData eventData) => IngredientClick();

	public void OnPointerDown(PointerEventData eventData)
	{
		holdState = 1;
	}
	
	public void OnPointerUp(PointerEventData eventData)
	{
		holdTimer = 0;
		holdState = 0;
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		if (CanScroll(eventData))
		{
			ExecuteEvents.Execute(scrollRect.gameObject, eventData, ExecuteEvents.beginDragHandler);
			holdState = 0;
		}
		else BeginIngredientDrag();
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (CanScroll(eventData))
		{
			ExecuteEvents.Execute(scrollRect.gameObject, eventData, ExecuteEvents.dragHandler);
			holdState = 0;
		}
			
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		EndIngredientDrag(eventData);
	}
	

	private bool CanScroll(PointerEventData eventData)
	{
		Vector2 distance = (eventData.position - eventData.pressPosition);
		distance = new(Mathf.Abs(distance.x), Mathf.Abs(distance.y));
		return distance.y - distance.x > distance.y / 3 && holdState != 2;
	}

	private void IngredientClick()
	{
		//if (onlyDrag) return;
		DesertBuilder.Get().ClickIngredient(ingredient, this);
	}

	private void BeginIngredientDrag()
	{
		dragging = true;

	}

	private void EndIngredientDrag(PointerEventData eventData)
	{
		if (!dragging)
		{
			ExecuteEvents.Execute(scrollRect.gameObject, eventData, ExecuteEvents.endDragHandler);
			return;
		}
		dragging = false;
		DesertBuilder.Get().DragIngredient(ingredient, this);
	}


    public void BeginActiveMode() => highlight.enabled = true;
    public void EndActiveMode() => highlight.enabled = false;
}
