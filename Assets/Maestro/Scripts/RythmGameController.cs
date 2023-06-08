using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RythmGameController : MonoBehaviour
{
    public AudioSource songSource;

    public NoteSpawner noteSpawner;

    public int BPM = 60;

    private int previousBeat = -1;

    private double audioStartTime;

    public float spawnTimeOffset;

    public int score, scoreMultiplier;
    public float hitTimeTreshHold;

    private void Start()
    {
        double currentAudioTime = AudioSettings.dspTime;
        audioStartTime = (float)currentAudioTime + 3;

        songSource.PlayScheduled(audioStartTime + spawnTimeOffset);
        noteSpawner.noteSpawnTimeOffset = spawnTimeOffset + 3;
    }

    private void Update()
    {
        // Use the Unity time for now, even though it WOULD desync from a song!
        // float time = Time.time;

        // How much audio time has elapsed since we requested the audio source to play?
        double time = AudioSettings.dspTime - audioStartTime;

        // Convert it from seconds to minutes, e.g., 30 seconds = 0.5 minutes.
        float toMinutes = (float)(time / 60);

        // Convert from seconds to beat, rounding down (e.g., 45.67 seconds = beat 45 at 60 bpm).
        int beat = Mathf.FloorToInt(toMinutes * BPM);

        if (previousBeat != beat)
        {
            int random = Random.Range(0, 2);

            if(random == 0)
            {
                noteSpawner.SpawnObjects();
            }
            previousBeat = beat;
        }

        //Debug.Log($"Time: {time} Beat: {beat}");
    }
}
