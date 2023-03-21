using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class VRRIg : MonoBehaviour
{
    public Transform leftHand;
    public Transform rightHand;
    public Transform head;

    void Update()
    {
        // Update left and right hand transforms to XR controller positions
        leftHand.position = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand).TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 leftPosition) ? leftPosition : Vector3.zero;
        leftHand.rotation = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand).TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion leftRotation) ? leftRotation : Quaternion.identity;
        rightHand.position = InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 rightPosition) ? rightPosition : Vector3.zero;
        rightHand.rotation = InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion rightRotation) ? rightRotation : Quaternion.identity;

        // Update head transform to XR headset position
        head.position = InputDevices.GetDeviceAtXRNode(XRNode.Head).TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 headPosition) ? headPosition : Vector3.zero;
        head.rotation = InputDevices.GetDeviceAtXRNode(XRNode.Head).TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion headRotation) ? headRotation : Quaternion.identity;
    }
}
