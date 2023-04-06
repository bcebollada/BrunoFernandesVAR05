using UnityEngine;
using UnityEngine.InputSystem;

public class ZoomOut : MonoBehaviour
{
    RagnarokVRInputActions vrActions;
    private bool isZoomedOut = false;

    [SerializeField]
    Camera vrRig;
    [SerializeField]
    Camera skyCam;

    private void Awake()
    {
        vrActions = new RagnarokVRInputActions();
        vrActions.Enable();
    }

    private void Start()
    {
        vrRig.enabled = true;
        skyCam.enabled = false;
    }

    void Update()
    {
        if (vrActions.Default.ZoomOutYButton.WasPressedThisFrame() || Keyboard.current.lKey.wasPressedThisFrame)
        {
            if (isZoomedOut == false)
            {
                // go to sky view
                vrRig.enabled = false;
                skyCam.enabled = true;

                isZoomedOut = true;
            }
            else if (isZoomedOut == true)
            {
                // go back to field view
                vrRig.enabled = true;
                skyCam.enabled = false;

                isZoomedOut = false;
            }
        }
    }
}
