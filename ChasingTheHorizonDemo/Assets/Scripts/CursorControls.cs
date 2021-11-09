// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/CursorControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @CursorControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @CursorControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""CursorControls"",
    ""maps"": [
        {
            ""name"": ""MapCursor"",
            ""id"": ""d348240a-6b68-4aba-80c8-8c4d2589abb1"",
            ""actions"": [
                {
                    ""name"": ""SelectUnit"",
                    ""type"": ""Button"",
                    ""id"": ""784a4757-55e6-4482-9ff6-83d78cc1664e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""DisplayMenu"",
                    ""type"": ""Button"",
                    ""id"": ""8dfa2959-24e7-4c48-b608-11026d7b7a80"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Movement"",
                    ""type"": ""Button"",
                    ""id"": ""c7009a6a-f749-4a55-ac79-22ddd177894b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""DeselectUnit"",
                    ""type"": ""Button"",
                    ""id"": ""f06fe24f-baa6-4f15-a0e6-71e1299505e8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""SelectEnemy"",
                    ""type"": ""Button"",
                    ""id"": ""ec83e062-ac13-467f-aa39-e5a5ffc3ef49"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""ResetTiles"",
                    ""type"": ""Button"",
                    ""id"": ""fd427a23-4074-4c5e-9e44-2dc620c774d8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""a92728c3-fe4d-4d02-afc5-3a237538173d"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""SelectUnit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""55bd2c5a-ab8f-4517-9037-2716d3428314"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""SelectUnit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""98642d6c-a0d6-4afc-8a7b-d4f5cbd8305e"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""DisplayMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a237a387-4482-489d-a93d-79254b45f9ed"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""DisplayMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""223c39f2-fca6-4efd-aa11-c82c1abf3218"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""acfea999-98c2-4f3b-9941-15a38f3514f1"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""a610c408-65e4-422e-94d7-f8aca1b0e4ed"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""0838b318-46cb-4393-940e-0d596f27fb8b"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""f240ad4d-439e-4655-aff0-ab7c382043eb"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""c56dc75d-fdff-415c-b087-cf43a3690661"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""15488850-4f59-4332-b72a-c687e28e042d"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""e1f2d31e-a44d-4e72-8410-c92d36f62e23"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""dc43ea10-fb95-430c-b183-3d0aec917c53"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""ad771e83-4453-4a21-bbda-62d4fd52ca3e"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""08294f9e-477a-4c88-8cce-8859afa43d97"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""89c6000c-50b2-4079-bfb1-2d5570ba5ab7"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""2b28b453-6925-4648-b9e3-ffaedb656b60"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""bd5254a7-e773-4db5-a9b8-cd9f95734782"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""72a7e14e-13f5-42a0-adb8-24ba8f2a0df4"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""26d4dcd2-9933-4a4c-ae21-8f7acf43021e"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""DeselectUnit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""350aeafc-4220-4db5-9f12-ecfc7e7c4020"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""DeselectUnit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6c696d79-8654-41c3-ae0c-1b8437f65a88"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""SelectEnemy"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""79b5bc5b-2829-45fc-9dcd-c3455e33af33"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""ResetTiles"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""UnitCursor"",
            ""id"": ""568d4dd3-a946-4692-b82b-cea3537b861d"",
            ""actions"": [
                {
                    ""name"": ""MoveUnit"",
                    ""type"": ""Button"",
                    ""id"": ""b8757fec-c879-48ed-9006-b04fdf994cdb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""DeselectUnit"",
                    ""type"": ""Button"",
                    ""id"": ""30e783d5-08c4-47dd-bbcd-dd16be4f7983"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Movement"",
                    ""type"": ""Button"",
                    ""id"": ""4394595b-bc42-4c06-a317-4b710fbb49ce"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""59d6125f-2492-4e22-b718-770c52f3af8c"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""DeselectUnit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""43f9a86f-bec2-4831-ab95-ff7028f9a41c"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""DeselectUnit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""947c6929-aaee-4d3d-82e2-7d9ac4d75e19"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""e344a6dd-4178-48e4-8dc6-1040ccf2db03"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""46b40a69-0281-4356-b858-a1bf4cfc3b84"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""96e0697d-8b74-4959-9c1f-c6034d533ac7"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""5caaa3f7-3fe2-439e-a938-af620dbf68e1"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""e55285c1-f35c-47dd-bc45-681c82c50a66"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""up"",
                    ""id"": ""76748495-f924-4dc9-a091-a5f8fd3a97fb"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""973c7291-c85a-442a-880f-b1e18f910312"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""711661ef-eba7-41b8-bb16-03b11e0586d5"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""b5921186-a9ea-431c-ba79-6ed4ba3e3676"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""86d2923d-e794-48f1-874d-52553062e447"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""45058f36-b7f6-4037-ad23-d145eb59625c"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""e57ddbaf-b110-4a38-a12b-2c51563a16ec"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""d5259408-23a1-4729-9156-8bfb60613fe9"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""9078ef4f-f73f-4061-a18d-16018e8ff78b"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""3cd5538c-e6cf-4d5d-bb2b-451b4cb92a30"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MoveUnit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8c7d98df-dc0c-4cc8-aa5b-e918989f457c"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""MoveUnit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""ActionMenuCursor"",
            ""id"": ""6d5867d8-5862-4d6e-a4ba-5de617c82d01"",
            ""actions"": [
                {
                    ""name"": ""UndoMove"",
                    ""type"": ""Button"",
                    ""id"": ""fe5e5963-8476-4597-8192-d617efcb9500"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""29d0f369-db32-4804-a634-ae88df1308c7"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""UndoMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""463569e0-7e5c-4c11-be49-039e883761f5"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""UndoMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""MenuCursor"",
            ""id"": ""f91ea50d-64c1-4aa3-b85f-2a17137bb689"",
            ""actions"": [
                {
                    ""name"": ""CloseMenu"",
                    ""type"": ""Button"",
                    ""id"": ""ad89ac3b-0ac0-4bbf-bac1-a388d4548216"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""7d09b365-5cce-4782-b1ca-db28b1cb71b6"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""CloseMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""16758109-11c5-43c3-a8c4-86c95a908d55"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""CloseMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""AttackCursor"",
            ""id"": ""f9981cc8-7653-46b5-bcfb-16e714c1949c"",
            ""actions"": [
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""dfa2dd04-fe09-47d5-9a5f-b69a5e155b46"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Movement"",
                    ""type"": ""Button"",
                    ""id"": ""8f4ec391-f7aa-40a7-b65b-bac51581912b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Cancel"",
                    ""type"": ""Button"",
                    ""id"": ""99a2c028-5b65-4a8f-82d8-c8c18318ac47"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""f1d5aae9-0d1b-4a2e-b3d0-b4317ffea715"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4c6a80ff-819b-46a0-8318-c90ce505c8a5"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""b8d136b0-3805-48b7-8bb6-38419e50f42b"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""ddb42846-28af-49b3-807e-514bf11763e3"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""7bfc2a0e-8cda-49d2-bdb0-e1a16b9d50da"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""c913a45c-60b9-437e-a33d-f759c8586789"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""632205fb-354b-4273-8875-80e8c48c4239"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""2a658462-a601-4b5c-adc4-00041ef87240"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""26315fa3-83ca-4299-86d0-f66db7d27676"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""191e5f20-7b52-45ac-a33b-3ff91790c38d"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""a0f71487-de60-46fa-bd45-0ed6b6c20905"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""c2043701-98bb-4220-85c4-89da9804c869"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""f86b64a3-436e-49d6-b6ce-6093b774c90d"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""17564916-2b94-48ac-9e0d-e1de3171c579"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""b6e3619d-c35f-4c25-8f3d-68538b40cceb"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""f142531e-7c79-4f8d-ae11-d347d9727a14"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""9e0cda31-d4bf-4d21-b5de-dcef5ef2ee64"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""13cf555a-bb4d-462c-a41b-a8f1110d8470"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""87eb4fdd-1723-443a-b173-deb046e69630"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""NeutralCursor"",
            ""id"": ""22c0acb0-4bdd-4e95-8d63-24e4eef55f91"",
            ""actions"": [
                {
                    ""name"": ""New action"",
                    ""type"": ""Button"",
                    ""id"": ""f44be0c9-429f-4e07-9b7e-ced29e84e507"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""f9583210-4f41-4423-a0a0-85828461ebdb"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""New action"",
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
        // MapCursor
        m_MapCursor = asset.FindActionMap("MapCursor", throwIfNotFound: true);
        m_MapCursor_SelectUnit = m_MapCursor.FindAction("SelectUnit", throwIfNotFound: true);
        m_MapCursor_DisplayMenu = m_MapCursor.FindAction("DisplayMenu", throwIfNotFound: true);
        m_MapCursor_Movement = m_MapCursor.FindAction("Movement", throwIfNotFound: true);
        m_MapCursor_DeselectUnit = m_MapCursor.FindAction("DeselectUnit", throwIfNotFound: true);
        m_MapCursor_SelectEnemy = m_MapCursor.FindAction("SelectEnemy", throwIfNotFound: true);
        m_MapCursor_ResetTiles = m_MapCursor.FindAction("ResetTiles", throwIfNotFound: true);
        // UnitCursor
        m_UnitCursor = asset.FindActionMap("UnitCursor", throwIfNotFound: true);
        m_UnitCursor_MoveUnit = m_UnitCursor.FindAction("MoveUnit", throwIfNotFound: true);
        m_UnitCursor_DeselectUnit = m_UnitCursor.FindAction("DeselectUnit", throwIfNotFound: true);
        m_UnitCursor_Movement = m_UnitCursor.FindAction("Movement", throwIfNotFound: true);
        // ActionMenuCursor
        m_ActionMenuCursor = asset.FindActionMap("ActionMenuCursor", throwIfNotFound: true);
        m_ActionMenuCursor_UndoMove = m_ActionMenuCursor.FindAction("UndoMove", throwIfNotFound: true);
        // MenuCursor
        m_MenuCursor = asset.FindActionMap("MenuCursor", throwIfNotFound: true);
        m_MenuCursor_CloseMenu = m_MenuCursor.FindAction("CloseMenu", throwIfNotFound: true);
        // AttackCursor
        m_AttackCursor = asset.FindActionMap("AttackCursor", throwIfNotFound: true);
        m_AttackCursor_Attack = m_AttackCursor.FindAction("Attack", throwIfNotFound: true);
        m_AttackCursor_Movement = m_AttackCursor.FindAction("Movement", throwIfNotFound: true);
        m_AttackCursor_Cancel = m_AttackCursor.FindAction("Cancel", throwIfNotFound: true);
        // NeutralCursor
        m_NeutralCursor = asset.FindActionMap("NeutralCursor", throwIfNotFound: true);
        m_NeutralCursor_Newaction = m_NeutralCursor.FindAction("New action", throwIfNotFound: true);
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

    // MapCursor
    private readonly InputActionMap m_MapCursor;
    private IMapCursorActions m_MapCursorActionsCallbackInterface;
    private readonly InputAction m_MapCursor_SelectUnit;
    private readonly InputAction m_MapCursor_DisplayMenu;
    private readonly InputAction m_MapCursor_Movement;
    private readonly InputAction m_MapCursor_DeselectUnit;
    private readonly InputAction m_MapCursor_SelectEnemy;
    private readonly InputAction m_MapCursor_ResetTiles;
    public struct MapCursorActions
    {
        private @CursorControls m_Wrapper;
        public MapCursorActions(@CursorControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @SelectUnit => m_Wrapper.m_MapCursor_SelectUnit;
        public InputAction @DisplayMenu => m_Wrapper.m_MapCursor_DisplayMenu;
        public InputAction @Movement => m_Wrapper.m_MapCursor_Movement;
        public InputAction @DeselectUnit => m_Wrapper.m_MapCursor_DeselectUnit;
        public InputAction @SelectEnemy => m_Wrapper.m_MapCursor_SelectEnemy;
        public InputAction @ResetTiles => m_Wrapper.m_MapCursor_ResetTiles;
        public InputActionMap Get() { return m_Wrapper.m_MapCursor; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MapCursorActions set) { return set.Get(); }
        public void SetCallbacks(IMapCursorActions instance)
        {
            if (m_Wrapper.m_MapCursorActionsCallbackInterface != null)
            {
                @SelectUnit.started -= m_Wrapper.m_MapCursorActionsCallbackInterface.OnSelectUnit;
                @SelectUnit.performed -= m_Wrapper.m_MapCursorActionsCallbackInterface.OnSelectUnit;
                @SelectUnit.canceled -= m_Wrapper.m_MapCursorActionsCallbackInterface.OnSelectUnit;
                @DisplayMenu.started -= m_Wrapper.m_MapCursorActionsCallbackInterface.OnDisplayMenu;
                @DisplayMenu.performed -= m_Wrapper.m_MapCursorActionsCallbackInterface.OnDisplayMenu;
                @DisplayMenu.canceled -= m_Wrapper.m_MapCursorActionsCallbackInterface.OnDisplayMenu;
                @Movement.started -= m_Wrapper.m_MapCursorActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_MapCursorActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_MapCursorActionsCallbackInterface.OnMovement;
                @DeselectUnit.started -= m_Wrapper.m_MapCursorActionsCallbackInterface.OnDeselectUnit;
                @DeselectUnit.performed -= m_Wrapper.m_MapCursorActionsCallbackInterface.OnDeselectUnit;
                @DeselectUnit.canceled -= m_Wrapper.m_MapCursorActionsCallbackInterface.OnDeselectUnit;
                @SelectEnemy.started -= m_Wrapper.m_MapCursorActionsCallbackInterface.OnSelectEnemy;
                @SelectEnemy.performed -= m_Wrapper.m_MapCursorActionsCallbackInterface.OnSelectEnemy;
                @SelectEnemy.canceled -= m_Wrapper.m_MapCursorActionsCallbackInterface.OnSelectEnemy;
                @ResetTiles.started -= m_Wrapper.m_MapCursorActionsCallbackInterface.OnResetTiles;
                @ResetTiles.performed -= m_Wrapper.m_MapCursorActionsCallbackInterface.OnResetTiles;
                @ResetTiles.canceled -= m_Wrapper.m_MapCursorActionsCallbackInterface.OnResetTiles;
            }
            m_Wrapper.m_MapCursorActionsCallbackInterface = instance;
            if (instance != null)
            {
                @SelectUnit.started += instance.OnSelectUnit;
                @SelectUnit.performed += instance.OnSelectUnit;
                @SelectUnit.canceled += instance.OnSelectUnit;
                @DisplayMenu.started += instance.OnDisplayMenu;
                @DisplayMenu.performed += instance.OnDisplayMenu;
                @DisplayMenu.canceled += instance.OnDisplayMenu;
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @DeselectUnit.started += instance.OnDeselectUnit;
                @DeselectUnit.performed += instance.OnDeselectUnit;
                @DeselectUnit.canceled += instance.OnDeselectUnit;
                @SelectEnemy.started += instance.OnSelectEnemy;
                @SelectEnemy.performed += instance.OnSelectEnemy;
                @SelectEnemy.canceled += instance.OnSelectEnemy;
                @ResetTiles.started += instance.OnResetTiles;
                @ResetTiles.performed += instance.OnResetTiles;
                @ResetTiles.canceled += instance.OnResetTiles;
            }
        }
    }
    public MapCursorActions @MapCursor => new MapCursorActions(this);

    // UnitCursor
    private readonly InputActionMap m_UnitCursor;
    private IUnitCursorActions m_UnitCursorActionsCallbackInterface;
    private readonly InputAction m_UnitCursor_MoveUnit;
    private readonly InputAction m_UnitCursor_DeselectUnit;
    private readonly InputAction m_UnitCursor_Movement;
    public struct UnitCursorActions
    {
        private @CursorControls m_Wrapper;
        public UnitCursorActions(@CursorControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @MoveUnit => m_Wrapper.m_UnitCursor_MoveUnit;
        public InputAction @DeselectUnit => m_Wrapper.m_UnitCursor_DeselectUnit;
        public InputAction @Movement => m_Wrapper.m_UnitCursor_Movement;
        public InputActionMap Get() { return m_Wrapper.m_UnitCursor; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UnitCursorActions set) { return set.Get(); }
        public void SetCallbacks(IUnitCursorActions instance)
        {
            if (m_Wrapper.m_UnitCursorActionsCallbackInterface != null)
            {
                @MoveUnit.started -= m_Wrapper.m_UnitCursorActionsCallbackInterface.OnMoveUnit;
                @MoveUnit.performed -= m_Wrapper.m_UnitCursorActionsCallbackInterface.OnMoveUnit;
                @MoveUnit.canceled -= m_Wrapper.m_UnitCursorActionsCallbackInterface.OnMoveUnit;
                @DeselectUnit.started -= m_Wrapper.m_UnitCursorActionsCallbackInterface.OnDeselectUnit;
                @DeselectUnit.performed -= m_Wrapper.m_UnitCursorActionsCallbackInterface.OnDeselectUnit;
                @DeselectUnit.canceled -= m_Wrapper.m_UnitCursorActionsCallbackInterface.OnDeselectUnit;
                @Movement.started -= m_Wrapper.m_UnitCursorActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_UnitCursorActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_UnitCursorActionsCallbackInterface.OnMovement;
            }
            m_Wrapper.m_UnitCursorActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MoveUnit.started += instance.OnMoveUnit;
                @MoveUnit.performed += instance.OnMoveUnit;
                @MoveUnit.canceled += instance.OnMoveUnit;
                @DeselectUnit.started += instance.OnDeselectUnit;
                @DeselectUnit.performed += instance.OnDeselectUnit;
                @DeselectUnit.canceled += instance.OnDeselectUnit;
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
            }
        }
    }
    public UnitCursorActions @UnitCursor => new UnitCursorActions(this);

    // ActionMenuCursor
    private readonly InputActionMap m_ActionMenuCursor;
    private IActionMenuCursorActions m_ActionMenuCursorActionsCallbackInterface;
    private readonly InputAction m_ActionMenuCursor_UndoMove;
    public struct ActionMenuCursorActions
    {
        private @CursorControls m_Wrapper;
        public ActionMenuCursorActions(@CursorControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @UndoMove => m_Wrapper.m_ActionMenuCursor_UndoMove;
        public InputActionMap Get() { return m_Wrapper.m_ActionMenuCursor; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ActionMenuCursorActions set) { return set.Get(); }
        public void SetCallbacks(IActionMenuCursorActions instance)
        {
            if (m_Wrapper.m_ActionMenuCursorActionsCallbackInterface != null)
            {
                @UndoMove.started -= m_Wrapper.m_ActionMenuCursorActionsCallbackInterface.OnUndoMove;
                @UndoMove.performed -= m_Wrapper.m_ActionMenuCursorActionsCallbackInterface.OnUndoMove;
                @UndoMove.canceled -= m_Wrapper.m_ActionMenuCursorActionsCallbackInterface.OnUndoMove;
            }
            m_Wrapper.m_ActionMenuCursorActionsCallbackInterface = instance;
            if (instance != null)
            {
                @UndoMove.started += instance.OnUndoMove;
                @UndoMove.performed += instance.OnUndoMove;
                @UndoMove.canceled += instance.OnUndoMove;
            }
        }
    }
    public ActionMenuCursorActions @ActionMenuCursor => new ActionMenuCursorActions(this);

    // MenuCursor
    private readonly InputActionMap m_MenuCursor;
    private IMenuCursorActions m_MenuCursorActionsCallbackInterface;
    private readonly InputAction m_MenuCursor_CloseMenu;
    public struct MenuCursorActions
    {
        private @CursorControls m_Wrapper;
        public MenuCursorActions(@CursorControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @CloseMenu => m_Wrapper.m_MenuCursor_CloseMenu;
        public InputActionMap Get() { return m_Wrapper.m_MenuCursor; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MenuCursorActions set) { return set.Get(); }
        public void SetCallbacks(IMenuCursorActions instance)
        {
            if (m_Wrapper.m_MenuCursorActionsCallbackInterface != null)
            {
                @CloseMenu.started -= m_Wrapper.m_MenuCursorActionsCallbackInterface.OnCloseMenu;
                @CloseMenu.performed -= m_Wrapper.m_MenuCursorActionsCallbackInterface.OnCloseMenu;
                @CloseMenu.canceled -= m_Wrapper.m_MenuCursorActionsCallbackInterface.OnCloseMenu;
            }
            m_Wrapper.m_MenuCursorActionsCallbackInterface = instance;
            if (instance != null)
            {
                @CloseMenu.started += instance.OnCloseMenu;
                @CloseMenu.performed += instance.OnCloseMenu;
                @CloseMenu.canceled += instance.OnCloseMenu;
            }
        }
    }
    public MenuCursorActions @MenuCursor => new MenuCursorActions(this);

    // AttackCursor
    private readonly InputActionMap m_AttackCursor;
    private IAttackCursorActions m_AttackCursorActionsCallbackInterface;
    private readonly InputAction m_AttackCursor_Attack;
    private readonly InputAction m_AttackCursor_Movement;
    private readonly InputAction m_AttackCursor_Cancel;
    public struct AttackCursorActions
    {
        private @CursorControls m_Wrapper;
        public AttackCursorActions(@CursorControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Attack => m_Wrapper.m_AttackCursor_Attack;
        public InputAction @Movement => m_Wrapper.m_AttackCursor_Movement;
        public InputAction @Cancel => m_Wrapper.m_AttackCursor_Cancel;
        public InputActionMap Get() { return m_Wrapper.m_AttackCursor; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(AttackCursorActions set) { return set.Get(); }
        public void SetCallbacks(IAttackCursorActions instance)
        {
            if (m_Wrapper.m_AttackCursorActionsCallbackInterface != null)
            {
                @Attack.started -= m_Wrapper.m_AttackCursorActionsCallbackInterface.OnAttack;
                @Attack.performed -= m_Wrapper.m_AttackCursorActionsCallbackInterface.OnAttack;
                @Attack.canceled -= m_Wrapper.m_AttackCursorActionsCallbackInterface.OnAttack;
                @Movement.started -= m_Wrapper.m_AttackCursorActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_AttackCursorActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_AttackCursorActionsCallbackInterface.OnMovement;
                @Cancel.started -= m_Wrapper.m_AttackCursorActionsCallbackInterface.OnCancel;
                @Cancel.performed -= m_Wrapper.m_AttackCursorActionsCallbackInterface.OnCancel;
                @Cancel.canceled -= m_Wrapper.m_AttackCursorActionsCallbackInterface.OnCancel;
            }
            m_Wrapper.m_AttackCursorActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Attack.started += instance.OnAttack;
                @Attack.performed += instance.OnAttack;
                @Attack.canceled += instance.OnAttack;
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Cancel.started += instance.OnCancel;
                @Cancel.performed += instance.OnCancel;
                @Cancel.canceled += instance.OnCancel;
            }
        }
    }
    public AttackCursorActions @AttackCursor => new AttackCursorActions(this);

    // NeutralCursor
    private readonly InputActionMap m_NeutralCursor;
    private INeutralCursorActions m_NeutralCursorActionsCallbackInterface;
    private readonly InputAction m_NeutralCursor_Newaction;
    public struct NeutralCursorActions
    {
        private @CursorControls m_Wrapper;
        public NeutralCursorActions(@CursorControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Newaction => m_Wrapper.m_NeutralCursor_Newaction;
        public InputActionMap Get() { return m_Wrapper.m_NeutralCursor; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(NeutralCursorActions set) { return set.Get(); }
        public void SetCallbacks(INeutralCursorActions instance)
        {
            if (m_Wrapper.m_NeutralCursorActionsCallbackInterface != null)
            {
                @Newaction.started -= m_Wrapper.m_NeutralCursorActionsCallbackInterface.OnNewaction;
                @Newaction.performed -= m_Wrapper.m_NeutralCursorActionsCallbackInterface.OnNewaction;
                @Newaction.canceled -= m_Wrapper.m_NeutralCursorActionsCallbackInterface.OnNewaction;
            }
            m_Wrapper.m_NeutralCursorActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Newaction.started += instance.OnNewaction;
                @Newaction.performed += instance.OnNewaction;
                @Newaction.canceled += instance.OnNewaction;
            }
        }
    }
    public NeutralCursorActions @NeutralCursor => new NeutralCursorActions(this);
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
    public interface IMapCursorActions
    {
        void OnSelectUnit(InputAction.CallbackContext context);
        void OnDisplayMenu(InputAction.CallbackContext context);
        void OnMovement(InputAction.CallbackContext context);
        void OnDeselectUnit(InputAction.CallbackContext context);
        void OnSelectEnemy(InputAction.CallbackContext context);
        void OnResetTiles(InputAction.CallbackContext context);
    }
    public interface IUnitCursorActions
    {
        void OnMoveUnit(InputAction.CallbackContext context);
        void OnDeselectUnit(InputAction.CallbackContext context);
        void OnMovement(InputAction.CallbackContext context);
    }
    public interface IActionMenuCursorActions
    {
        void OnUndoMove(InputAction.CallbackContext context);
    }
    public interface IMenuCursorActions
    {
        void OnCloseMenu(InputAction.CallbackContext context);
    }
    public interface IAttackCursorActions
    {
        void OnAttack(InputAction.CallbackContext context);
        void OnMovement(InputAction.CallbackContext context);
        void OnCancel(InputAction.CallbackContext context);
    }
    public interface INeutralCursorActions
    {
        void OnNewaction(InputAction.CallbackContext context);
    }
}
