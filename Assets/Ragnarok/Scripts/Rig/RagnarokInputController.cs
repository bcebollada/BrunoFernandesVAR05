using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.InputSystem;

public class RagnarokInputController : MonoBehaviour
{
    private VRInputActions actions;
    public bool debugging;


    public Vector2 Joystick;

    private void OnValidate()
    {
        Joystick = Vector3.ClampMagnitude(Joystick, 1);
    }

    private void Awake()
    {
        actions = new VRInputActions();
        actions.Enable();
    }

    private void Update()
    {
        XRHMD hmd = InputSystem.GetDevice<XRHMD>();

        if (!debugging)
        {
            Joystick = actions.Default.Joystick.ReadValue<Vector2>();
        }
    }
}
