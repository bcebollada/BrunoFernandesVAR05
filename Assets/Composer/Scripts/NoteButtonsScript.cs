using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteButtonsScript : MonoBehaviour
{
    public AudioClip note;
    public bool isEmpty;
    private GameObject gameController;

    private void Awake()
    {
        if(isEmpty) note = AudioClip.Create("empty", 1, 1, 1, false);
        gameController = GameObject.Find("GameController");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Select()
    {
        gameController.GetComponent<ComposerScript>().PressNote(note);
    }
}
