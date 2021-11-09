// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/CutsceneControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @CutsceneControls : IInputActionCollection, IDisposable
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
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Skip"",
                    ""type"": ""Button"",
                    ""id"": ""b5c8cbda-ade5-403f-93bb-dc9e2932e546"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""BDialogueNext"",
                    ""type"": ""Button"",
                    ""id"": ""c64f1769-cd71-499b-a886-d4b8c9af9f33"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
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
                },
                {
                    ""name"": """",
                    ""id"": ""238d6ff2-6bcf-4403-986f-9b0485adde38"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
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
        m_Cutscenes_BDialogueNext = m_Cutscenes.FindAction("BDialogueNext", throwIfNotFound: true);
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

    // Cutscenes
    private readonly InputActionMap m_Cutscenes;
    private ICutscenesActions m_CutscenesActionsCallbackInterface;
    private readonly InputAction m_Cutscenes_Next;
    private readonly InputAction m_Cutscenes_Skip;
    private readonly InputAction m_Cutscenes_BDialogueNext;
    public struct CutscenesActions
    {
        private @CutsceneControls m_Wrapper;
        public CutscenesActions(@CutsceneControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Next => m_Wrapper.m_Cutscenes_Next;
        public InputAction @Skip => m_Wrapper.m_Cutscenes_Skip;
        public InputAction @BDialogueNext => m_Wrapper.m_Cutscenes_BDialogueNext;
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
                @BDialogueNext.started -= m_Wrapper.m_CutscenesActionsCallbackInterface.OnBDialogueNext;
                @BDialogueNext.performed -= m_Wrapper.m_CutscenesActionsCallbackInterface.OnBDialogueNext;
                @BDialogueNext.canceled -= m_Wrapper.m_CutscenesActionsCallbackInterface.OnBDialogueNext;
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
                @BDialogueNext.started += instance.OnBDialogueNext;
                @BDialogueNext.performed += instance.OnBDialogueNext;
                @BDialogueNext.canceled += instance.OnBDialogueNext;
            }
        }
    }
    public CutscenesActions @Cutscenes => new CutscenesActions(this);
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
        void OnBDialogueNext(InputAction.CallbackContext context);
    }
}
