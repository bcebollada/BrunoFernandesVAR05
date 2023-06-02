using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteKiller : MonoBehaviour
{
    private NoteSpawner noteSpawner;

    // Start is called before the first frame update
    void Start()
    {
        noteSpawner = GameObject.Find("NoteSpawner").GetComponent<NoteSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Note"))
        {
            if(other.gameObject.GetComponent<NoteBehavior>().isLeft)
            {
                Destroy(noteSpawner.noteOrderLeft[noteSpawner.actualNoteToBeHitLeft]);
                noteSpawner.actualNoteToBeHitLeft += 1;
            }
            else
            {
                Destroy(noteSpawner.noteOrderRight[noteSpawner.actualNoteToBeHitRight]);
                noteSpawner.actualNoteToBeHitRight += 1;
            }
        }
    }
}