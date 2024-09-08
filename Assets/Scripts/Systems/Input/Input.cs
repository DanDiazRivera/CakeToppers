using System;
using UnityEngine.InputSystem.EnhancedTouch;
using Vector2 = UnityEngine.Vector2;
using Button = UnityEngine.InputSystem.InputAction;

public class Input : Staticon<Input>
{

	public InputActions asset;

	public static Vector2 Position = Get().asset.Controls.Position.ReadValue<Vector2>();
	public static Button Press = Get().asset.Controls.Press;
	public static Button Tap = Get().asset.Controls.Tap;
	public static Button DeepTap = Get().asset.Controls.DeepTap;
	public static int TapCount = Get().asset.Controls.TapCount.ReadValue<int>();
	public static float PressStartTime = Get().asset.Controls.PressStartTime.ReadValue<float>();
	public static Touch TouchData = Get().asset.Controls.TouchData.ReadValue<Touch>();
	public static Button Hold = Get().asset.Controls.Hold;
	private DragControl _Drag = new();
	public static DragControl Drag = Get()._Drag;

	private bool holding;

	public static bool IsPressed = Get().asset.Controls.Press.IsPressed();
	public static bool WasPressedThisFrame = Get().asset.Controls.Press.WasPressedThisFrame();
	public static bool WasReleasedThisFrame = Get().asset.Controls.Press.WasReleasedThisFrame();
	public static bool WasTappedThisFrame = Get().asset.Controls.Tap.WasPerformedThisFrame();
	public static bool WasDeepTappedThisFrame = Get().asset.Controls.DeepTap.WasPerformedThisFrame();
	public static bool IsHeld = Get().asset.Controls.Hold.IsPressed();
	public static bool IsDragged = Drag.Dragging;


	public override void Awake()
	{
		asset = new();
		asset.Enable();
		TouchSimulation.Enable();

		DragInit();
	}

	private void DragInit()
	{
		asset.Controls.Hold.performed += _ =>
		{
			_Drag.StartPosition = asset.Controls.StartPosition.ReadValue<Vector2>();
			holding = true;
		};
		asset.Controls.Press.canceled += _ =>
		{
			holding = false;
			if (_Drag.Dragging)
			{
				_Drag.Dragging = false;
				_Drag.Ended(_Drag);
			}
		};
	}

	public class DragControl
	{
		public bool Dragging;
		public Vector2 StartPosition;
		public Vector2 LastPosition;
		public const float minDifference = 5f;
		public Vector2 FrameDelta = Get().asset.Controls.Delta.ReadValue<Vector2>();
		public Vector2 Delta => StartPosition - LastPosition;
		public float Distance => (StartPosition - LastPosition).magnitude;

		public Action<DragControl> Started;
		public Action<DragControl> Active;
		public Action<DragControl> Ended;

	}

	//NOTE: Unity Function, refactor Input class into normal singleton later to make work.
	private void Update()
	{
		
		if (holding)
		{
			_Drag.LastPosition = asset.Controls.Position.ReadValue<Vector2>();
			if (_Drag.Dragging)
			{
				_Drag.Active(_Drag);
			}
			else if (_Drag.Distance > DragControl.minDifference)
			{
				_Drag.Dragging = true;
				_Drag.Started(_Drag);
			}
		}
	}

}
