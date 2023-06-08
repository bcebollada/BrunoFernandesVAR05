using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public Vector3 spawnAreaSize;
    public GameObject objectToSpawn;
    public int numObjectsToSpawn;

    public List<GameObject> noteOrderRight;
    public List<GameObject> noteOrderLeft;

    public int actualNoteToBeHitLeft;
    public int actualNoteToBeHitRight;

    public float noteSpawnTimeOffset; //seconds that the note will be spawned before its beat

    private void Awake()
    {
        noteOrderRight = new List<GameObject>(); //activate the list
        noteOrderLeft = new List<GameObject>(); //activate the list
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(transform.position, spawnAreaSize);
    }

    public void SpawnObjects()
    {
        int randomNum2 = Random.Range(0, 2);

        if (randomNum2 == 0) //will spawn note for left hand
        {
            for (int i = 0; i < numObjectsToSpawn; i++) //spawn notes in random position inside area
            {
                Vector3 randomPoint = new Vector3(
                    Random.Range(transform.position.x - spawnAreaSize.x / 2, transform.position.x),
                    Random.Range(transform.position.y - spawnAreaSize.y / 2, transform.position.y + spawnAreaSize.y / 2),
                    Random.Range(transform.position.z - spawnAreaSize.z / 2, transform.position.z + spawnAreaSize.z / 2)
                );

                var instantiatedNote = Instantiate(objectToSpawn, randomPoint, transform.rotation);
                instantiatedNote.GetComponent<NoteBehavior>().isLeft = true;
                instantiatedNote.GetComponent<NoteBehavior>().ActivateCue(2, noteSpawnTimeOffset - 2);

                noteOrderLeft.Add(instantiatedNote); //add instatiated note to the list
            }
        }
        else //will spawn note for right hand
        {
            for (int i = 0; i < numObjectsToSpawn; i++) //spawn notes in random position inside area
            {
                Vector3 randomPoint = new Vector3(
                    Random.Range(transform.position.x, transform.position.x + spawnAreaSize.x / 2),
                    Random.Range(transform.position.y - spawnAreaSize.y / 2, transform.position.y + spawnAreaSize.y / 2),
                    Random.Range(transform.position.z - spawnAreaSize.z / 2, transform.position.z + spawnAreaSize.z / 2)
                );

                var instantiatedNote = Instantiate(objectToSpawn, randomPoint, transform.rotation);
                instantiatedNote.GetComponent<NoteBehavior>().isLeft = false;
                instantiatedNote.GetComponent<NoteBehavior>().ActivateCue(2, noteSpawnTimeOffset - 2);


                noteOrderRight.Add(instantiatedNote); //add instatiated note to the list
            }
        }

    }

    public void NoteHitLeft(string noteType)
    {
        if(noteType == noteOrderLeft[actualNoteToBeHitLeft].GetComponent<NoteBehavior>().noteType)
        {
            //do something
            Destroy(noteOrderLeft[actualNoteToBeHitLeft]);
            actualNoteToBeHitLeft += 1;
        }
    }

    public void NoteHitRight(string noteType)
    {
        if (noteType == noteOrderRight[actualNoteToBeHitRight].GetComponent<NoteBehavior>().noteType)
        {
            //do something
            print(actualNoteToBeHitRight);
            print(noteOrderRight[actualNoteToBeHitRight].name);
            Destroy(noteOrderRight[actualNoteToBeHitRight]);
            actualNoteToBeHitRight += 1;

        }
    }
}
