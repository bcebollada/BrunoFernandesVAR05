using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    [SerializeField]
    GameObject axePrefab;

    void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.tag == "AxeBlade")
        {
            Debug.Log("Axe hit object)");
            axePrefab.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}
