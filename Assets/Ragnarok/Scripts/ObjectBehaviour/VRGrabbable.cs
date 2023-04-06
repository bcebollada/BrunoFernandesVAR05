using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRGrabbable : MonoBehaviour
{
    public Transform grabPoint;
    
    public bool hasSpecificRotationOnGrab;
    public Quaternion specificRotation;

    public float throwForceMultiplier; 


    private void Awake()
    {
        if (throwForceMultiplier == 0) throwForceMultiplier = 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
