using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CheckerScript : MonoBehaviour
{
    public GameObject buttonTopRight;
    public GameObject buttonTopLeft;
    public GameObject buttonDownRight;
    public GameObject buttonDownLeft;
    public GameController gameController;

    public bool selected;
    public bool isRedChecker;
    public bool upgraded;

    public int hPosition;
    public int vPosition;

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
        }

        else
        {
            buttonTopRight.SetActive(false);
            buttonTopLeft.SetActive(false);
            buttonDownRight.SetActive(false);
            buttonDownLeft.SetActive(false);
        }
    }

    public void Selected()
    {
        selected = true;
        gameController.CheckSpaces(this.gameObject);
    }

    public void TopRight()
    {
        selected = false;

    }

    public void TopLeft()
    {
        selected = false;

    }

    public void DownRight()
    {
        selected = false;

    }

    public void DownLeft()
    {
        selected = false;

    }
}

