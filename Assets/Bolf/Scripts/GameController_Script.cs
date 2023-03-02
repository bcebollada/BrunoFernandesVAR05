using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController_Script : MonoBehaviour
{
    public int remainingPins;
    public TMP_Text remainingPinsText, finalPointsText;
    public GameObject menuButton, restartButton;

    public GameObject arrow, ball;
    private Vector3 ballInitialPos;
    private Rigidbody ballRb;

    private bool hasShoot;
    public float arrowRotVel;
    private bool rotateClockWards;

    public float ballVelocity;

    private bool isSecondAttempt;
    private float secondsWithBallStopped;

    private void Awake()
    {
        ballRb = ball.GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        ballInitialPos = ball.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        remainingPinsText.text = "Remaining pins: " + remainingPins.ToString();
        if(!hasShoot)
        {
            ballRb.constraints = RigidbodyConstraints.None;
            //Debug.Log(arrow.transform.eulerAngles.y);
            if (arrow.transform.eulerAngles.y >= 45 && arrow.transform.eulerAngles.y <= 46) rotateClockWards = false;
            else if (arrow.transform.eulerAngles.y <= 315 && arrow.transform.eulerAngles.y >= 314) rotateClockWards = true;
            if (rotateClockWards) arrow.transform.Rotate(new Vector3(0, arrowRotVel, 0));
            else arrow.transform.Rotate(new Vector3(0, arrowRotVel*(-1), 0));
        }
        if(Mouse.current.leftButton.wasPressedThisFrame && !hasShoot)
        {
            Debug.Log("Shoot");
            hasShoot = true;
            ballRb.AddForce(ballVelocity*arrow.transform.forward, ForceMode.Impulse);
            arrow.SetActive(false);
        }

        if (ballRb.velocity.magnitude <= 0.1 && hasShoot)
        {
            secondsWithBallStopped += Time.deltaTime;
            if (secondsWithBallStopped > 3)
            {
                Debug.Log("Ball stopped!");
                if (!isSecondAttempt) SecondAttempt();
                else GameOver();
            }

        }
        else secondsWithBallStopped = 0;
        if (remainingPins == 0) GameOver();
    }

    public void SecondAttempt()
    {
        Debug.Log("Second attempt!");
        if (isSecondAttempt)
        {
            GameOver();
            return;
        }
        isSecondAttempt = true;
        ball.transform.position = ballInitialPos;
        ballRb.constraints = RigidbodyConstraints.FreezeAll;
        arrow.SetActive(true);
        hasShoot = false;
    }

    public void GameOver()
    {
        remainingPinsText.gameObject.SetActive(false);
        if (remainingPins == 0) finalPointsText.text = "You got them all!";
        else finalPointsText.text = "You missed " + remainingPins + " pins :(";
        menuButton.SetActive(true);
        restartButton.SetActive(true);
    }

    public void Restart()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu_Scene");

    }

}
