using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    private RythmGameController gameController;

    public Vector3 spawnAreaSize;
    public GameObject objectToSpawn;
    public int numObjectsToSpawn;

    public List<GameObject> noteOrderRight;
    public List<GameObject> noteOrderLeft;

    public int actualNoteToBeHitLeft;
    public int actualNoteToBeHitRight;

    public float noteSpawnTimeOffset; //seconds that the note will be spawned before its beat

    public Camera cam;

    private void Awake()
    {
        noteOrderRight = new List<GameObject>(); //activate the list
        noteOrderLeft = new List<GameObject>(); //activate the list

        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<RythmGameController>();
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
                    transform.position.z
                );

                var instantiatedNote = Instantiate(objectToSpawn, randomPoint, transform.rotation);
                var noteBehavior = instantiatedNote.GetComponent<NoteBehavior>();
                noteBehavior.isLeft = true;
                noteBehavior.ActivateCue(1, noteSpawnTimeOffset - 1);
                noteBehavior.noteSpawner = this;
                noteBehavior.cam = cam;

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
                var noteBehavior = instantiatedNote.GetComponent<NoteBehavior>();
                noteBehavior.isLeft = false;
                noteBehavior.ActivateCue(1, noteSpawnTimeOffset - 1);
                noteBehavior.noteSpawner = this;
                noteBehavior.cam = cam;

                noteOrderRight.Add(instantiatedNote); //add instatiated note to the list
            }
        }

    }

    public void NoteHitLeft(string noteType)
    {
        if(noteType == noteOrderLeft[actualNoteToBeHitLeft].GetComponent<NoteBehavior>().noteType)
        {
            var note = noteOrderLeft[actualNoteToBeHitLeft].GetComponent<NoteBehavior>();
            float hitDelay = note.spawnedTime - noteSpawnTimeOffset;

            gameController.AddScore(Mathf.Abs(hitDelay), noteOrderLeft[actualNoteToBeHitLeft].transform);

            //do something
            Destroy(noteOrderLeft[actualNoteToBeHitLeft]);
            actualNoteToBeHitLeft += 1;
        }
    }

    public void NoteHitRight(string noteType)
    {
        if (noteType == noteOrderRight[actualNoteToBeHitRight].GetComponent<NoteBehavior>().noteType)
        {
            var note = noteOrderRight[actualNoteToBeHitRight].GetComponent<NoteBehavior>();
            float hitDelay = note.spawnedTime - noteSpawnTimeOffset;
                
            gameController.AddScore(Mathf.Abs(hitDelay), noteOrderRight[actualNoteToBeHitRight].transform);

            //do something
            Destroy(noteOrderRight[actualNoteToBeHitRight]);
            actualNoteToBeHitRight += 1;

        }
    }
}
