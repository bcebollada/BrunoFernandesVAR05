using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.InputSystem;

public class RagnarokVRInputController : MonoBehaviour
{
    private RagnarokVRInputActions actions;
    
    public Vector2 JoystickLeft;
    public Vector2 JoystickRight;
    public bool ThumbPressedLeft;
    public bool B_ButtonPressed;

    public bool TriggerPressedLeft;
    public bool TriggerPressedRight;

    public bool debugging;

    private void OnValidate()
    {
        JoystickLeft = Vector3.ClampMagnitude(JoystickLeft, 1);
        JoystickRight = Vector3.ClampMagnitude(JoystickRight, 1);
    }

    private void Awake()
    {
        actions = new RagnarokVRInputActions();
        actions.Enable();
    }

    private void Update()
    {
        XRHMD hmd = InputSystem.GetDevice<XRHMD>();

        if (!debugging)
        {
            JoystickLeft = actions.Default.JoystickLeft.ReadValue<Vector2>();
            JoystickRight = actions.Default.JoystickRight.ReadValue<Vector2>();

            if (actions.Default.ThumbPressLeft.WasPerformedThisFrame()) ThumbPressedLeft = true;
            else if(actions.Default.ThumbPressLeft.WasReleasedThisFrame()) ThumbPressedLeft = false;

            if (actions.Default.TriggerLeft.WasPerformedThisFrame()) TriggerPressedLeft = true;
            else if (actions.Default.TriggerLeft.WasReleasedThisFrame()) TriggerPressedLeft = false;

            if (actions.Default.TriggerRight.WasPerformedThisFrame()) TriggerPressedRight = true;
            else if (actions.Default.TriggerRight.WasReleasedThisFrame()) TriggerPressedRight = false;

            if (actions.Default.B_Button.WasPerformedThisFrame()) B_ButtonPressed = true;
            else if (actions.Default.B_Button.WasReleasedThisFrame()) B_ButtonPressed = false;


        }
    }
}
