using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComposerScript : MonoBehaviour
{
    //[SerializeField] private AudioClip am4, a4, b4, cm4, c4, dm4, d4, e4, fm4, f4, gm4, g4;
    //private ArrayList noteList = new ArrayList();
    public List<AudioClip> noteList;
    public List<GameObject> noteBoxList;
    public AudioSource audio;
    public GameObject noteSelectedPrefab;
    public Canvas canvas;
    public Transform timeLineStart;
    private Vector3 timeLineStartBase;

    private void Start()
    {
        timeLineStartBase = timeLineStart.position;
    }


    public void PressNote(AudioClip notePressed)
    {
        noteList.Add(notePressed);
        UpdateTimeline();

        StopCoroutine("PlayMusic");
    }

    public void PressPlay()
    {
        StartCoroutine("PlayMusic");
    }

    IEnumerator PlayMusic()
    {
        foreach (AudioClip note in noteList)
        {
            audio.PlayOneShot(note);
            yield return new WaitForSeconds(1);
        }
    }

    public void UpdateTimeline()
    {
        StopAllCoroutines();
        timeLineStart.position = timeLineStartBase;

        foreach (GameObject gameObject in noteBoxList)
        {
            Destroy(gameObject);
        }

        foreach (AudioClip note in noteList)
        {
            var newNote = Instantiate(noteSelectedPrefab, timeLineStart.position, Quaternion.identity);
            newNote.transform.SetParent(canvas.transform, false);
            newNote.transform.position = timeLineStart.position;
            newNote.GetComponentInChildren<Text>().text = note.name;
            timeLineStart.position = new Vector3(timeLineStart.position.x + 20, timeLineStart.position.y, timeLineStart.position.z);
            noteBoxList.Add(newNote);

        }
    }

    public void RemoveNote(GameObject gameObject)
    {
        StopCoroutine("PlayMusic");

        var noteName = gameObject.GetComponentInChildren<Text>().text;
        StartCoroutine("RemoveNoteFromList", noteName);
        print("indo");
        noteBoxList.Remove(gameObject);
        Destroy(gameObject);


    }

    IEnumerator RemoveNoteFromList(string noteName)
    {
        foreach (AudioClip note in noteList)
        {
            if (note.name == noteName)
            {
                noteList.Remove(note);
                StopAllCoroutines();
                UpdateTimeline();
            }
            yield return null;
        }
    }
}
