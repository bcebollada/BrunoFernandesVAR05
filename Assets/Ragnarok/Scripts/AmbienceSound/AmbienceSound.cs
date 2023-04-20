using UnityEngine;

public class AmbienceSound : MonoBehaviour
{
    public AudioSource audioSourceAmbience, audioSourceEpic;
    public bool playEpicSound;
    public bool isPlayingEpicSound;

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
