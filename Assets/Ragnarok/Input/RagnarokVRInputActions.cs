//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/Ragnarok/Input/RagnarokVRInputActions.inputactions
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

public partial class @RagnarokVRInputActions : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @RagnarokVRInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""RagnarokVRInputActions"",
    ""maps"": [
        {
            ""name"": ""Default1"",
            ""id"": ""2535e272-61c8-4627-acba-56a454780cdb"",
            ""actions"": [
                {
                    ""name"": ""Grip Left"",
                    ""type"": ""Button"",
                    ""id"": ""df0234aa-893d-4371-8fe7-f2609f30c567"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Grip Right"",
                    ""type"": ""Button"",
                    ""id"": ""a9bf1997-f0d9-4e35-a930-be438d5d85a4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""New action"",
                    ""type"": ""Button"",
                    ""id"": ""56e5e8b5-cbd9-45d9-8c42-2351ebecb3f2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""a7036b34-0434-44fb-ac71-543b648d1684"",
                    ""path"": ""<OculusTouchController>{LeftHand}/gripPressed"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Grip Left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""37572f0d-0a07-404a-af40-6d17e0e88e0a"",
                    ""path"": ""<OculusTouchController>{RightHand}/gripPressed"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Grip Right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ad227f01-07a9-4552-9d17-8e4e3e0dfed2"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""New action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Default1
        m_Default1 = asset.FindActionMap("Default1", throwIfNotFound: true);
        m_Default1_GripLeft = m_Default1.FindAction("Grip Left", throwIfNotFound: true);
        m_Default1_GripRight = m_Default1.FindAction("Grip Right", throwIfNotFound: true);
        m_Default1_Newaction = m_Default1.FindAction("New action", throwIfNotFound: true);
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

    // Default1
    private readonly InputActionMap m_Default1;
    private IDefault1Actions m_Default1ActionsCallbackInterface;
    private readonly InputAction m_Default1_GripLeft;
    private readonly InputAction m_Default1_GripRight;
    private readonly InputAction m_Default1_Newaction;
    public struct Default1Actions
    {
        private @RagnarokVRInputActions m_Wrapper;
        public Default1Actions(@RagnarokVRInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @GripLeft => m_Wrapper.m_Default1_GripLeft;
        public InputAction @GripRight => m_Wrapper.m_Default1_GripRight;
        public InputAction @Newaction => m_Wrapper.m_Default1_Newaction;
        public InputActionMap Get() { return m_Wrapper.m_Default1; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(Default1Actions set) { return set.Get(); }
        public void SetCallbacks(IDefault1Actions instance)
        {
            if (m_Wrapper.m_Default1ActionsCallbackInterface != null)
            {
                @GripLeft.started -= m_Wrapper.m_Default1ActionsCallbackInterface.OnGripLeft;
                @GripLeft.performed -= m_Wrapper.m_Default1ActionsCallbackInterface.OnGripLeft;
                @GripLeft.canceled -= m_Wrapper.m_Default1ActionsCallbackInterface.OnGripLeft;
                @GripRight.started -= m_Wrapper.m_Default1ActionsCallbackInterface.OnGripRight;
                @GripRight.performed -= m_Wrapper.m_Default1ActionsCallbackInterface.OnGripRight;
                @GripRight.canceled -= m_Wrapper.m_Default1ActionsCallbackInterface.OnGripRight;
                @Newaction.started -= m_Wrapper.m_Default1ActionsCallbackInterface.OnNewaction;
                @Newaction.performed -= m_Wrapper.m_Default1ActionsCallbackInterface.OnNewaction;
                @Newaction.canceled -= m_Wrapper.m_Default1ActionsCallbackInterface.OnNewaction;
            }
            m_Wrapper.m_Default1ActionsCallbackInterface = instance;
            if (instance != null)
            {
                @GripLeft.started += instance.OnGripLeft;
                @GripLeft.performed += instance.OnGripLeft;
                @GripLeft.canceled += instance.OnGripLeft;
                @GripRight.started += instance.OnGripRight;
                @GripRight.performed += instance.OnGripRight;
                @GripRight.canceled += instance.OnGripRight;
                @Newaction.started += instance.OnNewaction;
                @Newaction.performed += instance.OnNewaction;
                @Newaction.canceled += instance.OnNewaction;
            }
        }
    }
    public Default1Actions @Default1 => new Default1Actions(this);
    public interface IDefault1Actions
    {
        void OnGripLeft(InputAction.CallbackContext context);
        void OnGripRight(InputAction.CallbackContext context);
        void OnNewaction(InputAction.CallbackContext context);
    }
}
