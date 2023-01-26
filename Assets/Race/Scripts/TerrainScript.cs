using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainScript : MonoBehaviour
{
    public bool isMuddyTerrain;
    public bool isLastTerrain;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(isMuddyTerrain) other.gameObject.GetComponent<PawnScript>().isOnMuddyTerrain = true;
        else if (!isMuddyTerrain)other.gameObject.GetComponent<PawnScript>().isOnMuddyTerrain = false;

        if(isLastTerrain) other.gameObject.GetComponent<PawnScript>().isOnLastTerrain = true;
        else if (!isLastTerrain) other.gameObject.GetComponent<PawnScript>().isOnLastTerrain = false;


    }
}
