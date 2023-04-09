using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.XR.Oculus.Input;
using UnityEngine.InputSystem.XR;

public class GrabVR : MonoBehaviour
{
    VRInputActions vrInputActions;
    public GameObject grabbedObject;

    private Vector3 previousPosition;
    private Vector3 handVelocity;
    private Quaternion previousRotation;
    private Vector3 angularVelocity;


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
        if (!debuggingMode)
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
                grabbedObject.GetComponent<Rigidbody>().angularVelocity = angularVelocity;
                grabbedObject.GetComponent<Rigidbody>().velocity = handVelocity * grabbedObject.GetComponent<VRGrabbable>().throwForceMultiplier;
                grabbedObject.GetComponent<Rigidbody>().angularVelocity = angularVelocity * grabbedObject.GetComponent<VRGrabbable>().throwForceMultiplier;
                print(angularVelocity);
                grabbedObject = null;

                hasObjectGrabbed = false;

            }
        }
    }

    private void FixedUpdate()
    {
        handVelocity = (transform.position - previousPosition) / Time.deltaTime; //gets velocity per second
        previousPosition = transform.position;

        if(grabbedObject)
        {
            //calculate angular velocity so object has it when released
            // "Subtract" the current rotation from the previous rotation.
            Quaternion delta = grabbedObject.transform.rotation * Quaternion.Inverse(previousRotation);

            // Convert it to an "angle axis", basically a direction and how rotated it is around that direction.
            delta.ToAngleAxis(out float angle, out Vector3 axis);

            // Lastly, convert it into radians per second.
            angularVelocity = (Mathf.Deg2Rad * angle / Time.fixedDeltaTime) * axis.normalized;

            previousRotation = grabbedObject.transform.rotation;
        }
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponentInParent<VRGrabbable>() != null && !hasObjectGrabbed)
        {
            if (hasGripped)
            {
                GrabObject(other.gameObject.transform.parent.gameObject);
            }
        }
    }
    
    public void GrabObject(GameObject grabbable)
    {
        var vrGrabbable = grabbable.GetComponent<VRGrabbable>();
        grabbable.transform.parent = transform;
        grabbedObject = grabbable.gameObject;
        grabbable.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        hasObjectGrabbed = true;

        if (vrGrabbable.hasSpecificRotationOnGrab)
        {
            grabbable.transform.localRotation = vrGrabbable.specificRotation;
        }
            
        if (vrGrabbable.grabPoint != null)
        {
            // Calculate the offset between the grab point position and the controller position
            //grabbable.transform.position = transform.position;
            Vector3 offset = vrGrabbable.grabPoint.position - grabbable.transform.position;

            // Set the position of the grabbed object relative to the offset
            grabbable.transform.position = GetComponent<ObjectCallBack>().callBackPoint.position - offset;
            Debug.DrawLine(vrGrabbable.grabPoint.position, vrGrabbable.grabPoint.position + offset, Color.red, 100f);
            Debug.Log("Grab offset is" + offset);
           
        }


    }
}
