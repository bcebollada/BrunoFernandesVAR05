using UnityEngine;

public class PumpkinController : MonoBehaviour
{
    public bool hasBeenHit = false;

    private void OnCollisionEnter(Collision collision)
    {
        hasBeenHit = true;
    }
}