using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLevelController : MonoBehaviour

{
    public GameObject[] targets; // Array of target objects
    
    private int currentTargetIndex = 0; // Index of the current target

    private void Start()
    {
        ActivateTarget(currentTargetIndex); // Activate the first target
    }

    private void Update()
    {
        if (targets[currentTargetIndex].GetComponentInChildren<TargetController>().hasBeenHit == true)
        {
            HitTarget(); // Call the HitTarget function
        }
    }

    private void HitTarget()
    {
        if(targets[currentTargetIndex].GetComponentInChildren<TargetController>().axeHasBeenRecalled == true)
        {
            targets[currentTargetIndex].GetComponentInChildren<TargetController>().StartCoroutine
                (targets[currentTargetIndex].GetComponentInChildren<TargetController>().PlayDownAnimationAfterDelay(3f));

            //targets[currentTargetIndex].SetActive(false); // Deactivate the current target

            currentTargetIndex++; // Move to the next target

            if (currentTargetIndex < targets.Length) // Check if there are more targets
            {
                ActivateTarget(currentTargetIndex); // Activate the next target
            }

        }

        //else
        //{
        //    Debug.Log("You hit all the targets!"); // Display a message when all targets are hit
        //}
    }

    private void ActivateTarget(int index)
    {
        targets[index].SetActive(true); // Activate the target at the given index
        targets[currentTargetIndex].GetComponentInChildren<TargetController>().StartCoroutine
              (targets[currentTargetIndex].GetComponentInChildren<TargetController>().PlayUpAnimationAfterDelay(3));
    }
}
