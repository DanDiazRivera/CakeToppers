//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Scripts/Systems/Input/InputActions.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @InputActions: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputActions"",
    ""maps"": [
        {
            ""name"": ""Controls"",
            ""id"": ""fc6c685c-9088-4fd9-9f55-ca1f63f0ea2a"",
            ""actions"": [
                {
                    ""name"": ""Position"",
                    ""type"": ""Value"",
                    ""id"": ""646ad4fc-c022-4836-b49d-073b42afb7a9"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Delta"",
                    ""type"": ""Value"",
                    ""id"": ""08e89ce9-f8a4-4b41-ba3f-41a9ca9335d4"",
                    ""expectedControlType"": ""Delta"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""StartPosition"",
                    ""type"": ""Value"",
                    ""id"": ""b8592b50-4221-4847-bedb-27a9671fd7b2"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Press"",
                    ""type"": ""Button"",
                    ""id"": ""4fc60666-687a-49e7-a5c6-b66646697567"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Tap"",
                    ""type"": ""Button"",
                    ""id"": ""2786b954-c88c-4206-a182-28ea7c718768"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Hold"",
                    ""type"": ""Value"",
                    ""id"": ""66e522bd-b119-4f50-ba5f-9e612df5b6b5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold"",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""DeepTap"",
                    ""type"": ""Button"",
                    ""id"": ""74efce43-0c37-43c6-9cbd-f5baf6e15a39"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""TapCount"",
                    ""type"": ""Value"",
                    ""id"": ""ecffe3a3-ce15-42de-b5f2-41375f7c6b6e"",
                    ""expectedControlType"": ""Integer"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""PressStartTime"",
                    ""type"": ""Value"",
                    ""id"": ""4bbe6cd4-0adf-4219-b964-1ade55c4b2d3"",
                    ""expectedControlType"": ""Double"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""TouchData"",
                    ""type"": ""Value"",
                    ""id"": ""a1365831-8849-4c0d-8cb2-a63850821f47"",
                    ""expectedControlType"": ""Touch"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""d2b98084-88a1-417b-b630-d80cf3415cc2"",
                    ""path"": ""<Touchscreen>/primaryTouch/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Position"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8f087d4d-59ac-4189-a11e-c5d7e995c59a"",
                    ""path"": ""<Touchscreen>/primaryTouch/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Delta"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f73985b7-f7a6-4922-992c-74e13e056143"",
                    ""path"": ""<Touchscreen>/Press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Press"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8fcc267b-264d-4a68-b6c8-92179513d6b6"",
                    ""path"": ""<Touchscreen>/primaryTouch/tap"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Tap"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d1bbd32b-dfe9-4a3c-932c-927e6762a865"",
                    ""path"": ""<Touchscreen>/Press"",
                    ""interactions"": ""SlowTap"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DeepTap"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7e17f1a3-85a7-4b2d-820f-1de5171c535a"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DeepTap"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0d38a2b1-1259-458a-8c5b-b97e95eedfdd"",
                    ""path"": ""<Touchscreen>/primaryTouch/startPosition"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""StartPosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7bd50da0-2412-4e13-bdc7-ee0803bc58bc"",
                    ""path"": ""<Touchscreen>/primaryTouch/tapCount"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TapCount"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""63ac77dd-c74d-4b26-a1df-61eebbb01d2c"",
                    ""path"": ""<Touchscreen>/primaryTouch/startTime"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PressStartTime"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b7e914a9-5590-4a75-bcee-1ce5813025f6"",
                    ""path"": ""<Touchscreen>/primaryTouch"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TouchData"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""42072774-afc1-46ac-8b3c-f50ac4b419a5"",
                    ""path"": ""<Touchscreen>/Press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Hold"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Controls
        m_Controls = asset.FindActionMap("Controls", throwIfNotFound: true);
        m_Controls_Position = m_Controls.FindAction("Position", throwIfNotFound: true);
        m_Controls_Delta = m_Controls.FindAction("Delta", throwIfNotFound: true);
        m_Controls_StartPosition = m_Controls.FindAction("StartPosition", throwIfNotFound: true);
        m_Controls_Press = m_Controls.FindAction("Press", throwIfNotFound: true);
        m_Controls_Tap = m_Controls.FindAction("Tap", throwIfNotFound: true);
        m_Controls_Hold = m_Controls.FindAction("Hold", throwIfNotFound: true);
        m_Controls_DeepTap = m_Controls.FindAction("DeepTap", throwIfNotFound: true);
        m_Controls_TapCount = m_Controls.FindAction("TapCount", throwIfNotFound: true);
        m_Controls_PressStartTime = m_Controls.FindAction("PressStartTime", throwIfNotFound: true);
        m_Controls_TouchData = m_Controls.FindAction("TouchData", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Controls
    private readonly InputActionMap m_Controls;
    private List<IControlsActions> m_ControlsActionsCallbackInterfaces = new List<IControlsActions>();
    private readonly InputAction m_Controls_Position;
    private readonly InputAction m_Controls_Delta;
    private readonly InputAction m_Controls_StartPosition;
    private readonly InputAction m_Controls_Press;
    private readonly InputAction m_Controls_Tap;
    private readonly InputAction m_Controls_Hold;
    private readonly InputAction m_Controls_DeepTap;
    private readonly InputAction m_Controls_TapCount;
    private readonly InputAction m_Controls_PressStartTime;
    private readonly InputAction m_Controls_TouchData;
    public struct ControlsActions
    {
        private @InputActions m_Wrapper;
        public ControlsActions(@InputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Position => m_Wrapper.m_Controls_Position;
        public InputAction @Delta => m_Wrapper.m_Controls_Delta;
        public InputAction @StartPosition => m_Wrapper.m_Controls_StartPosition;
        public InputAction @Press => m_Wrapper.m_Controls_Press;
        public InputAction @Tap => m_Wrapper.m_Controls_Tap;
        public InputAction @Hold => m_Wrapper.m_Controls_Hold;
        public InputAction @DeepTap => m_Wrapper.m_Controls_DeepTap;
        public InputAction @TapCount => m_Wrapper.m_Controls_TapCount;
        public InputAction @PressStartTime => m_Wrapper.m_Controls_PressStartTime;
        public InputAction @TouchData => m_Wrapper.m_Controls_TouchData;
        public InputActionMap Get() { return m_Wrapper.m_Controls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ControlsActions set) { return set.Get(); }
        public void AddCallbacks(IControlsActions instance)
        {
            if (instance == null || m_Wrapper.m_ControlsActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_ControlsActionsCallbackInterfaces.Add(instance);
            @Position.started += instance.OnPosition;
            @Position.performed += instance.OnPosition;
            @Position.canceled += instance.OnPosition;
            @Delta.started += instance.OnDelta;
            @Delta.performed += instance.OnDelta;
            @Delta.canceled += instance.OnDelta;
            @StartPosition.started += instance.OnStartPosition;
            @StartPosition.performed += instance.OnStartPosition;
            @StartPosition.canceled += instance.OnStartPosition;
            @Press.started += instance.OnPress;
            @Press.performed += instance.OnPress;
            @Press.canceled += instance.OnPress;
            @Tap.started += instance.OnTap;
            @Tap.performed += instance.OnTap;
            @Tap.canceled += instance.OnTap;
            @Hold.started += instance.OnHold;
            @Hold.performed += instance.OnHold;
            @Hold.canceled += instance.OnHold;
            @DeepTap.started += instance.OnDeepTap;
            @DeepTap.performed += instance.OnDeepTap;
            @DeepTap.canceled += instance.OnDeepTap;
            @TapCount.started += instance.OnTapCount;
            @TapCount.performed += instance.OnTapCount;
            @TapCount.canceled += instance.OnTapCount;
            @PressStartTime.started += instance.OnPressStartTime;
            @PressStartTime.performed += instance.OnPressStartTime;
            @PressStartTime.canceled += instance.OnPressStartTime;
            @TouchData.started += instance.OnTouchData;
            @TouchData.performed += instance.OnTouchData;
            @TouchData.canceled += instance.OnTouchData;
        }

        private void UnregisterCallbacks(IControlsActions instance)
        {
            @Position.started -= instance.OnPosition;
            @Position.performed -= instance.OnPosition;
            @Position.canceled -= instance.OnPosition;
            @Delta.started -= instance.OnDelta;
            @Delta.performed -= instance.OnDelta;
            @Delta.canceled -= instance.OnDelta;
            @StartPosition.started -= instance.OnStartPosition;
            @StartPosition.performed -= instance.OnStartPosition;
            @StartPosition.canceled -= instance.OnStartPosition;
            @Press.started -= instance.OnPress;
            @Press.performed -= instance.OnPress;
            @Press.canceled -= instance.OnPress;
            @Tap.started -= instance.OnTap;
            @Tap.performed -= instance.OnTap;
            @Tap.canceled -= instance.OnTap;
            @Hold.started -= instance.OnHold;
            @Hold.performed -= instance.OnHold;
            @Hold.canceled -= instance.OnHold;
            @DeepTap.started -= instance.OnDeepTap;
            @DeepTap.performed -= instance.OnDeepTap;
            @DeepTap.canceled -= instance.OnDeepTap;
            @TapCount.started -= instance.OnTapCount;
            @TapCount.performed -= instance.OnTapCount;
            @TapCount.canceled -= instance.OnTapCount;
            @PressStartTime.started -= instance.OnPressStartTime;
            @PressStartTime.performed -= instance.OnPressStartTime;
            @PressStartTime.canceled -= instance.OnPressStartTime;
            @TouchData.started -= instance.OnTouchData;
            @TouchData.performed -= instance.OnTouchData;
            @TouchData.canceled -= instance.OnTouchData;
        }

        public void RemoveCallbacks(IControlsActions instance)
        {
            if (m_Wrapper.m_ControlsActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IControlsActions instance)
        {
            foreach (var item in m_Wrapper.m_ControlsActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_ControlsActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public ControlsActions @Controls => new ControlsActions(this);
    public interface IControlsActions
    {
        void OnPosition(InputAction.CallbackContext context);
        void OnDelta(InputAction.CallbackContext context);
        void OnStartPosition(InputAction.CallbackContext context);
        void OnPress(InputAction.CallbackContext context);
        void OnTap(InputAction.CallbackContext context);
        void OnHold(InputAction.CallbackContext context);
        void OnDeepTap(InputAction.CallbackContext context);
        void OnTapCount(InputAction.CallbackContext context);
        void OnPressStartTime(InputAction.CallbackContext context);
        void OnTouchData(InputAction.CallbackContext context);
    }
}
