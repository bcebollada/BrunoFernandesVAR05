using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeController : MonoBehaviour
{
    private Rigidbody rb;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("Axe hit object)");
            rb.isKinematic = true;
            //transform.SetParent(other.transform);
            //transform.localRotation = Quaternion.LookRotation(other.contacts[0].normal);
            // add particle effects or sound effects here
        
    }
}
