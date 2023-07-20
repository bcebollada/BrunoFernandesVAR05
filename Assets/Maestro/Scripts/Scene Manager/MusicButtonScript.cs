using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;

public class MusicButtonScript : MonoBehaviour
{
    public ChangeScene sceneManager;
    public MenuRaycaster menuRaycaster; // Reference to the MenuRaycaster script

    public float countdownDuration = 3f; // Duration of the countdown in seconds
    public TMP_Text countdownText; // Text component to display the countdown
    public Button Level_One_Button;
    public Button Level_Two_Button;

    private bool isHoveringButton; // Flag to track if the raycast is hovering over a button
    private bool countdownStarted; // Flag to track if the countdown has started
    private float countdownTimer; // Timer for the countdown

    private void Update()
    {
        // Check if the raycast is hovering over a button
        bool isHovering = menuRaycaster.CheckRayCast() && EventSystem.current.currentSelectedGameObject == Level_One_Button.gameObject;

        // Start or reset the countdown if the hover state changes
        if (isHovering != isHoveringButton)
        {
            if (isHovering)
            {
                Debug.Log("Raycast is hovering over a button");
                StartCountdown();
            }
            else
            {
                ResetCountdown();
            }
        }

        // Update the hover state
        isHoveringButton = isHovering;

        // Update the countdown timer if it's running
        if (countdownStarted)
        {
            countdownTimer -= Time.deltaTime;

            // Update the countdown text
            int countdownValue = Mathf.CeilToInt(countdownTimer);
            countdownText.text = countdownValue.ToString();

            // Check if the countdown reaches 0
            if (countdownTimer <= 0f)
            {
                LoadSongLevel();
            }
        }
    }

    private void StartCountdown()
    {
        // Start the countdown
        countdownStarted = true;
        countdownTimer = countdownDuration;

        // Show the countdown text
        countdownText.gameObject.SetActive(true);
    }

    private void ResetCountdown()
    {
        // Reset the countdown
        countdownStarted = false;
        countdownTimer = 0f;

        // Hide the countdown text
        countdownText.gameObject.SetActive(false);
    }
    private void Awake()
    {
        // Find and assign the SceneManager script in the scene
        sceneManager = FindObjectOfType<ChangeScene>();
    }

    public void LoadSongLevel()
    {
        // Call the ChangeScene method of the SceneManager
        sceneManager.New_Level("Bruno Test");
    }
}
