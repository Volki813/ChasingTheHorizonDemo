//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/Scripts/CutsceneControls.inputactions
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

public partial class @CutsceneControls : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @CutsceneControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""CutsceneControls"",
    ""maps"": [
        {
            ""name"": ""Cutscenes"",
            ""id"": ""d12a22a3-984f-4872-a5ab-567a37edeef9"",
            ""actions"": [
                {
                    ""name"": ""Next"",
                    ""type"": ""Button"",
                    ""id"": ""bb6a4c1a-4e50-4e91-aa15-8053342c39cc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Skip"",
                    ""type"": ""Button"",
                    ""id"": ""b5c8cbda-ade5-403f-93bb-dc9e2932e546"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press"",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""e636cfbc-bfda-44f3-a139-ea8c6e64bb8b"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Next"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7291a270-5cb5-4421-9833-baca7c08085c"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Next"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c60b45bd-f951-4d07-92a5-9f331622ba7c"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Skip"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c38b63f6-0ea1-423a-87ed-db30faf0414c"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Skip"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Maps"",
            ""id"": ""aae58a19-c0e0-4c81-a88e-90cf7bbd05d7"",
            ""actions"": [
                {
                    ""name"": ""FastMode"",
                    ""type"": ""Button"",
                    ""id"": ""8c43afe6-31ca-491a-8c7e-f14fd9ae95ef"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SDialogueNext"",
                    ""type"": ""Button"",
                    ""id"": ""5105823c-ece9-49e3-b3b2-4bc71eb034ab"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""BDialogueNext"",
                    ""type"": ""Button"",
                    ""id"": ""ad8839ea-4a9b-4211-96e7-1b2a48de4550"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press"",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""b633586d-4f38-4de3-afaa-33b4d0f1a555"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""FastMode"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e9db871a-b19b-405e-8891-7e9268e92211"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""FastMode"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""26fd3c41-077b-48f8-9c41-8675d93a7e3b"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""SDialogueNext"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b8f11a10-ee5f-44b7-81a9-e3f70ad90f8d"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""SDialogueNext"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""954853b2-3a61-4b6b-847f-c0af6a2025b8"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""BDialogueNext"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""401ea3d7-2173-469c-ae0f-8357b3e5fc3b"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""BDialogueNext"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Cutscenes
        m_Cutscenes = asset.FindActionMap("Cutscenes", throwIfNotFound: true);
        m_Cutscenes_Next = m_Cutscenes.FindAction("Next", throwIfNotFound: true);
        m_Cutscenes_Skip = m_Cutscenes.FindAction("Skip", throwIfNotFound: true);
        // Maps
        m_Maps = asset.FindActionMap("Maps", throwIfNotFound: true);
        m_Maps_FastMode = m_Maps.FindAction("FastMode", throwIfNotFound: true);
        m_Maps_SDialogueNext = m_Maps.FindAction("SDialogueNext", throwIfNotFound: true);
        m_Maps_BDialogueNext = m_Maps.FindAction("BDialogueNext", throwIfNotFound: true);
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

    // Cutscenes
    private readonly InputActionMap m_Cutscenes;
    private ICutscenesActions m_CutscenesActionsCallbackInterface;
    private readonly InputAction m_Cutscenes_Next;
    private readonly InputAction m_Cutscenes_Skip;
    public struct CutscenesActions
    {
        private @CutsceneControls m_Wrapper;
        public CutscenesActions(@CutsceneControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Next => m_Wrapper.m_Cutscenes_Next;
        public InputAction @Skip => m_Wrapper.m_Cutscenes_Skip;
        public InputActionMap Get() { return m_Wrapper.m_Cutscenes; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CutscenesActions set) { return set.Get(); }
        public void SetCallbacks(ICutscenesActions instance)
        {
            if (m_Wrapper.m_CutscenesActionsCallbackInterface != null)
            {
                @Next.started -= m_Wrapper.m_CutscenesActionsCallbackInterface.OnNext;
                @Next.performed -= m_Wrapper.m_CutscenesActionsCallbackInterface.OnNext;
                @Next.canceled -= m_Wrapper.m_CutscenesActionsCallbackInterface.OnNext;
                @Skip.started -= m_Wrapper.m_CutscenesActionsCallbackInterface.OnSkip;
                @Skip.performed -= m_Wrapper.m_CutscenesActionsCallbackInterface.OnSkip;
                @Skip.canceled -= m_Wrapper.m_CutscenesActionsCallbackInterface.OnSkip;
            }
            m_Wrapper.m_CutscenesActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Next.started += instance.OnNext;
                @Next.performed += instance.OnNext;
                @Next.canceled += instance.OnNext;
                @Skip.started += instance.OnSkip;
                @Skip.performed += instance.OnSkip;
                @Skip.canceled += instance.OnSkip;
            }
        }
    }
    public CutscenesActions @Cutscenes => new CutscenesActions(this);

    // Maps
    private readonly InputActionMap m_Maps;
    private IMapsActions m_MapsActionsCallbackInterface;
    private readonly InputAction m_Maps_FastMode;
    private readonly InputAction m_Maps_SDialogueNext;
    private readonly InputAction m_Maps_BDialogueNext;
    public struct MapsActions
    {
        private @CutsceneControls m_Wrapper;
        public MapsActions(@CutsceneControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @FastMode => m_Wrapper.m_Maps_FastMode;
        public InputAction @SDialogueNext => m_Wrapper.m_Maps_SDialogueNext;
        public InputAction @BDialogueNext => m_Wrapper.m_Maps_BDialogueNext;
        public InputActionMap Get() { return m_Wrapper.m_Maps; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MapsActions set) { return set.Get(); }
        public void SetCallbacks(IMapsActions instance)
        {
            if (m_Wrapper.m_MapsActionsCallbackInterface != null)
            {
                @FastMode.started -= m_Wrapper.m_MapsActionsCallbackInterface.OnFastMode;
                @FastMode.performed -= m_Wrapper.m_MapsActionsCallbackInterface.OnFastMode;
                @FastMode.canceled -= m_Wrapper.m_MapsActionsCallbackInterface.OnFastMode;
                @SDialogueNext.started -= m_Wrapper.m_MapsActionsCallbackInterface.OnSDialogueNext;
                @SDialogueNext.performed -= m_Wrapper.m_MapsActionsCallbackInterface.OnSDialogueNext;
                @SDialogueNext.canceled -= m_Wrapper.m_MapsActionsCallbackInterface.OnSDialogueNext;
                @BDialogueNext.started -= m_Wrapper.m_MapsActionsCallbackInterface.OnBDialogueNext;
                @BDialogueNext.performed -= m_Wrapper.m_MapsActionsCallbackInterface.OnBDialogueNext;
                @BDialogueNext.canceled -= m_Wrapper.m_MapsActionsCallbackInterface.OnBDialogueNext;
            }
            m_Wrapper.m_MapsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @FastMode.started += instance.OnFastMode;
                @FastMode.performed += instance.OnFastMode;
                @FastMode.canceled += instance.OnFastMode;
                @SDialogueNext.started += instance.OnSDialogueNext;
                @SDialogueNext.performed += instance.OnSDialogueNext;
                @SDialogueNext.canceled += instance.OnSDialogueNext;
                @BDialogueNext.started += instance.OnBDialogueNext;
                @BDialogueNext.performed += instance.OnBDialogueNext;
                @BDialogueNext.canceled += instance.OnBDialogueNext;
            }
        }
    }
    public MapsActions @Maps => new MapsActions(this);
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    public interface ICutscenesActions
    {
        void OnNext(InputAction.CallbackContext context);
        void OnSkip(InputAction.CallbackContext context);
    }
    public interface IMapsActions
    {
        void OnFastMode(InputAction.CallbackContext context);
        void OnSDialogueNext(InputAction.CallbackContext context);
        void OnBDialogueNext(InputAction.CallbackContext context);
    }
}
