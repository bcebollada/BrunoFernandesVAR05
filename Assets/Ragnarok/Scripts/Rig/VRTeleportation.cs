using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRTeleportation : MonoBehaviour
{
    public Transform head;
    public Transform hand;
    public GameObject teleportTargetMarkPrefab;
    private GameObject teleportTargetMark;

    public bool Teleport;
    private bool isTryingToTeleport;
    public Vector3 TeleportTarget;

    private RagnarokVRInputController input;

    private void Awake()
    {
        input = GetComponent<RagnarokVRInputController>();
    }

    private void Update()
    {
        if (Physics.Raycast(hand.position, -hand.up, out RaycastHit hit))
        {
            Debug.DrawLine(hand.position, hit.point, Color.green);
            TeleportTarget = hit.point;
        }

        Vector3 directionToHead = transform.position - head.position;

        directionToHead.y = 0;

        // If the user presses the trigger? If they do *something*.
        // They point the controller somewhere, getting a
        // location they want to move to.
        if (input.ThumbPressedLeft || Teleport)
        {
            isTryingToTeleport = true;
            // Teleport the...rig? To the target position.
      
        }

        if(isTryingToTeleport) //if is holding thumbsticks shows target on ground
        {
            if (teleportTargetMark == null)
            {
                teleportTargetMark = Instantiate(teleportTargetMarkPrefab, new Vector3(TeleportTarget.x, TeleportTarget.y, TeleportTarget.z), Quaternion.identity);
            }
            else teleportTargetMark.transform.position = TeleportTarget;

            if (!input.ThumbPressedLeft) //when realese thumbstick destroys target and moves
            {
                Destroy(teleportTargetMark);
                transform.position = TeleportTarget + directionToHead;
                isTryingToTeleport = false;
                Teleport = false;
            }
        }

        Debug.DrawLine(transform.position, head.position, Color.cyan, 0);
        Debug.DrawLine(transform.position, TeleportTarget, Color.yellow, 0);

        Debug.DrawRay(TeleportTarget, directionToHead, Color.red, 0);
    }
}
