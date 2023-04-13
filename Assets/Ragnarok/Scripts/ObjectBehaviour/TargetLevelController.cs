using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TargetLevelController : MonoBehaviour

{
    public GameObject[] targets; // Array of target objects
    
    public int currentTargetIndex = 0; // Index of the current target

    public bool levelOneComplete = false;

    public bool timeHasRunOut = false;

    public float totalTime = 60.0f;
    private float timeLeft;
    public TMP_Text countdownText;

    private void Start()
    {
        ActivateTarget(currentTargetIndex); // Activate the first target
        timeLeft = totalTime;
        countdownText.text = timeLeft.ToString();
    }

    private void Update()
    {
        timeLeft -= Time.deltaTime;
        countdownText.text = "Time Remaining: " + Mathf.RoundToInt(timeLeft).ToString();

        if (timeLeft <= 0 && timeHasRunOut == false)
        {
            // Do something when the countdown timer reaches 0
            Debug.Log("Countdown timer has ended!");
            timeHasRunOut = true;
        }

        if(timeHasRunOut == true)
        {
            countdownText.text = "Game Over";
        }

        if (currentTargetIndex < targets.Length && targets[currentTargetIndex].GetComponentInChildren<TargetController>().hasBeenHit == true 
            && targets[currentTargetIndex].GetComponentInChildren<TargetController>().axeHasBeenRecalled == true)
        {
            HitTarget(); // Call the HitTarget function
        }

        else if(currentTargetIndex == 3 && levelOneComplete == false)
        {

            Debug.Log("You hit all the targets!"); // Display a message when all targets are hit
            levelOneComplete = true;
        }


    }

    private void ActivateTarget(int index)
    {
        targets[index].SetActive(true); // Activate the target at the given index

        targets[currentTargetIndex].GetComponentInChildren<TargetController>().StartCoroutine
              (targets[currentTargetIndex].GetComponentInChildren<TargetController>().PlayUpAnimationAfterDelay(3));
    }

    private void HitTarget()
    {
        //    if(targets[currentTargetIndex].GetComponentInChildren<TargetController>().axeHasBeenRecalled == true)

        //    {
        targets[currentTargetIndex].GetComponentInChildren<TargetController>().StartCoroutine
            (targets[currentTargetIndex].GetComponentInChildren<TargetController>().PlayDownAnimationAfterDelay(3f));

        //        //targets[currentTargetIndex].SetActive(false); // Deactivate the current target

        currentTargetIndex++; // Move to the next target

        if (currentTargetIndex < targets.Length) // Check if there are more targets
        {
            ActivateTarget(currentTargetIndex); // Activate the next target
        }

        //    }
    }

    //private void Update()
    //{
    //    if (currentTargetIndex < targets.Length && targets[currentTargetIndex].GetComponentInChildren<TargetController>().hasBeenHit)
    //    {
    //        StartCoroutine(HitTarget());
    //    }
    //    else if (currentTargetIndex >= targets.Length)
    //    {
    //        Debug.Log("You hit all the targets!");
    //      // Enter the "You win" state here
    //    }
    //}

    //private IEnumerator HitTarget()
    //{
    //    TargetController currentTargetController = targets[currentTargetIndex].GetComponentInChildren<TargetController>();

    //    if (currentTargetController.axeHasBeenRecalled == true)
    //    {
    //        yield return currentTargetController.StartCoroutine(currentTargetController.PlayDownAnimationAfterDelay(3f)); // Wait for the "down" animation to finish playing

    //        currentTargetController.gameObject.SetActive(false); // Deactivate the current target

    //        currentTargetIndex++; // Move to the next target

    //        if (currentTargetIndex < targets.Length) // Check if there are more targets
    //        {
    //            ActivateTarget(currentTargetIndex); // Activate the next target
    //        }
    //        else
    //        {
    //            Debug.Log("Level One Complete"); // Display a message when all targets are hit 
    //                                                   // Enter the "You win" state here

    //            levelOneComplete = true;
    //        }
    //    }
    //}

}
