using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.InputSystem;

public class MenuMain_Script : MonoBehaviour
{
    private VRInputController input;
    private VRInputActions actions;

    public GameObject menu;
    public Transform menuSpawner;

    private bool isMenuActive;

    private void Awake()
    {
        input = GetComponent<VRInputController>();
        actions = new VRInputActions();
        actions.Enable();
    }

 

    // Update is called once per frame
    void Update()
    {
        if(actions.Default.Primary.WasPressedThisFrame())
        {
            menu.SetActive(true);
            menu.GetComponent<RubeGoldBergController_Script>().GetMats();
            isMenuActive = true;
        }

        if(actions.Default.Primary.WasReleasedThisFrame())
        {
            if (isMenuActive)
            {
                menu.SetActive(false);
                isMenuActive = false;
            }
        }

        if(isMenuActive)
        {
            menu.transform.position = menuSpawner.position;
            menu.transform.rotation = menuSpawner.rotation;
        }
    }
}
