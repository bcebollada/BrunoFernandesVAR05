using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.InputSystem;

public class MenuMain_Script : MonoBehaviour
{
    private VRInputController input;
    private VRInputActions actions;

    public GameObject menuPrefab;
    private GameObject activeMenu;
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
            activeMenu = Instantiate(menuPrefab, menuSpawner.position, menuSpawner.rotation);
            isMenuActive = true;
        }

        if(actions.Default.Primary.WasReleasedThisFrame())
        {
            if (isMenuActive)
            {
                Destroy(activeMenu);
                isMenuActive = false;
            }
        }

        if(isMenuActive)
        {
            activeMenu.transform.position = menuSpawner.position;
            activeMenu.transform.rotation = menuSpawner.rotation;
        }
    }
}
