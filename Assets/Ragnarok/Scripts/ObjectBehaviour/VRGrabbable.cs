using UnityEngine;

public class VRGrabbable : MonoBehaviour
{
    public Transform grabPoint;
    public Quaternion specificRotation;

    public float throwForceMultiplier;
    public bool hasSpecificRotationOnGrab;

    private void Awake()
    {
        if (throwForceMultiplier == 0) throwForceMultiplier = 1;
    }
}
