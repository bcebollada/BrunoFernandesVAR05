using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class VRRig : MonoBehaviour
{
    public Transform head, left, right;

    public Vector3 lastPosition;
    private static VRRig instance;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        transform.position = lastPosition;

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        lastPosition = transform.position;


        if (XRController.leftHand != null)
        {
            // Update the transforms of the components of our VR rig,
            // i.e., the head and hands
            // "i.e." just means "in other words"
            Vector3 leftPosition = transform.position + XRController.leftHand.devicePosition.ReadValue();
            Quaternion leftRotation = XRController.leftHand.deviceRotation.ReadValue();

            left.SetPositionAndRotation(leftPosition, leftRotation);
        }

        if (XRController.rightHand != null)
        {
            Vector3 rightPosition = transform.position + XRController.rightHand.devicePosition.ReadValue();
            Quaternion rightRotation = XRController.rightHand.deviceRotation.ReadValue();

            right.SetPositionAndRotation(rightPosition, rightRotation);
        }

        XRHMD hmd = InputSystem.GetDevice<XRHMD>();

        if (hmd != null)
        {
            Vector3 headPosition = transform.position + hmd.devicePosition.ReadValue();
            Quaternion headRotation = hmd.deviceRotation.ReadValue();

            head.SetPositionAndRotation(headPosition, headRotation);
        }
    }
}