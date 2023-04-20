using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerRespawnAxe : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Grabbable") && other.transform.parent.GetComponent<AxeController>() != null)
        {
            other.transform.parent.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            other.transform.parent.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

            other.transform.parent.transform.position = other.transform.GetComponentInParent<AxeController>().originalPos;
            other.transform.parent.transform.rotation = other.transform.GetComponentInParent<AxeController>().originalRot;

            Debug.Log("Respawn axe");
        }
    }
}
