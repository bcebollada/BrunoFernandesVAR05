using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.XR.Oculus.Input;
using UnityEngine.InputSystem.XR;

public class VRButtonInput : MonoBehaviour
{
    VRInputActions vrInputActions;

    private void Start()
    {
        vrInputActions = new VRInputActions();
        vrInputActions.Enable();
    }

    private void Update()
    {
        if(vrInputActions.Default.Primary.WasPerformedThisFrame())
        {
            Debug.Log("Primary pressed");
        }
    }

}
