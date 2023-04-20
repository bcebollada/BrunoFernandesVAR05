using UnityEngine;

public class TriggerRespawnAxe : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Grabbable") && other.transform.parent.GetComponent<AxeController>() != null)
        {
            other.transform.parent.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            other.transform.parent.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

            other.transform.parent.transform.position = other.transform.GetComponentInParent<AxeController>().originalPos;
            other.transform.parent.transform.rotation = other.transform.GetComponentInParent<AxeController>().originalRot;

            Debug.Log("Respawn axe");
        }
    }
}
