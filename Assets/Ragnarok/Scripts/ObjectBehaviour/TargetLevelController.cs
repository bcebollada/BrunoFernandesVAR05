using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLevelController : MonoBehaviour

{
    public GameObject[] targets; // Array of target objects
    
    private int currentTargetIndex = 0; // Index of the current target

    public bool levelOneComplete = false;

    private void Start()
    {
        ActivateTarget(currentTargetIndex); // Activate the first target
    }

    //private void Update()
    //{
    //    if (currentTargetIndex < targets.Length && targets[currentTargetIndex].GetComponentInChildren<TargetController>().hasBeenHit == true)
    //    {
    //        HitTarget(); // Call the HitTarget function
    //    }

    //    else if(currentTargetIndex == targets.Length -1)
    //    {

    //        Debug.Log("You hit all the targets!"); // Display a message when all targets are hit 
    //    }


    //}

    private void Update()
    {
        if (currentTargetIndex < targets.Length && targets[currentTargetIndex].GetComponentInChildren<TargetController>().hasBeenHit)
        {
            StartCoroutine(HitTarget());
        }
        else if (currentTargetIndex >= targets.Length)
        {
            Debug.Log("You hit all the targets!");
          // Enter the "You win" state here
        }
    }

    //private void HitTarget()
    //{
    //    if(targets[currentTargetIndex].GetComponentInChildren<TargetController>().axeHasBeenRecalled == true)
    //    {
    //        targets[currentTargetIndex].GetComponentInChildren<TargetController>().StartCoroutine
    //            (targets[currentTargetIndex].GetComponentInChildren<TargetController>().PlayDownAnimationAfterDelay(3f));

    //        //targets[currentTargetIndex].SetActive(false); // Deactivate the current target

    //        currentTargetIndex++; // Move to the next target

    //        if (currentTargetIndex < targets.Length) // Check if there are more targets
    //        {
    //            ActivateTarget(currentTargetIndex); // Activate the next target
    //        }

    //    }

    //}
    private IEnumerator HitTarget()
    {
        TargetController currentTargetController = targets[currentTargetIndex].GetComponentInChildren<TargetController>();

        if (currentTargetController.axeHasBeenRecalled == true)
        {
            yield return currentTargetController.StartCoroutine(currentTargetController.PlayDownAnimationAfterDelay(3f)); // Wait for the "down" animation to finish playing

            currentTargetController.gameObject.SetActive(false); // Deactivate the current target

            currentTargetIndex++; // Move to the next target

            if (currentTargetIndex < targets.Length) // Check if there are more targets
            {
                ActivateTarget(currentTargetIndex); // Activate the next target
            }
            else
            {
                Debug.Log("Level One Complete"); // Display a message when all targets are hit 
                                                       // Enter the "You win" state here

                levelOneComplete = true;
            }
        }
    }

    private void ActivateTarget(int index)
    {
        targets[index].SetActive(true); // Activate the target at the given index
        targets[currentTargetIndex].GetComponentInChildren<TargetController>().StartCoroutine
              (targets[currentTargetIndex].GetComponentInChildren<TargetController>().PlayUpAnimationAfterDelay(3));
    }
}
