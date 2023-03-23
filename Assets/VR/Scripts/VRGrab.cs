using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.XR.Oculus.Input;
using UnityEngine.InputSystem.XR;

public class VRGrab : MonoBehaviour
{
    VRInputActions vrInputActions;
    private GameObject grabbedObject;

    private Vector3 previousPosition;
    private Vector3 handVelocity;


    public bool hasObjectGrabbed;
    public bool isLeftHand;

    public bool hasGripped;
    public bool hasRealesed;
    public bool debuggingMode;


    private void Awake()
    {
        vrInputActions = new VRInputActions();
        vrInputActions.Enable();
    }

    private void Update()
    {
        if(!debuggingMode)
        {
            if (isLeftHand)
            {
                if (vrInputActions.Default.GripLeft.WasPerformedThisFrame())
                {
                    hasGripped = true;
                    Debug.Log("grip");
                }
                else hasGripped = false;

                if (vrInputActions.Default.GripLeft.WasReleasedThisFrame()) hasRealesed = true;
                else hasRealesed = false;

            }
            else
            {
                if (vrInputActions.Default.GripRight.WasPerformedThisFrame()) hasGripped = true;
                else hasGripped = false;

                if (vrInputActions.Default.GripRight.WasReleasedThisFrame()) hasRealesed = true;
                else hasRealesed = false;
            }
        }

        if (grabbedObject != null)
        {
            if (hasRealesed)
            {
                //does something when pressed
                grabbedObject.transform.parent = null;
                grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
                grabbedObject.GetComponent<Rigidbody>().velocity = handVelocity;
                grabbedObject = null;

                hasObjectGrabbed = false;

            }
        }
    }

    private void FixedUpdate()
    {
        handVelocity = (transform.position - previousPosition) / Time.deltaTime; //gets velocity per second
        previousPosition = transform.position;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<GrabbableObject>() != null && !hasObjectGrabbed)
        {
            other.gameObject.GetComponent<GrabbableObject>().isHovered = true;
            if (hasGripped)
            {
                other.transform.parent = transform;
                grabbedObject = other.gameObject;
                other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                hasObjectGrabbed = true;
            }            
        }
    }
}
