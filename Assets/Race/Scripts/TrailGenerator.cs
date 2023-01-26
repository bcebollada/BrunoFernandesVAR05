using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class TrailGenerator : MonoBehaviour
{
    public int tilesNumber;
    public float spaceBetweenTiles;

    public GameObject muddyTerPrefab;
    public GameObject regularTerPrefab;
    public GameObject pawnPrefab;

    public GameObject pawnIA;
    public GameObject pawnPlayer;
    public Transform camera;

    public bool isGameOver;

    public List<GameObject> terrainList;

    public TMP_Text roundCounterText;
    public int roundCounter;
    public float timeBetweenRounds;

    public InputActionReference click;
    public bool gameStarted;

    public Material playerMaterial;

    // Start is called before the first frame update
    void Start()
    {
        terrainList.Add(muddyTerPrefab);
        terrainList.Add(regularTerPrefab);
        for(int i = 0; i < tilesNumber; i++)
        {
            var selectedTerrain = terrainList[Random.Range(0, 2)];

            var terrainTile = Instantiate(selectedTerrain, new Vector3(transform.position.x, transform.position.y, transform.position.z + i * spaceBetweenTiles), Quaternion.identity);
            if (i == tilesNumber-1) terrainTile.GetComponent<TerrainScript>().isLastTerrain = true;
        }

        pawnIA = Instantiate(pawnPrefab, new Vector3(transform.position.x -1, 1.49f, transform.position.z), Quaternion.identity);
        pawnPlayer = Instantiate(pawnPrefab, new Vector3(transform.position.x + 1, 1.49f, transform.position.z), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameStarted)
        {
            if (click.action.WasPressedThisFrame())
            {
                Debug.Log("clicked");
                Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    Debug.Log("ray hit" + hit.transform.gameObject.name);

                    if (hit.transform.gameObject == pawnIA)
                    {
                        Debug.Log("ray pawn");

                        pawnIA.GetComponent<MeshRenderer>().material = playerMaterial;
                        gameStarted = true;
                        StartCoroutine(MovePawns());

                    }
                    else if (hit.transform.gameObject == pawnPlayer)
                    {
                        Debug.Log("ray pawn 2");

                        pawnPlayer.GetComponent<MeshRenderer>().material = playerMaterial;
                        gameStarted = true;
                        StartCoroutine(MovePawns());

                    }

                }
            }

        }

        UpdateCamera();

    }

    IEnumerator MovePawns()
    {
        
        while(!isGameOver)
        {
            roundCounter += 1;
            roundCounterText.text = "Round: " + roundCounter.ToString(); 
            if(pawnIA.GetComponent<PawnScript>().isOnMuddyTerrain)
            {
                var chance = Random.Range(0, 9);
                if (chance == 0) pawnIA.transform.position = new Vector3(pawnIA.transform.position.x, pawnIA.transform.position.y, pawnIA.transform.position.z + spaceBetweenTiles);
            }
            else if (pawnIA.GetComponent<PawnScript>().isOnMuddyTerrain == false)
            {
                var chance = Random.Range(0, 5);
                if (chance == 0) pawnIA.transform.position = new Vector3(pawnIA.transform.position.x, pawnIA.transform.position.y, pawnIA.transform.position.z + spaceBetweenTiles);
            }

            if (pawnPlayer.GetComponent<PawnScript>().isOnMuddyTerrain)
            {
                var chance = Random.Range(0, 9);
                if (chance == 0) pawnPlayer.transform.position = new Vector3(pawnPlayer.transform.position.x, pawnPlayer.transform.position.y, pawnPlayer.transform.position.z + spaceBetweenTiles);
            }
            else if (pawnPlayer.GetComponent<PawnScript>().isOnMuddyTerrain == false)
            {
                var chance = Random.Range(0, 5);
                if (chance == 0) pawnPlayer.transform.position = new Vector3(pawnPlayer.transform.position.x, pawnPlayer.transform.position.y, pawnPlayer.transform.position.z + spaceBetweenTiles);
            }
            if ((pawnIA.GetComponent<PawnScript>().isOnLastTerrain) || (pawnPlayer.GetComponent<PawnScript>().isOnLastTerrain)) FinishGame();

            yield return new WaitForSeconds(timeBetweenRounds);
        }

    }

    public void FinishGame()
    {
        isGameOver = true;
        roundCounterText.text = "Game ended";
    }

    public void UpdateCamera()
    {
        if(pawnPlayer.transform.position.z < pawnIA.transform.position.z)
        {
            var translation = camera.position.z - (pawnPlayer.transform.position.z - 5);
            var newPosition = new Vector3(camera.position.x, camera.position.y, pawnPlayer.transform.position.z - 20);
            camera.position = Vector3.Lerp(camera.position, newPosition, 1);
        }
        else
        {
            var translation = camera.position.z - (pawnIA.transform.position.z - 5);
            var newPosition = new Vector3(camera.position.x, camera.position.y, pawnIA.transform.position.z - 20);
            camera.position = Vector3.Lerp(camera.position, newPosition, 1);
        }
    }
}
