using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComposerScript : MonoBehaviour
{
    //[SerializeField] private AudioClip am4, a4, b4, cm4, c4, dm4, d4, e4, fm4, f4, gm4, g4;
    //private ArrayList noteList = new ArrayList();
    public List<AudioClip> noteList;
    public AudioSource audio;
    public GameObject noteSelectedPrefab;
    public Canvas canvas;
    public Transform timeLineStart;

        
    public void PressNote(AudioClip notePressed)
    {
        noteList.Add(notePressed);

        var note = Instantiate(noteSelectedPrefab, timeLineStart.position, Quaternion.identity);
        note.transform.SetParent(canvas.transform, false);
        note.transform.position = timeLineStart.position;
        timeLineStart.position = new Vector3(timeLineStart.position.x + 20, timeLineStart.position.y, timeLineStart.position.z);
        note.transform.GetChild(1).GetComponent<Text>().text = notePressed.ToString();

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

    public void RemoveNote()
    {

    }
}
