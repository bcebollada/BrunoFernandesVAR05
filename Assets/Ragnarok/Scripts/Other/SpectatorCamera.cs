using UnityEngine;

public class SpectatorCamera : MonoBehaviour
{

    public Transform target;
    public float translateSpeed = 6f;
    public float rotateSpeed = 8f;


    //CHANGE SPECTATOR CAMERA FIELD OF VIEW TO 90

    //so that all the logic has finished before executing
    private void LateUpdate()
    {
        Vector3 targetPosition = target.position;
        Quaternion targetRotation = target.rotation;

        transform.position = Vector3.Lerp(transform.position, targetPosition, translateSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
    }
}
