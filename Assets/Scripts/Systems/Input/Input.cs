using System;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Button = UnityEngine.InputSystem.InputAction;
using Vector2 = UnityEngine.Vector2;

public class Input : Singleton<Input>
{
	
	public static new Input Get() => InitCreate(true);
	public static new bool TryGet(out Input output)
	{
		output = Get();
		return output != null;
	}
	

	public InputActions asset;

	public static Vector2 Position => Get().asset.Controls.Position.ReadValue<Vector2>();
	public static Button Press => Get().asset.Controls.Press;
	public static Button Tap => Get().asset.Controls.Tap;
	public static Button DeepTap => Get().asset.Controls.DeepTap;
	public static int TapCount => Get().asset.Controls.TapCount.ReadValue<int>();
	public static float PressStartTime => Get().asset.Controls.PressStartTime.ReadValue<float>();
	public static Touch TouchData => Get().asset.Controls.TouchData.ReadValue<Touch>();
	public static Button Hold => Get().asset.Controls.Hold;
	private DragControl _Drag;
	public static DragControl Drag => Get()._Drag;

	private bool holding;

	public static bool IsPressed => Get().asset.Controls.Press.IsPressed();
	public static bool WasPressedThisFrame => Get().asset.Controls.Press.WasPressedThisFrame();
	public static bool WasReleasedThisFrame => Get().asset.Controls.Press.WasReleasedThisFrame();
	public static bool WasTappedThisFrame => Get().asset.Controls.Tap.WasPerformedThisFrame();
	public static bool WasDeepTappedThisFrame => Get().asset.Controls.DeepTap.WasPerformedThisFrame();
	public static bool IsHeld => Get().asset.Controls.Hold.IsPressed();
	public static bool IsDragged => Get()._Drag.Dragging;


	protected override void OnAwake()
	{
		asset = new();
		asset.Enable();
		TouchSimulation.Enable();
		CheckForDebugTouchSim();

		DragInit();
	}

	private void DragInit()
	{
		_Drag = new();
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
				_Drag.Ended?.Invoke(_Drag);
			}
		};
	}

	[Serializable]
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
				_Drag.Active?.Invoke(_Drag);
			}
			else if (_Drag.Distance > DragControl.minDifference)
			{
				_Drag.Dragging = true;
				_Drag.Started?.Invoke(_Drag);
			}
		}
	}
	 
	void CheckForDebugTouchSim()
	{
#if UNITY_EDITOR
		UnityEngine.Debug.Log("Make sure Debug Simulated Touchscreen is active. Go to Window/Analysis/Input Debugger, click on Options, and click \"Simulate Touch Input From Mouse or Pen\"");
#endif
	}
}
