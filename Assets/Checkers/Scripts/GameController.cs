using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public GameObject[,] positions = new GameObject[8, 8]; //2d array, first horizontal, second vertical

    public GameObject blackChecker;
    public GameObject redChecker;

    public Transform blackCheckerSpawn;
    public Transform redCheckerSpawn;
    public float horizontalOffset;
    public float verticalOffset;

    public int blackDestroyed;
    public int redDestroyed;

    //bools to control is there are chekcers in relative positions
    public bool canMoveTopRight;
    public bool canMoveTopLeft;
    public bool canMoveDownRight;
    public bool canMoveDownLeft;
    public bool canJumpTopRight;
    public bool canJumpTopLeft;
    public bool canJumpDownRight;
    public bool canJumpDownLeft;

    public bool isRedCheckerTurn;
    public TMP_Text turnText;


    // Start is called before the first frame update
    private void Awake()
    {
        CreateWhiteCheckers();
        CreateRedCheckers();
    }

    // Update is called once per frame
    void Update()
    {
        if (isRedCheckerTurn) turnText.text = "is Red turn";
        else turnText.text = "is Black turn";

        if (blackDestroyed == 16) turnText.text = "Red wins!";
        else if (redDestroyed == 16) turnText.text = "Black wins!";
    }



    public void CreateWhiteCheckers()
    {
        for (int i = 0; i < 13; i++)
        {
            print(i);
            if (i <= 3)
            {
                var position = new Vector3(blackCheckerSpawn.position.x + (i * horizontalOffset * 2), blackCheckerSpawn.position.y, blackCheckerSpawn.position.z);
                var checker = Instantiate(blackChecker, position, Quaternion.identity);
                positions[i*2, 0] = checker;
                checker.transform.SetParent(blackCheckerSpawn, false);
                checker.transform.position = position;

                checker.GetComponent<CheckerScript>().hPosition = i*2; //saves array horizontal position
                checker.GetComponent<CheckerScript>().vPosition = 0; //saves array vertical position

            }
            else if (i <= 7)
            {
                var position = new Vector3(blackCheckerSpawn.position.x + ((i -4 ) * horizontalOffset * 2 + horizontalOffset), blackCheckerSpawn.position.y + verticalOffset, blackCheckerSpawn.position.z);
                var checker = Instantiate(blackChecker, position, Quaternion.identity);
                positions[(i - 4)*2+1, 1] = checker;
                print("position" + (i - 4));
                checker.transform.SetParent(blackCheckerSpawn, false);
                checker.transform.position = position;

                checker.GetComponent<CheckerScript>().hPosition = (i - 4)*2+1; //saves array horizontal position
                checker.GetComponent<CheckerScript>().vPosition = 1; //saves array vertical position
            }

            else if (i <= 11)
            {
                var position = new Vector3(blackCheckerSpawn.position.x + ((i - 8) * horizontalOffset * 2), blackCheckerSpawn.position.y + verticalOffset*2, blackCheckerSpawn.position.z);
                var checker = Instantiate(blackChecker, position, Quaternion.identity);
                positions[(i - 8)*2, 2] = checker;
                checker.transform.SetParent(blackCheckerSpawn, false);
                checker.transform.position = position;

                checker.GetComponent<CheckerScript>().hPosition = (i - 8)*2; //saves array horizontal position
                checker.GetComponent<CheckerScript>().vPosition = 2; //saves array vertical position
            }
            //yield return new WaitForSeconds(1);
        }
    }


    public void CreateRedCheckers()
    {
        for (int i = 0; i < 13; i++)
        {
            if (i <= 3)
            {
                var position = new Vector3(redCheckerSpawn.position.x + (i * horizontalOffset * 2) + horizontalOffset, redCheckerSpawn.position.y, redCheckerSpawn.position.z);
                var checker = Instantiate(redChecker, position, Quaternion.identity);
                positions[i*2+1, 5] = checker;
                checker.transform.SetParent(redCheckerSpawn, false);
                checker.transform.position = position;

                checker.GetComponent<CheckerScript>().hPosition = i*2+1; //saves array horizontal position
                checker.GetComponent<CheckerScript>().vPosition = 5; //saves array vertical position
            }
            else if(i<=7)
            {
                var position = new Vector3(redCheckerSpawn.position.x + ((i - 4) * horizontalOffset*2), redCheckerSpawn.position.y + verticalOffset, redCheckerSpawn.position.z);
                var checker = Instantiate(redChecker, position, Quaternion.identity);
                positions[(i - 4)*2, 6] = checker;
                checker.transform.SetParent(redCheckerSpawn, false);
                checker.transform.position = position;

                checker.GetComponent<CheckerScript>().hPosition = (i - 4)*2; //saves array horizontal position
                checker.GetComponent<CheckerScript>().vPosition = 6; //saves array vertical position
            }
            else if (i <=11)
            {
                var position = new Vector3(redCheckerSpawn.position.x + ((i - 8) * horizontalOffset * 2 + horizontalOffset), redCheckerSpawn.position.y + verticalOffset*2, redCheckerSpawn.position.z);
                var checker = Instantiate(redChecker, position, Quaternion.identity);
                positions[(i - 8) * 2+1, 7] = checker;
                checker.transform.SetParent(redCheckerSpawn, false);
                checker.transform.position = position;

                checker.GetComponent<CheckerScript>().hPosition = (i - 8)*2+1; //saves array horizontal position
                checker.GetComponent<CheckerScript>().vPosition = 7; //saves array vertical position
            }
        }
    }


    public void CheckSpaces(GameObject gameObj)
    {
        canMoveTopRight = false;
        canMoveTopLeft = false;
        canMoveDownRight = false;
        canMoveDownLeft = false;
        canJumpTopRight = false;
        canJumpTopLeft = false;
        canJumpDownRight = false;
        canJumpDownLeft = false;

        var hPosition = gameObj.GetComponent<CheckerScript>().hPosition;
        var vPosition = gameObj.GetComponent<CheckerScript>().vPosition;
        var position = positions[hPosition, vPosition];


        //checks if checker can move to diagonal adjacent spaces
        if(hPosition == 0 && vPosition == 0)
        {
            if (positions[hPosition + 1, vPosition + 1] == null)
            {
                canMoveTopRight = true;
            }
        }

        else if (hPosition == 0 && vPosition == 7)
        {
            if (positions[hPosition + 1, vPosition - 1] == null)
            {
                canMoveDownRight = true;
            }
        }

        else if(hPosition == 0 && vPosition > 0 && vPosition < 7)
        {
            if (positions[hPosition + 1, vPosition + 1] == null)
            {
                canMoveTopRight = true;
            }
            if (positions[hPosition + 1, vPosition - 1] == null)
            {

                canMoveDownRight = true;
            }
        }

        else if(hPosition == 7 && vPosition == 0)
        {
            if (positions[hPosition - 1, vPosition + 1] == null)
            {
                canMoveTopLeft = true;
            }
        }

        else if(hPosition == 7 && vPosition == 7)
        {
            if (positions[hPosition - 1, vPosition - 1] == null)
            {
                canMoveDownLeft = true;
            }
        }

        else if (hPosition == 7 && vPosition > 0 && vPosition < 7)
        {
            if (positions[hPosition - 1, vPosition + 1] == null)
            {
                canMoveTopLeft = true;
            }

            if (positions[hPosition - 1, vPosition - 1] == null)
            {
                canMoveDownLeft = true;
            }
        }

        else if (vPosition == 7 && hPosition > 0 && hPosition < 7)
        {
            if (positions[hPosition + 1, vPosition - 1] == null)
            {
                canMoveDownRight = true;
            }
            if (positions[hPosition - 1, vPosition - 1] == null)
            {
                canMoveDownLeft = true;
            }
        }

        else if (vPosition == 0 && hPosition > 0 && hPosition < 7)
        {
            if (positions[hPosition + 1, vPosition + 1] == null)
            {
                canMoveTopRight = true;
            }
            if (positions[hPosition - 1, vPosition + 1] == null)
            {
                canMoveTopLeft = true;
            }
        }

        else
        {

            if (hPosition < 7 && vPosition < 7 && positions[hPosition + 1, vPosition + 1] == null)
            {
                canMoveTopRight = true;
            }
            if (hPosition > 0 && vPosition < 7 && positions[hPosition - 1, vPosition + 1] == null)
            {
                canMoveTopLeft = true;
            }
            if (hPosition < 7 && vPosition > 0 && positions[hPosition + 1, vPosition - 1] == null)
            {

                canMoveDownRight = true;
            }
            if (hPosition > 0 && vPosition > 0 && positions[hPosition - 1, vPosition - 1] == null)
            {
                canMoveDownLeft = true;
            }
        }


        //checks if checker can jump to diagonal spaces
        if (hPosition < 2 && vPosition < 2)
        {
           if(positions[hPosition+1, vPosition+1] != null)
            {
                if (positions[hPosition + 2, vPosition + 2] == null)
                {
                    canJumpTopRight = true;
                }
            }
        }

        else if (hPosition < 2 && vPosition > 5)
        {
            if (positions[hPosition + 1, vPosition - 1] != null)
            {
                if (positions[hPosition + 2, vPosition - 2] == null)
                {
                    canJumpDownRight = true;
                }
            }
        }

        else if (hPosition < 2 && vPosition >= 2 && vPosition <= 5)
        {
            if (positions[hPosition + 1, vPosition + 1] != null)
            {
                if (positions[hPosition + 2, vPosition + 2] == null)
                {
                    canJumpTopRight = true;
                }
            }

            if (positions[hPosition + 1, vPosition - 1] != null)
            {
                if (positions[hPosition + 2, vPosition - 2] == null)
                {
                    canJumpDownRight = true;
                }
            }
        }

        else if (hPosition > 5 && vPosition < 2)
        {
            if (positions[hPosition - 1, vPosition + 1] != null)
            {
                if (positions[hPosition - 2, vPosition + 2] == null)
                {
                    canJumpTopLeft = true;
                }
            }
        }

        else if (hPosition > 5 && vPosition > 5)
        {
            if (positions[hPosition - 1, vPosition - 1] != null)
            {
                if (positions[hPosition - 2, vPosition - 2] == null)
                {
                    canJumpDownLeft = true;
                }
            }
        }

        else if (hPosition > 5 && vPosition >= 2 && vPosition <= 5)
        {
            if (positions[hPosition - 1, vPosition - 1] != null)
            {
                if (positions[hPosition - 2, vPosition - 2] == null)
                {
                    canJumpDownLeft = true;
                }
            }

            if (positions[hPosition - 1, vPosition + 1] != null)
            {
                if (positions[hPosition - 2, vPosition + 2] == null)
                {
                    canJumpTopLeft = true;
                }
            }
        }

        else if (vPosition > 5 && hPosition >= 2 && hPosition <= 5)
        {
            print("here");
            if (positions[hPosition - 1, vPosition - 1] != null)
            {
                if (positions[hPosition - 2, vPosition - 2] == null)
                {
                    canJumpDownLeft = true;
                }
            }

            if (positions[hPosition + 1, vPosition - 1] != null)
            {
                if (positions[hPosition + 2, vPosition - 2] == null)
                {
                    canJumpDownRight = true;
                }
            }
        }

        else if (vPosition < 2 && hPosition >= 2 && hPosition <= 5)
        {
            if (positions[hPosition - 1, vPosition + 1] != null)
            {
                if (positions[hPosition - 2, vPosition + 2] == null)
                {
                    canJumpTopLeft = true;
                }
            }

            if (positions[hPosition + 1, vPosition + 1] != null)
            {
                if (positions[hPosition + 2, vPosition + 2] == null)
                {
                    canJumpTopRight = true;
                }
            }

        }

        else
        {
            if (positions[hPosition + 1, vPosition + 1] != null)
            {
                if (positions[hPosition + 2, vPosition + 2] == null)
                {
                    canJumpTopRight = true;
                }
            }
            if (positions[hPosition - 1, vPosition + 1] != null)
            {
                if (positions[hPosition - 2, vPosition + 2] == null)
                {
                    canJumpTopLeft = true;
                }
            }
            if (positions[hPosition + 1, vPosition - 1] != null)
            {
                if (positions[hPosition + 2, vPosition - 2] == null)
                {
                    canJumpDownRight = true;
                }
            }
            if (positions[hPosition - 1, vPosition - 1] != null)
            {
                if (positions[hPosition - 2, vPosition - 2] == null)
                {
                    canJumpDownLeft = true;
                }
            }

        }

    }
}
