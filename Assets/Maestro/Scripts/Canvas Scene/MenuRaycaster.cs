using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class MenuRaycaster : MonoBehaviour
{
    public float raycastDistance = 10f;
    public LayerMask raycastLayer;
    public Color lineColor = Color.white;
    public float lineThickness = 0.02f;
    public Vector3 forwardVector = Vector3.forward;

    private LineRenderer lineRenderer;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = true;
        lineRenderer.startWidth = lineThickness;
        lineRenderer.material.color = lineColor;
    }

    void Update()
    {
        Vector3 localForwardVector = transform.TransformDirection(forwardVector);
        Ray ray = new Ray(transform.position, localForwardVector);
        RaycastHit hit;
        bool hitSomething = Physics.Raycast(ray, out hit, raycastDistance, raycastLayer);

        lineRenderer.enabled = true;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, transform.position);

        if (hitSomething)
        {
            lineRenderer.SetPosition(1, hit.point);
            if (hit.collider.gameObject.GetComponent<Button>())
            {
                EventSystem.current.SetSelectedGameObject(hit.collider.gameObject);
            }
        }
        else
        {
            lineRenderer.SetPosition(1, transform.position + localForwardVector * raycastDistance);
            EventSystem.current.SetSelectedGameObject(null);
        }
    }
    public bool CheckRayCast()
    {
        // Cast a ray forward from the controller and check if it hits a button
        Vector3 localForwardVector = transform.TransformDirection(forwardVector);
        Ray ray = new Ray (transform.position, localForwardVector);
        RaycastHit hit;
        bool HitButton = Physics.Raycast(ray, out hit, raycastDistance, raycastLayer);
        return HitButton;
    }

    public void ClearLineRenderer()
    {
        lineRenderer.positionCount = 0;
    }
}
