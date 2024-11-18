using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EditorAttributes;
using UnityEngine;
using UnityEngine.EventSystems;


public class DesertBuilder : Singleton<DesertBuilder>
{
	#region Config
	public LayerMask stationLayerMask;
	public RuntimeAnimatorController ingredientPlaceController;
	public UnityEngine.UI.Button submitButton;
	public float submitCooldown;

	#endregion
	#region Components

	private D_Cake cake;
	private new AudioSource audio;

	#endregion
	#region Data

	Plane plane = new(Vector3.up, Vector3.zero);
	Vector3 cursorPosition;
	private int mode; // 0 = Normal, 1 = Multi Object Placement, 2 = Drawing
	private Ingredient activeModeIngredient;
	private IngredientButton activeModeIngredientButton;
    public new Camera camera;
	private float submitCooldownTimer;

    #endregion


    protected override void OnAwake()
	{
		BeginCake();
		Input.Tap.performed += _ => Tap();
		audio = GetComponent<AudioSource>();
	}

	private void BeginCake() => cake = Desert.Create<D_Cake>("New Cake", transform);

	public void SubmitDesert()
	{
		if (cake == null || submitCooldownTimer > 0) return;
		OrderManager.Get().SubmitDesert(cake);
		ClearCurrentDesert();
		submitCooldownTimer = submitCooldown;
		submitButton.interactable = false;
    }

	public void ClearCurrentDesert()
	{
		Destroy(cake.gameObject);
		BeginCake();
	}

	bool GetMousePosition(out Ray mouseRay)
	{
        Vector2 PositionScaled = Input.Position / new Vector2(Screen.width, Screen.height);
        if (PositionScaled.x > 1 || PositionScaled.x < 0 || PositionScaled.y > 1 || PositionScaled.y < 0)
		{
			mouseRay = new Ray();
            return false;
        }
        mouseRay = Camera.main.ViewportPointToRay(PositionScaled);
		if (Physics.Raycast(mouseRay, out RaycastHit hitInfo, 1000f, stationLayerMask))
		{
			cursorPosition = hitInfo.point;
			return true;
		}
		else
		{
			cursorPosition = Vector3.zero;
			return false;
		}
	}
	bool GetMousePosition() => GetMousePosition(out _);

	public void ClickIngredient(Ingredient ingredient, IngredientButton button)
	{
		if (ingredient is IStaticIngredient) AddIngredient(ingredient, Vector3.zero);
		else if (ingredient is IObjectIngredient) BeginActiveMode(ingredient, button);
		else if (ingredient is IPaintIngredient) BeginActiveMode(ingredient, button);
		else AddIngredient(ingredient, Vector3.zero);
	}
	public void DragIngredient(Ingredient ingredient, IngredientButton button)
	{
		if (!GetMousePosition()) return;

		if (ingredient is IStaticIngredient) AddIngredient(ingredient, Vector3.zero);
		else if (ingredient is IObjectIngredient) AddIngredient(ingredient, cursorPosition);
		else if (ingredient is IPaintIngredient) BeginActiveMode(ingredient, button);
		else AddIngredient(ingredient, Vector3.zero);
	}

	void BeginActiveMode(Ingredient ingredient, IngredientButton button)
	{
		activeModeIngredient = ingredient;
		activeModeIngredientButton = button;
		button.BeginActiveMode();
		mode = 1;

		if (ingredient is IPaintIngredient)
		{
			mode = 2;
			activeModeIngredient = cake.AddIngredient(ingredient, Vector3.zero);
			if (activeModeIngredient == null) EndActiveMode();
		}

	}
	void EndActiveMode()
	{
		if(mode == 2 && activeModeIngredient != null)
            if ((activeModeIngredient as Ing_FrostingDraw).texture.GetPixels(5)[0].r == 0)
			{
                cake.icings.Remove(activeModeIngredient as Ing_FrostingDraw);
				Destroy(activeModeIngredient.gameObject);
				activeModeIngredient = null;
            }
				

        mode = 0;
        activeModeIngredient = null;
        if (activeModeIngredientButton) activeModeIngredientButton.EndActiveMode();
        activeModeIngredientButton = null;
    }

	void Tap()
	{
		if (mode == 0) return;
		if (!GetMousePosition())
		{
			EndActiveMode();
			return;
		}

		if (mode == 1 && activeModeIngredient is IObjectIngredient)
			if (AddIngredient(activeModeIngredient, cursorPosition) == null)
				EndActiveMode();
	}

	private Ingredient AddIngredient(Ingredient ingredient, Vector3 position)
	{
		Ingredient NewIngredient = cake.AddIngredient(ingredient, position);
        if (NewIngredient)
		{
			if (NewIngredient.placeAnimation)
			{
                Animator anim = NewIngredient.gameObject.AddComponent<Animator>();
				anim.runtimeAnimatorController = ingredientPlaceController;
				(anim.runtimeAnimatorController as AnimatorOverrideController)["IngBaseAnim"] = NewIngredient.placeAnimation;
				anim.Play("Place");
                Destroy(anim, NewIngredient.placeAnimation.length + 0.01f);
            }
			audio.PlayOneShot(NewIngredient.placeAudio);
			return NewIngredient;
		}
		return null;
	}

    private void Update()
    {
		if(submitCooldownTimer > 0)
		{
			submitCooldownTimer -= Time.deltaTime;
			if(submitCooldownTimer <= 0)
			{
				submitButton.interactable = true;
			}
		}

        if (Input.Press.IsPressed() && mode == 2 && GetMousePosition(out Ray screenRay))
        {
            castHits = Physics.RaycastAll(screenRay, 1000f);
            RaycastHit correctHit = castHits.FirstOrDefault(check => check.transform.parent == activeModeIngredient.transform);
			if(correctHit.transform is not null) 
				(activeModeIngredient as Ing_FrostingDraw).DrawPixel(correctHit.textureCoord);
        }
    }

    private RaycastHit[] castHits;
}
