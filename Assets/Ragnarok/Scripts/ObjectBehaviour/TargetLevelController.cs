using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TargetLevelController : MonoBehaviour

{
    public GameObject[] targets; // Array of target objects
    public GameObject[] pumpkinHeads; // Array of pumpkinHead objects
    public AmbienceSound ambienceSound;

    public int currentTargetIndex = 0; // Index of the current target

    public int currentPumpkinIndex = 0;

    public bool gameStart = false;
    public bool levelTwoStart = false;
    public bool timerStart = false;


    public bool levelOneComplete = false;
    public bool levelTwoComplete = false;

    public bool timeHasRunOut = false;

    public float totalTime = 60.0f;
    private float timeLeft;
    public TMP_Text countdownText;
    public TMP_Text messageText;
    public GameObject messageTextGO;

    private void Start()
    {
        messageText.text = "Welcome to Ragnarok! \nTake a drink from the mug \nto start the game!";
    }

    private void startGame()
    {
        ActivateTarget(currentTargetIndex); // Activate the first target
        timeLeft = totalTime;
        countdownText.text = timeLeft.ToString();
        messageTextGO.SetActive(false);
    }
    private void startLevelTwo()
    {
        ActivateScareCrowTarget(currentPumpkinIndex);
        timeLeft = 60.0f;
        countdownText.text = timeLeft.ToString();
        messageTextGO.SetActive(false);
    }

    private void Update()
    {
        if(gameStart == true && timerStart == false && levelOneComplete == false)
        {
            startGame();
            timerStart = true;
        }

        if (gameStart == true && timerStart == true && currentTargetIndex < 3 && levelOneComplete == false)
        {
            ambienceSound.playEpicSound = true;
            timeLeft -= Time.deltaTime;
            countdownText.text = "Time Remaining: " + Mathf.RoundToInt(timeLeft).ToString();
            if (timeLeft <= 0 && timeHasRunOut == false)
            {
                // Do something when the countdown timer reaches 0
                Debug.Log("Countdown timer has ended!");
                timeHasRunOut = true;
            }
        }

        if(levelTwoStart == true && timerStart == false && levelTwoComplete == false)
        {
            startLevelTwo();
            timerStart = true;
        }

        if(levelTwoStart == true && timerStart == true && currentPumpkinIndex < 3)
        {
            ambienceSound.playEpicSound = true;
            timeLeft -= Time.deltaTime;
            countdownText.text = "Time Remaining: " + Mathf.RoundToInt(timeLeft).ToString();
            if (timeLeft <= 0 && timeHasRunOut == false)
            {
                // Do something when the countdown timer reaches 0
                Debug.Log("Countdown timer has ended!");
                timeHasRunOut = true;
            }
        }

        if (timeHasRunOut == true)
        {
            messageText.text = "Game Over";
        }

        //// Level One Logic ////
         
        if (currentTargetIndex < targets.Length && targets[currentTargetIndex].GetComponentInChildren<TargetController>().hasBeenHit == true 
            && targets[currentTargetIndex].GetComponentInChildren<TargetController>().axeHasBeenRecalled == true)
        {
            HitTarget(); // Call the HitTarget function
        }

        else if(currentTargetIndex == 3 && levelOneComplete == false)
        {

            Debug.Log("You hit all the targets!"); // Display a message when all targets are hit
            messageTextGO.SetActive(true);
            messageText.text = "Round 1 Complete! \nTake a drink from the mug \nto start round two!";
            levelOneComplete = true;
            timerStart = false;
            ambienceSound.playEpicSound = false;
        }

        //// Level Two Logic ////

        if (currentPumpkinIndex < pumpkinHeads.Length && pumpkinHeads[currentPumpkinIndex].GetComponentInChildren<PumpkinController>().hasBeenHit == true)
        {
            HitPumpkin();
        }
        else if(currentPumpkinIndex == 3 && levelTwoComplete == false)
        {
            Debug.Log("You hit all the targets!"); // Display a message when all targets are hit
            messageTextGO.SetActive(true);
            messageText.text = "Round 2 Complete! \nTake a drink from the mug \nto start round two!";
            levelTwoComplete = true;
            timerStart = false;
            ambienceSound.playEpicSound = false;
        }
    }

    private void HitPumpkin()
    {
        currentPumpkinIndex++;

        if(currentPumpkinIndex < pumpkinHeads.Length)
        {
            Debug.Log("activating scarecrow");
            ActivateScareCrowTarget(currentPumpkinIndex);
        }
    }

    private void ActivateScareCrowTarget(int index)
    {
        pumpkinHeads[index].SetActive(true);
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
