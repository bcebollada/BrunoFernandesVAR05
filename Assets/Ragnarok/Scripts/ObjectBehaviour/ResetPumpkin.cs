using UnityEngine;

public class ResetPumpkin : MonoBehaviour
{
    public bool resetPumpkinHasBeenHit = false;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Grabbable")) resetPumpkinHasBeenHit = true;
    }
}
