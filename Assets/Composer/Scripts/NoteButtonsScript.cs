using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteButtonsScript : MonoBehaviour
{
    public AudioClip note;
    private GameObject gameController;

    private void Awake()
    {
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
