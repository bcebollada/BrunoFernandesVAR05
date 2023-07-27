using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanShaderCommunicator : MonoBehaviour
{
    public float partialView;
    public Renderer objectRenderer;

    void Start()
    {
        // Check if the object has a renderer component
        if (objectRenderer == null)
        {
            objectRenderer = GetComponent<Renderer>();
        }

        // Check if the object has a material with the "PartialView" shader
        if (objectRenderer != null && objectRenderer.sharedMaterial.shader.name == "Custom/PartialView")
        {
            // Calculate the object height based on the transform scale
            Vector3 objectScale = transform.lossyScale;
            float objectHeight = GetComponent<MeshRenderer>().bounds.size.y*2;

            for (int i = 0; i < objectRenderer.materials.Length; i++)
            {
                // Set the object position and height in the shader

                objectRenderer.materials[i].SetVector("_ObjectPos", transform.position);
                objectRenderer.materials[i].SetFloat("_PartialView", partialView);
                objectRenderer.materials[i].SetVector("_ObjectHeight", new Vector3(0, objectHeight, 0));
            }


        }
        else
        {
            Debug.LogWarning("The object does not have a material with the 'PartialView' shader.");
        }
    }

    void OnDestroy()
    {
        // Destroy the unique material instance to clean up memory
        if (objectRenderer != null)
        {
            Destroy(objectRenderer.material);
        }
    }

    void Update()
    {
        for (int i = 0; i < objectRenderer.materials.Length; i++)
        {
            objectRenderer.materials[i].SetFloat("_PartialView", partialView);

        }
    }


}
