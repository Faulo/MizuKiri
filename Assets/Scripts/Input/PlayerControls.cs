//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.3
//     from Assets/ScriptableObjects/PlayerControls.inputactions
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

namespace MizuKiri.Input
{
    public partial class @PlayerControls : IInputActionCollection2, IDisposable
    {
        public InputActionAsset asset { get; }
        public @PlayerControls()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Touch"",
            ""id"": ""5dd9f3a8-335d-4e5d-9b02-c6d7ea7bf22c"",
            ""actions"": [
                {
                    ""name"": ""Touch"",
                    ""type"": ""PassThrough"",
                    ""id"": ""0308b199-f521-43c9-9b36-3595c947c654"",
                    ""expectedControlType"": ""Touch"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""MouseClick"",
                    ""type"": ""PassThrough"",
                    ""id"": ""3b633cf6-a099-4be2-a878-ba07dc0ffa7e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""MousePosition"",
                    ""type"": ""PassThrough"",
                    ""id"": ""4ceec40e-8dfa-4596-9653-ce77f70d2b19"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""803cf04e-2968-4a3d-968d-bda9fec831c6"",
                    ""path"": ""<Pointer>/primaryTouch"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Touch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ec640f08-3a22-4fe6-abe0-27a39c087a22"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""659309c3-e1ae-4a53-b160-2a6a44086f8a"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Gyro"",
            ""id"": ""84dde7e3-5a1e-436c-83de-c198249b25c0"",
            ""actions"": [
                {
                    ""name"": ""Sensor"",
                    ""type"": ""PassThrough"",
                    ""id"": ""488eccc2-7cf9-48ff-a4a6-f304a2c2c3e4"",
                    ""expectedControlType"": ""Vector3"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""MouseClick"",
                    ""type"": ""PassThrough"",
                    ""id"": ""33371209-3a75-4d16-b502-0725ca5cf655"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""MouseDelta"",
                    ""type"": ""PassThrough"",
                    ""id"": ""730e367e-d0ed-48db-895e-a2bc361a9599"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""a956b2b4-dde1-4558-9565-83e6264e9042"",
                    ""path"": ""<GravitySensor>/gravity"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Sensor"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c606d912-a3e2-498f-a613-b9bca8e27871"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseDelta"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d5742a81-52ee-4a6a-8c70-16cc50f6d54d"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // Touch
            m_Touch = asset.FindActionMap("Touch", throwIfNotFound: true);
            m_Touch_Touch = m_Touch.FindAction("Touch", throwIfNotFound: true);
            m_Touch_MouseClick = m_Touch.FindAction("MouseClick", throwIfNotFound: true);
            m_Touch_MousePosition = m_Touch.FindAction("MousePosition", throwIfNotFound: true);
            // Gyro
            m_Gyro = asset.FindActionMap("Gyro", throwIfNotFound: true);
            m_Gyro_Sensor = m_Gyro.FindAction("Sensor", throwIfNotFound: true);
            m_Gyro_MouseClick = m_Gyro.FindAction("MouseClick", throwIfNotFound: true);
            m_Gyro_MouseDelta = m_Gyro.FindAction("MouseDelta", throwIfNotFound: true);
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

        // Touch
        private readonly InputActionMap m_Touch;
        private ITouchActions m_TouchActionsCallbackInterface;
        private readonly InputAction m_Touch_Touch;
        private readonly InputAction m_Touch_MouseClick;
        private readonly InputAction m_Touch_MousePosition;
        public struct TouchActions
        {
            private @PlayerControls m_Wrapper;
            public TouchActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
            public InputAction @Touch => m_Wrapper.m_Touch_Touch;
            public InputAction @MouseClick => m_Wrapper.m_Touch_MouseClick;
            public InputAction @MousePosition => m_Wrapper.m_Touch_MousePosition;
            public InputActionMap Get() { return m_Wrapper.m_Touch; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(TouchActions set) { return set.Get(); }
            public void SetCallbacks(ITouchActions instance)
            {
                if (m_Wrapper.m_TouchActionsCallbackInterface != null)
                {
                    @Touch.started -= m_Wrapper.m_TouchActionsCallbackInterface.OnTouch;
                    @Touch.performed -= m_Wrapper.m_TouchActionsCallbackInterface.OnTouch;
                    @Touch.canceled -= m_Wrapper.m_TouchActionsCallbackInterface.OnTouch;
                    @MouseClick.started -= m_Wrapper.m_TouchActionsCallbackInterface.OnMouseClick;
                    @MouseClick.performed -= m_Wrapper.m_TouchActionsCallbackInterface.OnMouseClick;
                    @MouseClick.canceled -= m_Wrapper.m_TouchActionsCallbackInterface.OnMouseClick;
                    @MousePosition.started -= m_Wrapper.m_TouchActionsCallbackInterface.OnMousePosition;
                    @MousePosition.performed -= m_Wrapper.m_TouchActionsCallbackInterface.OnMousePosition;
                    @MousePosition.canceled -= m_Wrapper.m_TouchActionsCallbackInterface.OnMousePosition;
                }
                m_Wrapper.m_TouchActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Touch.started += instance.OnTouch;
                    @Touch.performed += instance.OnTouch;
                    @Touch.canceled += instance.OnTouch;
                    @MouseClick.started += instance.OnMouseClick;
                    @MouseClick.performed += instance.OnMouseClick;
                    @MouseClick.canceled += instance.OnMouseClick;
                    @MousePosition.started += instance.OnMousePosition;
                    @MousePosition.performed += instance.OnMousePosition;
                    @MousePosition.canceled += instance.OnMousePosition;
                }
            }
        }
        public TouchActions @Touch => new TouchActions(this);

        // Gyro
        private readonly InputActionMap m_Gyro;
        private IGyroActions m_GyroActionsCallbackInterface;
        private readonly InputAction m_Gyro_Sensor;
        private readonly InputAction m_Gyro_MouseClick;
        private readonly InputAction m_Gyro_MouseDelta;
        public struct GyroActions
        {
            private @PlayerControls m_Wrapper;
            public GyroActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
            public InputAction @Sensor => m_Wrapper.m_Gyro_Sensor;
            public InputAction @MouseClick => m_Wrapper.m_Gyro_MouseClick;
            public InputAction @MouseDelta => m_Wrapper.m_Gyro_MouseDelta;
            public InputActionMap Get() { return m_Wrapper.m_Gyro; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(GyroActions set) { return set.Get(); }
            public void SetCallbacks(IGyroActions instance)
            {
                if (m_Wrapper.m_GyroActionsCallbackInterface != null)
                {
                    @Sensor.started -= m_Wrapper.m_GyroActionsCallbackInterface.OnSensor;
                    @Sensor.performed -= m_Wrapper.m_GyroActionsCallbackInterface.OnSensor;
                    @Sensor.canceled -= m_Wrapper.m_GyroActionsCallbackInterface.OnSensor;
                    @MouseClick.started -= m_Wrapper.m_GyroActionsCallbackInterface.OnMouseClick;
                    @MouseClick.performed -= m_Wrapper.m_GyroActionsCallbackInterface.OnMouseClick;
                    @MouseClick.canceled -= m_Wrapper.m_GyroActionsCallbackInterface.OnMouseClick;
                    @MouseDelta.started -= m_Wrapper.m_GyroActionsCallbackInterface.OnMouseDelta;
                    @MouseDelta.performed -= m_Wrapper.m_GyroActionsCallbackInterface.OnMouseDelta;
                    @MouseDelta.canceled -= m_Wrapper.m_GyroActionsCallbackInterface.OnMouseDelta;
                }
                m_Wrapper.m_GyroActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Sensor.started += instance.OnSensor;
                    @Sensor.performed += instance.OnSensor;
                    @Sensor.canceled += instance.OnSensor;
                    @MouseClick.started += instance.OnMouseClick;
                    @MouseClick.performed += instance.OnMouseClick;
                    @MouseClick.canceled += instance.OnMouseClick;
                    @MouseDelta.started += instance.OnMouseDelta;
                    @MouseDelta.performed += instance.OnMouseDelta;
                    @MouseDelta.canceled += instance.OnMouseDelta;
                }
            }
        }
        public GyroActions @Gyro => new GyroActions(this);
        public interface ITouchActions
        {
            void OnTouch(InputAction.CallbackContext context);
            void OnMouseClick(InputAction.CallbackContext context);
            void OnMousePosition(InputAction.CallbackContext context);
        }
        public interface IGyroActions
        {
            void OnSensor(InputAction.CallbackContext context);
            void OnMouseClick(InputAction.CallbackContext context);
            void OnMouseDelta(InputAction.CallbackContext context);
        }
    }
}
