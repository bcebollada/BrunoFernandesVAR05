using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteBoxScript : MonoBehaviour
{

    private ComposerScript composer;
    // Start is called before the first frame update
    void Start()
    {
        composer = GameObject.Find("GameController").GetComponent<ComposerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RemoveButton()
    {
        composer.RemoveNote(this.gameObject);
    }
}
