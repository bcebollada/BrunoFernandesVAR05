using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectatorCamera_Script : MonoBehaviour
{
    public Transform head;

    public float translateSpeed = 4, rotateSpeed = 4;

    private void LateUpdate()
    {

        Vector3 targetPosition = head.position;
        Quaternion targetRotation = head.rotation;

        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);
    }
}
