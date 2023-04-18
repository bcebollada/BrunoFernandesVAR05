using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeController : MonoBehaviour
{
    public Rigidbody rigidBody;
    public Collider bladeCollider;
    public Collider handleCollider;

    public bool isStuck = false;

    public Vector3 originalPos;
    public Quaternion originalRot;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();

        //saves original transform 
        originalPos = transform.position;
        originalRot = transform.rotation;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (isStuck) return;

        ContactPoint contact = collision.contacts[0];

        if (contact.thisCollider == bladeCollider && contact.otherCollider.CompareTag("Target"))
        {
            Debug.Log("Blade was hit");
            rigidBody.isKinematic = true;
            //isStuck = true;
        }
        else if (contact.thisCollider == handleCollider && contact.otherCollider.CompareTag("Target"))
        {
            Debug.Log("Handle was hit");
            //rigidBody.AddForce(contact.normal * 500f);
        }
    }
}
