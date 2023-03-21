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

    private void Awake()
    {
        vrInputActions = new VRInputActions();
        vrInputActions.Enable();
    }

    private void Update()
    {
        if (grabbedObject != null)
        {
            if (vrInputActions.Default.Primary.WasReleasedThisFrame())
            {
                //does something when pressed
                grabbedObject.transform.parent = null;
                grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
                grabbedObject.GetComponent<Rigidbody>().velocity = handVelocity;
                grabbedObject = null;
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
        Debug.Log("trigger");

        if (vrInputActions.Default.Primary.WasPerformedThisFrame())
        {
            Debug.Log("grabbed");
            //does something when pressed
            other.transform.parent = transform;
            grabbedObject = other.gameObject;
            other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}
