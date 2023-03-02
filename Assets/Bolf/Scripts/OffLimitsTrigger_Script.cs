using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffLimitsTrigger_Script : MonoBehaviour
{
    public GameController_Script gameController;

    private void Awake()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController_Script>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Ball"))gameController.SecondAttempt();
    }
}
