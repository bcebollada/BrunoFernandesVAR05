using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasInteraction : MonoBehaviour
{
    public GameObject welcomeCanvas; // Reference to the welcome canvas
    public GameObject musicSelectionCanvas; // Reference to the music selection canvas
    public float canvasMoveOffset = 1f; // The amount to move the canvas up (adjust as needed)

    private bool canvasMoved; // Flag to track if the canvas has already moved

    public TextMeshProUGUI[] textsToDeactivate; // Array of TextMeshPro objects to deactivate on touch
    public TextMeshProUGUI[] textsToActivate; // Array of TextMeshPro objects to activate on touch

    private Vector3 welcomeCanvasStartPosition; // Starting position of the welcome canvas

    private void Start()
    {
        welcomeCanvasStartPosition = welcomeCanvas.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!canvasMoved && other.gameObject.CompareTag("Baton"))
        {
            MoveCanvasUp();
            canvasMoved = true;
        }
    }

    private void MoveCanvasUp()
    {
        // Move the welcome canvas up
        welcomeCanvas.transform.position = welcomeCanvasStartPosition + Vector3.up * canvasMoveOffset;

        // Activate the music selection canvas
        musicSelectionCanvas.SetActive(true);

        // Deactivate specified TextMeshPro objects
        foreach (TextMeshProUGUI textToDeactivate in textsToDeactivate)
        {
            textToDeactivate.gameObject.SetActive(false);
        }

        // Activate specified TextMeshPro objects
        foreach (TextMeshProUGUI textToActivate in textsToActivate)
        {
            textToActivate.gameObject.SetActive(true);
        }
    }
}
