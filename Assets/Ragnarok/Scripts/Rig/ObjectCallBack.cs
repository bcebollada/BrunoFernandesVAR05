    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEditor;

public class ObjectCallBack : MonoBehaviour
{
    private GrabVR grabVR;

    public float capsuleCastRadius, recallDistance, recallSpeed; //values to define capsule cast
    public bool shouldRecall;

    private RagnarokVRInputController input;

    private GameObject recallObject;

    private void Awake()
    {
        grabVR = GetComponent<GrabVR>();
        input = GetComponent<RagnarokVRInputController>();
    }


    // Update is called once per frame
    void Update()
    {
        RaycastHit hitLeft;
        if (Physics.SphereCast(transform.position, capsuleCastRadius, -transform.up, out hitLeft, recallDistance))
        {
            Debug.Log(hitLeft.collider.gameObject.name);

            if (hitLeft.collider.gameObject.GetComponent<GrabbableObject>() != null)
            {
                Debug.Log("CapsuleCast hit grabbable object");

                if (shouldRecall)
                {
                    recallObject = hitLeft.collider.gameObject;
                    recallObject.GetComponent<Rigidbody>().isKinematic = true;
                    recallObject.transform.position = Vector3.MoveTowards(recallObject.transform.position, transform.position, recallSpeed * Time.deltaTime);
                }
            }
        }

        if(recallObject != null && !shouldRecall) //if we are pulling object but dont want to, removes kinematic
        {
            recallObject.GetComponent<Rigidbody>().isKinematic = false;

        }

    }


    //function to visualize capsuleDraw
    public static void DrawWireCapsule(Vector3 p1, Vector3 p2, float radius)
    {
#if UNITY_EDITOR
        // Special case when both points are in the same position
        if (p1 == p2)
        {
            // DrawWireSphere works only in gizmo methods
            Gizmos.DrawWireSphere(p1, radius);
            return;
        }
        using (new UnityEditor.Handles.DrawingScope(Gizmos.color, Gizmos.matrix))
        {
            Quaternion p1Rotation = Quaternion.LookRotation(p1 - p2);
            Quaternion p2Rotation = Quaternion.LookRotation(p2 - p1);
            // Check if capsule direction is collinear to Vector.up
            float c = Vector3.Dot((p1 - p2).normalized, Vector3.up);
            if (c == 1f || c == -1f)
            {
                // Fix rotation
                p2Rotation = Quaternion.Euler(p2Rotation.eulerAngles.x, p2Rotation.eulerAngles.y + 180f, p2Rotation.eulerAngles.z);
            }
            // First side
            UnityEditor.Handles.DrawWireArc(p1, p1Rotation * Vector3.left, p1Rotation * Vector3.down, 180f, radius);
            UnityEditor.Handles.DrawWireArc(p1, p1Rotation * Vector3.up, p1Rotation * Vector3.left, 180f, radius);
            UnityEditor.Handles.DrawWireDisc(p1, (p2 - p1).normalized, radius);
            // Second side
            UnityEditor.Handles.DrawWireArc(p2, p2Rotation * Vector3.left, p2Rotation * Vector3.down, 180f, radius);
            UnityEditor.Handles.DrawWireArc(p2, p2Rotation * Vector3.up, p2Rotation * Vector3.left, 180f, radius);
            UnityEditor.Handles.DrawWireDisc(p2, (p1 - p2).normalized, radius);
            // Lines
            UnityEditor.Handles.DrawLine(p1 + p1Rotation * Vector3.down * radius, p2 + p2Rotation * Vector3.down * radius);
            UnityEditor.Handles.DrawLine(p1 + p1Rotation * Vector3.left * radius, p2 + p2Rotation * Vector3.right * radius);
            UnityEditor.Handles.DrawLine(p1 + p1Rotation * Vector3.up * radius, p2 + p2Rotation * Vector3.up * radius);
            UnityEditor.Handles.DrawLine(p1 + p1Rotation * Vector3.right * radius, p2 + p2Rotation * Vector3.left * radius);
        }
#endif
    }

    private void OnDrawGizmos()
    {
        DrawWireCapsule(transform.position, transform.position + (-transform.up * recallDistance), capsuleCastRadius);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == recallObject) //checks if object is the pulled one
        {
            //creates same grab event as the in the GrabVR script
            other.transform.parent = transform;
            grabVR.grabbedObject = other.gameObject;
            other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            grabVR.hasObjectGrabbed = true;


            shouldRecall = false;
            recallObject = null; //clears pulled object
        }
    }
}

