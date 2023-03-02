using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin_Script : MonoBehaviour
{
    public GameController_Script gameController;
    private Quaternion initialRotation;
    public bool isStanding;

    private void Awake()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController_Script>();
        initialRotation = transform.localRotation;
    }

    private void Start()
    {
        gameController.remainingPins += 1;
    }

    // Update is called once per frame
    void Update()
    {
        float angle = Quaternion.Angle(transform.localRotation, initialRotation);
        if (angle >= 45 && isStanding)
        {
            isStanding = false;
            gameController.remainingPins -= 1;
        }
    }
}
