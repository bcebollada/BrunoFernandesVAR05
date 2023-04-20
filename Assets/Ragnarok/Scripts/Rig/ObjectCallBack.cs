    using UnityEngine;

public class ObjectCallBack : MonoBehaviour
{
    // Create a layer mask that includes only layer X
    public int layerMask;
    public float capsuleCastRadius, recallDistance, recallSpeed; //values to define capsule cast
    public float sphereRadius;
    public bool shouldRecall;

    public Transform callBackPoint;
    public Transform vrCamera;
    public GrabIndicator grabIndicator; // added reference to grabIndicator game object

    private RagnarokVRInputActions actions;
    private GameObject recallObject;
    private bool isLeft; //to know which hand is
    private GrabVR grabVR;
    private bool objectHit;

    //for playing callback sound
    private AudioSource audioSource;
    public AudioClip callBackSound;

    private void Awake()
    {
        grabVR = GetComponent<GrabVR>();
        actions = new RagnarokVRInputActions();
        actions.Enable();

        layerMask   = 1 << LayerMask.NameToLayer("Grabbable");
    }

    private void Start()
    {
        if (GetComponent<GrabVR>().isLeftHand) isLeft = true;
    }

    private void MoveObjectTowardsHand()
    {
        recallObject.GetComponent<Rigidbody>().isKinematic = true;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, sphereRadius);
        
        foreach (var hitCollider in hitColliders)
        {
            
            Debug.Log(hitCollider.gameObject.name); // prints name of all overlapped collider by hand sphere ND


            if (hitCollider.gameObject.transform.tag == "recallObject") 
            {
                //creates same grab event as the in the GrabVR script
                Debug.Log("Axe Has Hit hand and should be grabbed");
                shouldRecall = false;
                GetComponent<GrabVR>().GrabObject(recallObject);

                recallObject = null; //clears pulled object
                break;
            }

            else if(hitCollider.gameObject.transform.parent != null) // check for the recallObject tag on parent ND
            {
                if(hitCollider.gameObject.transform.parent.gameObject.tag == "recallObject")
                {
                    //creates same grab event as the in the GrabVR script
                    Debug.Log("Axe Has Hit hand and should be grabbed");
                    shouldRecall = false;
                    GetComponent<GrabVR>().GrabObject(recallObject);

                    recallObject = null; //clears pulled object
                    break;
                }
            }
        }

        if (recallObject != null)
        {
            recallObject.transform.position = Vector3.Lerp(recallObject.transform.position, callBackPoint.position, recallSpeed * Time.deltaTime);

            if (recallObject.GetComponent<AudioSource>() != null) //if recallObj has audioSource, will play sound
            {
                if (audioSource == recallObject.GetComponent<AudioSource>()) return; //return ir sound was already played

                audioSource = recallObject.GetComponent<AudioSource>();
                audioSource.clip = callBackSound;
                audioSource.Play();
            }
            else Debug.Log("Recalled Obj does not have audio source, if you want to play sound, add it");
        }
    }

    void Update()
    {
        RaycastHit hitLeft;
        if (Physics.SphereCast(vrCamera.transform.position, capsuleCastRadius, vrCamera.forward, out hitLeft, recallDistance, layerMask) && recallObject == null)
        {
            if (hitLeft.collider.gameObject.GetComponentInParent<VRGrabbable>() != null)
            {
                objectHit = true; //raycast detected object with vrgrabbable

                Debug.Log("CapsuleCast hit grabbable object"); // check for if the capsule is hitting grabbable object ND

                if (hitLeft.collider.gameObject.GetComponentInParent<VRGrabbable>().grabPoint != null)
                {
                    //hitLeft.collider.gameObject.GetComponent<GrabIndicator>().isHovered = true; - this was throwing an error, changed how it checks for grab indicator by using direct reference ND
                    grabIndicator = hitLeft.collider.gameObject.GetComponentInParent<VRGrabbable>().grabPoint.gameObject.GetComponent<GrabIndicator>();
                    grabIndicator.isHovered = true;

                }


                if (shouldRecall)
                {
                    if (hitLeft.collider.gameObject.transform.parent != null)
                    {
                        // sets the "recallObject" to the parent object of the colliders ND
                        recallObject = hitLeft.collider.gameObject.transform.parent.gameObject;
                    }
                    else recallObject = hitLeft.collider.gameObject;

                    Debug.Log("Parent object 'The object to be recalled' name: " + recallObject.name);

                    recallObject.tag = "recallObject";

                    MoveObjectTowardsHand();
                }
            }
        }
        else objectHit = false;


        if (shouldRecall)
        {
            MoveObjectTowardsHand();
        }

        if (recallObject != null && !shouldRecall) //if we are pulling object but dont want to, removes kinematic
        {
            recallObject.GetComponentInParent<Rigidbody>().isKinematic = false;
            recallObject = null;

        }

        if(isLeft)
        {
            if (actions.Default.GripLeft.WasPerformedThisFrame() && recallObject == null)
            {
                Debug.Log("Left Grip was pressed in the CallBack Script without looking at Grabbable Object");

                // Check that stops it from entering error loop if trying to recall while looking at any other objects
                if (objectHit)
                {
                    Debug.Log("Grip was pressed and an object should be recalled");
                    shouldRecall = true;
                }
            }

            else if (actions.Default.GripLeft.WasReleasedThisFrame())
            {
                //recallObject = null;
                Debug.Log("Left grip released");

                shouldRecall = false;
                audioSource = null;
            }
        }
        else
        {
            if (actions.Default.GripRight.WasPerformedThisFrame() && recallObject == null)
            {
                Debug.Log("Right Grip was pressed in the CallBack Script without looking at Grabbable Object");
                
                // Check that stops it from entering error loop if trying to recall while looking at any other objects
                if(objectHit)
                {
                    Debug.Log("Grip was pressed and an object should be recalled");
                    shouldRecall = true;
                }
                    
            }
            else if (actions.Default.GripRight.WasReleasedThisFrame())
            {
                //recallObject = null;
                Debug.Log("Right grip released");

                shouldRecall = false;
                audioSource = null;

            }
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
        DrawWireCapsule(vrCamera.transform.position, vrCamera.transform.position + (vrCamera.forward * recallDistance), capsuleCastRadius);
        Gizmos.DrawWireSphere(transform.position, sphereRadius);
    }
}

