using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject[,] positions = new GameObject[8,8]; //2d array, first horizontal, second vertical

    public GameObject whiteChecker;
    public GameObject redChecker;

    public Transform whiteCheckerSpawn;
    public Transform redCheckerSpawn;
    public float horizontalOffset;
    public float verticalOffset;

    //bools to control is there are chekcers in relative positions
    public bool canMoveTopRight;
    public bool canMoveTopLeft;
    public bool canMoveDownRight;
    public bool canMoveDownLeft;
    public bool canJumpTopRight;
    public bool canJumpTopLeft;
    public bool canJumpDownRight;
    public bool canJumpDownLeft;

    // Start is called before the first frame update
    private void Awake()
    {
        CreateWhiteCheckers();
        CreateRedCheckers();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateWhiteCheckers()
    {
        for (int i = 0; i < 16; i++)
        {
            if(i < 8)
            {
                var position = new Vector3(whiteCheckerSpawn.position.x + (i * horizontalOffset), whiteCheckerSpawn.position.y, whiteCheckerSpawn.position.z);
                var checker = Instantiate(whiteChecker, position, Quaternion.identity);
                positions[i, 0] = checker;
                checker.transform.SetParent(whiteCheckerSpawn, false);
                checker.transform.position = position;

                checker.GetComponent<CheckerScript>().hPosition = i; //saves array horizontal position
                checker.GetComponent<CheckerScript>().vPosition = 0; //saves array vertical position

            }
            else
            {
                var position = new Vector3(whiteCheckerSpawn.position.x + ((i-8) * horizontalOffset), whiteCheckerSpawn.position.y + verticalOffset, whiteCheckerSpawn.position.z);
                var checker = Instantiate(whiteChecker, position, Quaternion.identity);
                positions[(i-8), 1] = checker;
                checker.transform.SetParent(whiteCheckerSpawn, false);
                checker.transform.position = position;

                checker.GetComponent<CheckerScript>().hPosition = i-8; //saves array horizontal position
                checker.GetComponent<CheckerScript>().vPosition = 1; //saves array vertical position
            }
        }
    }

    public void CreateRedCheckers()
    {
        for (int i = 0; i < 16; i++)
        {
            if (i < 8)
            {
                var position = new Vector3(redCheckerSpawn.position.x + (i * horizontalOffset), redCheckerSpawn.position.y, redCheckerSpawn.position.z);
                var checker = Instantiate(redChecker, position, Quaternion.identity);
                positions[i, 6] = checker;
                checker.transform.SetParent(redCheckerSpawn, false);
                checker.transform.position = position;

                checker.GetComponent<CheckerScript>().hPosition = i; //saves array horizontal position
                checker.GetComponent<CheckerScript>().vPosition = 6; //saves array vertical position
            }
            else
            {
                var position = new Vector3(redCheckerSpawn.position.x + ((i - 8) * horizontalOffset), redCheckerSpawn.position.y + verticalOffset, redCheckerSpawn.position.z);
                var checker = Instantiate(redChecker, position, Quaternion.identity);
                positions[(i - 8), 7] = checker;
                checker.transform.SetParent(redCheckerSpawn, false);
                checker.transform.position = position;

                checker.GetComponent<CheckerScript>().hPosition = i - 8; //saves array horizontal position
                checker.GetComponent<CheckerScript>().vPosition = 7; //saves array vertical position
            }
        }
    }

    public void CheckSpaces(GameObject gameObj)
    {
        var hPosition = gameObj.GetComponent<CheckerScript>().hPosition;
        var vPosition = gameObj.GetComponent<CheckerScript>().vPosition;
        var position = positions[hPosition, vPosition];

        if(positions[hPosition+1,vPosition+1] = null)
        {
            canMoveTopRight = true;
        }
        if (positions[hPosition - 1, vPosition + 1] = null)
        {
            canMoveTopLeft = true;
        }
        if (positions[hPosition + 1, vPosition - 1] = null)
        {
            canMoveDownRight = true;
        }
        if (positions[hPosition - 1, vPosition - 1] = null)
        {
            canMoveDownLeft = true;
        }

    }

}
