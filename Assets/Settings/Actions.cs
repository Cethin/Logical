// GENERATED AUTOMATICALLY FROM 'Assets/Settings/Actions.inputactions'

using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class Actions : IInputActionCollection
{
    private InputActionAsset asset;
    public Actions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Actions"",
    ""maps"": [
        {
            ""name"": ""KBM"",
            ""id"": ""b1c3a64e-dec6-44d1-9c8a-62f87739635d"",
            ""actions"": [
                {
                    ""name"": ""Select"",
                    ""type"": ""Button"",
                    ""id"": ""7a59de21-cade-47d9-b5b7-dc5ecdb77734"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pos"",
                    ""type"": ""Value"",
                    ""id"": ""6a47de6b-96ac-4310-9ca7-c9576e15f96a"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Tick"",
                    ""type"": ""Button"",
                    ""id"": ""5eccfc38-0ebd-425c-aa44-28f8cada214f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Delete"",
                    ""type"": ""Button"",
                    ""id"": ""2ae6618f-738e-4c91-8d25-3e42a61e8d39"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Reset"",
                    ""type"": ""Button"",
                    ""id"": ""c3f02b46-b051-4256-b9b9-98f0b02211d6"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""ca01633d-3cd9-49d0-b97f-8e6df0cbbfb7"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8bd291ce-f021-41f7-9f99-3f61d692f8a8"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pos"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7ca5c8cd-4852-4f7a-beb8-141d01148eab"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Tick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""60a400cb-d6fc-4f09-8ec9-7ba0f6883075"",
                    ""path"": ""<Keyboard>/delete"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Delete"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2e3c8b45-351c-4689-81c8-8556d70b95f9"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Reset"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // KBM
        m_KBM = asset.FindActionMap("KBM", throwIfNotFound: true);
        m_KBM_Select = m_KBM.FindAction("Select", throwIfNotFound: true);
        m_KBM_Pos = m_KBM.FindAction("Pos", throwIfNotFound: true);
        m_KBM_Tick = m_KBM.FindAction("Tick", throwIfNotFound: true);
        m_KBM_Delete = m_KBM.FindAction("Delete", throwIfNotFound: true);
        m_KBM_Reset = m_KBM.FindAction("Reset", throwIfNotFound: true);
    }

    ~Actions()
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

    // KBM
    private readonly InputActionMap m_KBM;
    private IKBMActions m_KBMActionsCallbackInterface;
    private readonly InputAction m_KBM_Select;
    private readonly InputAction m_KBM_Pos;
    private readonly InputAction m_KBM_Tick;
    private readonly InputAction m_KBM_Delete;
    private readonly InputAction m_KBM_Reset;
    public struct KBMActions
    {
        private Actions m_Wrapper;
        public KBMActions(Actions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Select => m_Wrapper.m_KBM_Select;
        public InputAction @Pos => m_Wrapper.m_KBM_Pos;
        public InputAction @Tick => m_Wrapper.m_KBM_Tick;
        public InputAction @Delete => m_Wrapper.m_KBM_Delete;
        public InputAction @Reset => m_Wrapper.m_KBM_Reset;
        public InputActionMap Get() { return m_Wrapper.m_KBM; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(KBMActions set) { return set.Get(); }
        public void SetCallbacks(IKBMActions instance)
        {
            if (m_Wrapper.m_KBMActionsCallbackInterface != null)
            {
                Select.started -= m_Wrapper.m_KBMActionsCallbackInterface.OnSelect;
                Select.performed -= m_Wrapper.m_KBMActionsCallbackInterface.OnSelect;
                Select.canceled -= m_Wrapper.m_KBMActionsCallbackInterface.OnSelect;
                Pos.started -= m_Wrapper.m_KBMActionsCallbackInterface.OnPos;
                Pos.performed -= m_Wrapper.m_KBMActionsCallbackInterface.OnPos;
                Pos.canceled -= m_Wrapper.m_KBMActionsCallbackInterface.OnPos;
                Tick.started -= m_Wrapper.m_KBMActionsCallbackInterface.OnTick;
                Tick.performed -= m_Wrapper.m_KBMActionsCallbackInterface.OnTick;
                Tick.canceled -= m_Wrapper.m_KBMActionsCallbackInterface.OnTick;
                Delete.started -= m_Wrapper.m_KBMActionsCallbackInterface.OnDelete;
                Delete.performed -= m_Wrapper.m_KBMActionsCallbackInterface.OnDelete;
                Delete.canceled -= m_Wrapper.m_KBMActionsCallbackInterface.OnDelete;
                Reset.started -= m_Wrapper.m_KBMActionsCallbackInterface.OnReset;
                Reset.performed -= m_Wrapper.m_KBMActionsCallbackInterface.OnReset;
                Reset.canceled -= m_Wrapper.m_KBMActionsCallbackInterface.OnReset;
            }
            m_Wrapper.m_KBMActionsCallbackInterface = instance;
            if (instance != null)
            {
                Select.started += instance.OnSelect;
                Select.performed += instance.OnSelect;
                Select.canceled += instance.OnSelect;
                Pos.started += instance.OnPos;
                Pos.performed += instance.OnPos;
                Pos.canceled += instance.OnPos;
                Tick.started += instance.OnTick;
                Tick.performed += instance.OnTick;
                Tick.canceled += instance.OnTick;
                Delete.started += instance.OnDelete;
                Delete.performed += instance.OnDelete;
                Delete.canceled += instance.OnDelete;
                Reset.started += instance.OnReset;
                Reset.performed += instance.OnReset;
                Reset.canceled += instance.OnReset;
            }
        }
    }
    public KBMActions @KBM => new KBMActions(this);
    public interface IKBMActions
    {
        void OnSelect(InputAction.CallbackContext context);
        void OnPos(InputAction.CallbackContext context);
        void OnTick(InputAction.CallbackContext context);
        void OnDelete(InputAction.CallbackContext context);
        void OnReset(InputAction.CallbackContext context);
    }
}
