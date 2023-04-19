using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbienceSound : MonoBehaviour
{

    public AudioSource audioSourceAmbience, audioSourceEpic;

    public bool playEpicSound;
    public bool isPlayingEpicSound;

    private void Awake()
    {

    }

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playEpicSound && !isPlayingEpicSound)
        {
            isPlayingEpicSound = true;
            audioSourceEpic.Play();
        }
        else if (!playEpicSound)
        {
            audioSourceEpic.Stop();
            isPlayingEpicSound = false;
        }
    }


}
