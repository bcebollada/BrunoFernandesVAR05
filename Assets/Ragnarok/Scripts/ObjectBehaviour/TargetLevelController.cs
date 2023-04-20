using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TargetLevelController : MonoBehaviour

{
    public GameObject[] targets;
    public GameObject[] pumpkinHeads;
    public GameObject restartCanvas;
    public GameObject restartPumpkin;
    public AmbienceSound ambienceSound;

    public int currentTargetIndex = 0;

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
        restartCanvas.SetActive(false);
        restartPumpkin.SetActive(false);
    }

    private void Update()
    {

        //// Level One Logic ////
        
        // Start Logic //

        if (gameStart == true && timerStart == false && levelOneComplete == false)
        {
            startGame();
            timerStart = true;
        }

        // End Logic //

        if (gameStart == true && timerStart == true && currentTargetIndex < 3 && levelOneComplete == false)
        {
            ambienceSound.playEpicSound = true;
            timeLeft -= Time.deltaTime;
            countdownText.text = "Time Remaining: " + Mathf.RoundToInt(timeLeft).ToString();
            if (timeLeft <= 0 && timeHasRunOut == false)
            {
                Debug.Log("Countdown timer has ended!");
                timeHasRunOut = true;
            }
        }

        // Gameplay Logic //
         
        if (currentTargetIndex < targets.Length && targets[currentTargetIndex].GetComponentInChildren<TargetController>().hasBeenHit == true 
            && targets[currentTargetIndex].GetComponentInChildren<TargetController>().axeHasBeenRecalled == true)
        {
            HitTarget();
        }

        else if(currentTargetIndex == 3 && levelOneComplete == false)
        {

            Debug.Log("You hit all the targets!");
            messageTextGO.SetActive(true);
            messageText.text = "Round 1 Complete! \nTake a drink from the mug \nto start round two!";
            levelOneComplete = true;
            timerStart = false;
            ambienceSound.playEpicSound = false;
        }

        //// Level Two Logic ////
        
        // Start Logic //

        if (levelTwoStart == true && timerStart == false && levelTwoComplete == false)
        {
            startLevelTwo();
            timerStart = true;
        }

        // End Logic //

        if (levelTwoStart == true && timerStart == true && currentPumpkinIndex < 3)
        {
            ambienceSound.playEpicSound = true;
            timeLeft -= Time.deltaTime;
            countdownText.text = "Time Remaining: " + Mathf.RoundToInt(timeLeft).ToString();
            if (timeLeft <= 0 && timeHasRunOut == false)
            {
                Debug.Log("Countdown timer has ended!");
                timeHasRunOut = true;
            }
        }

        // Gameplay Logic //

        if (currentPumpkinIndex < pumpkinHeads.Length && pumpkinHeads[currentPumpkinIndex].GetComponentInChildren<PumpkinController>().hasBeenHit == true)
        {
            HitPumpkin();
        }
        else if(currentPumpkinIndex == 3 && levelTwoComplete == false)
        {
            Debug.Log("You hit all the targets!");
            messageTextGO.SetActive(true);
            messageText.text = "Round 2 Complete! \nhit the pumpkin of restarting \n to reset";
            levelTwoComplete = true;
            timerStart = false;
            ambienceSound.playEpicSound = false;
        }

        //// Game Over Logic ////

        if (timeHasRunOut == true)
        {
            messageTextGO.SetActive(true);
            messageText.text = "Game Over! \nhit the pumpkin of restarting \n to reset";
            countdownText.text = "Time Remaining: 00";

            restartCanvas.SetActive(true);
            restartPumpkin.SetActive(true);
            for (int i = 0; i < pumpkinHeads.Length; i++)
            {
                pumpkinHeads[i].SetActive(false);
            }
            for (int j = 0; j < targets.Length; j++)
            {
                targets[j].SetActive(false);
            }
        }
    }

    //// Functions ////

    private void startGame()
    {
        ActivateTarget(currentTargetIndex);
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
        targets[index].SetActive(true);

        targets[currentTargetIndex].GetComponentInChildren<TargetController>().StartCoroutine
              (targets[currentTargetIndex].GetComponentInChildren<TargetController>().PlayUpAnimationAfterDelay(3));
    }

    private void HitTarget()
    {

        targets[currentTargetIndex].GetComponentInChildren<TargetController>().StartCoroutine
            (targets[currentTargetIndex].GetComponentInChildren<TargetController>().PlayDownAnimationAfterDelay(3f));

        currentTargetIndex++;

        if (currentTargetIndex < targets.Length)
        {
            ActivateTarget(currentTargetIndex);
        }

    }

}
