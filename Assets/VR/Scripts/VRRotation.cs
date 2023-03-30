using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRRotation : MonoBehaviour
{
    [SerializeField]
    Transform head;

    public bool rotate;
    public RotateDirection direction;

    public enum RotateDirection { Left, Right}

    private void Update()
    {
        if (rotate)
        {
            int newDirection = direction == RotateDirection.Left ? -1 : 1;

            Vector3 initialHeadPosition = head.position;

            transform.Rotate(0, 90 * newDirection, 0);

            Vector3 headTranslationOffset = initialHeadPosition - head.position;

            transform.position += headTranslationOffset;

            rotate = false;
        }
    }
}
