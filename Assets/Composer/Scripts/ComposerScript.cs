using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComposerScript : MonoBehaviour
{
    [SerializeField] private AudioClip am4, a4, b4, cm4, c4, dm4, d4, e4, fm4, f4, gm4, g4;

    private ArrayList noteList;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PressNote(string note)
    {
        noteList.Add(note);
    }

    public void Play()
    {
        foreach(string note in noteList)
        {

        }
    }
}
