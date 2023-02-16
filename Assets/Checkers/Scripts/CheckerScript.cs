using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CheckerScript : MonoBehaviour
{
    public GameObject buttonTopRight;
    public GameObject buttonTopLeft;
    public GameObject buttonDownRight;
    public GameObject buttonDownLeft;
    public GameObject buttonJumpTopRight;
    public GameObject buttonJumpTopLeft;
    public GameObject buttonJumpDownRight;
    public GameObject buttonJumpDownLeft;
    public Button checkerButton;

    public GameController gameController;

    public bool selected;
    public bool isRedChecker;
    public bool upgraded;

    public int hPosition;
    public int vPosition;

    public bool isRedTurn;



    // Start is called before the first frame update
    private void Awake()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        buttonTopRight.SetActive(false);
        buttonTopLeft.SetActive(false);
        buttonDownRight.SetActive(false);
        buttonDownLeft.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(selected)
        {
            if(gameController.canMoveTopRight) buttonTopRight.SetActive(true);
            if(gameController.canMoveTopLeft) buttonTopLeft.SetActive(true);
            if (gameController.canMoveDownRight) buttonDownRight.SetActive(true);
            if (gameController.canMoveDownLeft) buttonDownLeft.SetActive(true);

            if (gameController.canJumpTopRight) buttonJumpTopRight.SetActive(true);
            if (gameController.canJumpTopLeft) buttonJumpTopLeft.SetActive(true);
            if (gameController.canJumpDownRight) buttonJumpDownRight.SetActive(true);
            if (gameController.canJumpDownLeft) buttonJumpDownLeft.SetActive(true);
        }

        else
        {
            buttonTopRight.SetActive(false);
            buttonTopLeft.SetActive(false);
            buttonDownRight.SetActive(false);
            buttonDownLeft.SetActive(false);
            buttonJumpTopRight.SetActive(false);
            buttonJumpTopLeft.SetActive(false);
            buttonJumpDownRight.SetActive(false);
            buttonJumpDownLeft.SetActive(false);
        }
        
        if (!isRedChecker && !upgraded)
        {
            buttonJumpDownRight.SetActive(false);
            buttonJumpDownLeft.SetActive(false);
            buttonDownRight.SetActive(false);
            buttonDownLeft.SetActive(false);
        }
        else if (isRedChecker && !upgraded)
        {
            buttonJumpTopRight.SetActive(false);
            buttonJumpTopLeft.SetActive(false);
            buttonTopRight.SetActive(false);
            buttonTopLeft.SetActive(false);
        }

        if(gameController.isRedCheckerTurn)
        {
            if (isRedChecker) checkerButton.enabled = true;
            else checkerButton.enabled = false;
        }
        else
        {
            if (isRedChecker) checkerButton.enabled = false;
            else checkerButton.enabled = true;
        }
    }

    public void Selected()
    {
        GameObject[] checkers = GameObject.FindGameObjectsWithTag("checker");
        List<GameObject> checkerList = new List<GameObject>(checkers);
        foreach (GameObject checker in checkerList)
        {
            checker.GetComponent<CheckerScript>().selected = false;
        }

        selected = true;
        gameController.CheckSpaces(this.gameObject);
    }

    public void TopRight()
    {
        selected = false;
        transform.position = new Vector3(transform.position.x + gameController.horizontalOffset,transform.position.y + gameController.verticalOffset, transform.position.z);
        gameController.positions[hPosition, vPosition] = null;
        gameController.positions[hPosition+1, vPosition+1] = this.gameObject;
        hPosition += 1;
        vPosition += 1;
        if (isRedChecker) gameController.isRedCheckerTurn = false;
        else gameController.isRedCheckerTurn = true;
        CheckPosition();

    }

    public void TopLeft()
    {
        selected = false;
        transform.position = new Vector3(transform.position.x - gameController.horizontalOffset, transform.position.y + gameController.verticalOffset, transform.position.z);
        gameController.positions[hPosition, vPosition] = null;
        gameController.positions[hPosition - 1, vPosition + 1] = this.gameObject;
        hPosition -= 1;
        vPosition += 1;
        if (isRedChecker) gameController.isRedCheckerTurn = false;
        else gameController.isRedCheckerTurn = true;
        CheckPosition();

    }

    public void DownRight()
    {
        selected = false;
        transform.position = new Vector3(transform.position.x + gameController.horizontalOffset, transform.position.y - gameController.verticalOffset, transform.position.z);
        gameController.positions[hPosition, vPosition] = null;
        gameController.positions[hPosition + 1, vPosition - 1] = this.gameObject;
        hPosition += 1;
        vPosition -= 1;
        if (isRedChecker) gameController.isRedCheckerTurn = false;
        else gameController.isRedCheckerTurn = true;
        CheckPosition();

    }

    public void DownLeft()
    {
        selected = false;
        transform.position = new Vector3(transform.position.x - gameController.horizontalOffset, transform.position.y - gameController.verticalOffset, transform.position.z);
        gameController.positions[hPosition, vPosition] = null;
        gameController.positions[hPosition - 1, vPosition - 1] = this.gameObject;
        hPosition -= 1;
        vPosition -= 1;
        if (isRedChecker) gameController.isRedCheckerTurn = false;
        else gameController.isRedCheckerTurn = true;
        CheckPosition();

    }

    public void JumpTopRight()
    {
        selected = false;
        transform.position = new Vector3(transform.position.x + gameController.horizontalOffset*2, transform.position.y + gameController.verticalOffset*2, transform.position.z);
        gameController.positions[hPosition, vPosition] = null;
        Destroy(gameController.positions[hPosition + 1, vPosition + 1]); //destroys checker it jumped on
        gameController.positions[hPosition + 1, vPosition + 1] = null;
        gameController.positions[hPosition + 2, vPosition + 2] = this.gameObject;
        hPosition += 2;
        vPosition += 2;
        if (isRedChecker)
        {
            gameController.isRedCheckerTurn = false;
            gameController.whiteDestroyed += 1;
        }
        else
        {
            gameController.isRedCheckerTurn = true;
            gameController.redDestroyed += 1;
        }
        CheckPosition();

    }

    public void JumpTopLeft()
    {
        selected = false;
        transform.position = new Vector3(transform.position.x - gameController.horizontalOffset*2, transform.position.y + gameController.verticalOffset*2, transform.position.z);
        gameController.positions[hPosition, vPosition] = null;
        Destroy(gameController.positions[hPosition - 1, vPosition + 1]); //destroys checker it jumped on
        gameController.positions[hPosition - 1, vPosition + 1] = null;
        gameController.positions[hPosition - 2, vPosition + 2] = this.gameObject;
        hPosition -= 2;
        vPosition += 2;
        if (isRedChecker)
        {
            gameController.isRedCheckerTurn = false;
            gameController.whiteDestroyed += 1;
        }
        else
        {
            gameController.isRedCheckerTurn = true;
            gameController.redDestroyed += 1;
        }
        CheckPosition();

    }

    public void JumpDownRight()
    {
        selected = false;
        transform.position = new Vector3(transform.position.x + gameController.horizontalOffset*2, transform.position.y - gameController.verticalOffset*2, transform.position.z);
        gameController.positions[hPosition, vPosition] = null;
        Destroy(gameController.positions[hPosition + 1, vPosition - 1]); //destroys checker it jumped on
        gameController.positions[hPosition + 1, vPosition - 1] = null;
        gameController.positions[hPosition + 2, vPosition - 2] = this.gameObject;
        hPosition += 2;
        vPosition -= 2;
        if (isRedChecker)
        {
            gameController.isRedCheckerTurn = false;
            gameController.whiteDestroyed += 1;
        }
        else
        {
            gameController.isRedCheckerTurn = true;
            gameController.redDestroyed += 1;
        }
        CheckPosition();

    }

    public void JumpDownLeft()
    {
        selected = false;
        transform.position = new Vector3(transform.position.x - gameController.horizontalOffset*2, transform.position.y - gameController.verticalOffset*2, transform.position.z);
        gameController.positions[hPosition, vPosition] = null;
        Destroy(gameController.positions[hPosition - 1, vPosition - 1]); //destroys checker it jumped on
        gameController.positions[hPosition - 1, vPosition - 1] = null;
        gameController.positions[hPosition - 2, vPosition - 2] = this.gameObject;
        hPosition -= 2;
        vPosition -= 2;
        if (isRedChecker)
        {
            gameController.isRedCheckerTurn = false;
            gameController.whiteDestroyed += 1;
        }
        else
        {
            gameController.isRedCheckerTurn = true;
            gameController.redDestroyed += 1;
        }
        CheckPosition();
    }

    private void CheckPosition()
    {
        if(isRedChecker)
        {
            if (vPosition == 0) upgraded = true;
        }
        else
        {
            if (vPosition == 7) upgraded = true;
        }
    }
}

