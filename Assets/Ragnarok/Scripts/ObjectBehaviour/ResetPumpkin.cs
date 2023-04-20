using UnityEngine;

public class ResetPumpkin : MonoBehaviour
{
    public bool resetPumpkinHasBeenHit = false;

    private void OnCollisionEnter(Collision collision)
    {
        resetPumpkinHasBeenHit = true;
    }
}
