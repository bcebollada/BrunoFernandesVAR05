using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRRotation : MonoBehaviour
{
    [SerializeField]
    Transform head;

    public bool rotate;
    public bool debugging; 
    public RotateDirection direction;

    private RagnarokVRInputController input;
    private float rotationVelocity;

    public enum RotateDirection { Left, Right}

    private void Awake()
    {
        input = GetComponent<RagnarokVRInputController>();
    }

    private void Update()
    {
        if (debugging) //debug mode
        {
            if(rotate)
            {
                int newDirection = direction == RotateDirection.Left ? -1 : 1;

                Vector3 initialHeadPosition = head.position;

                transform.Rotate(0, 90 * newDirection, 0);

                Vector3 headTranslationOffset = initialHeadPosition - head.position;

                transform.position += headTranslationOffset;

                rotate = false;
            }
        }
        else //using HMD
        {
            rotationVelocity = input.JoystickRight.x;

            Vector3 initialHeadPosition = head.position;

            transform.Rotate(0, 90 * rotationVelocity* Time.deltaTime, 0);

            Vector3 headTranslationOffset = initialHeadPosition - head.position;

            transform.position += headTranslationOffset;
        }
        
    }
}
