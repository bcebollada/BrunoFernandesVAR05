using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.XR.Oculus.Input;
using UnityEngine.InputSystem.XR;

public class RefinedGrab : MonoBehaviour
{
    private Rigidbody heldObject;

    private VRInputController input;

    private void Awake()
    {
        input = GetComponent<VRInputController>();
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
