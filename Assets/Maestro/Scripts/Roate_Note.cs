using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roate_Note : MonoBehaviour
{
    public float rotationSpeed = 50f; // Adjust the speed of rotation
    public float bobbingSpeed = 0.5f; // Adjust the speed of bobbing
    public float bobbingHeight = 0.5f; // Adjust the height of bobbing

    private float originalY;
    private float timer = 0f;

    private void Start()
    {
        originalY = transform.position.y;
    }

    private void Update()
    {
        // Rotate the object around itself
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        // Bob the object up and down
        timer += bobbingSpeed * Time.deltaTime;
        float newY = originalY + Mathf.Sin(timer) * bobbingHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
