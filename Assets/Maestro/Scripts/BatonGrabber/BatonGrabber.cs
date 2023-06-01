using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatonGrabber : MonoBehaviour
{

    public GameObject batonPrefab; // Reference to the baton prefab
    private GameObject grabbedObject; // Reference to the currently grabbed object
    // Start is called before the first frame update

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Baton"))
        {
            ReplaceObjectWithBaton(other.gameObject);
        }
    }

    void ReplaceObjectWithBaton(GameObject objectToReplace)
    {
        // Create a new baton
        GameObject newBaton = Instantiate(batonPrefab, objectToReplace.transform.position, objectToReplace.transform.rotation);
        // Destroy the object that was replaced
        Destroy(objectToReplace);
        // Set the grabbed object to the new baton
        grabbedObject = newBaton;
    }

}
