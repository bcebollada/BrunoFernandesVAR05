using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableObject : MonoBehaviour
{
    private Material defaultMaterial;
    public Material highLightMat;

    public bool isHovered;
    public bool isGrabbed;

    private float timeOutOfHover;

    private void Awake()
    {
        defaultMaterial = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        if (isHovered)
        {
            if (GetComponent<Renderer>().sharedMaterial.name == defaultMaterial.name)
            {
                GetComponent<Renderer>().material = highLightMat;
                highLightMat = GetComponent<Renderer>().material;
                timeOutOfHover = 0;
            }

        }
        else if (timeOutOfHover > 0.2f)
        {
            if(GetComponent<Renderer>().sharedMaterial.name == highLightMat.name) GetComponent<Renderer>().material = defaultMaterial;
        }

        timeOutOfHover += Time.deltaTime;
        isHovered = false;

        
    }
}
