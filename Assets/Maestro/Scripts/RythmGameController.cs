using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class RythmGameController : MonoBehaviour
{
    public AudioSource songSource;

    public NoteSpawner noteSpawner;

    public int BPM = 60;

    private int previousBeat = -1;

    private double audioStartTime;

    public float spawnTimeOffset;

    public int score, scoreMultiplier, comboCount;
    public float hitTimeTreshHold;

    private int totalScore, totalHighScore;
    public TMP_Text scoreText, scoreXText, highScoreText;

    public GameObject feebackTextPrefab;

    private void Start()
    {
        double currentAudioTime = AudioSettings.dspTime;
        audioStartTime = (float)currentAudioTime + 4;

        songSource.PlayScheduled(audioStartTime);
        noteSpawner.noteSpawnTimeOffset = spawnTimeOffset + 2;

        LoadScore();
        UpdateScoreUI();
    }

    private void Update()
    {
        // Use the Unity time for now, even though it WOULD desync from a song!
        // float time = Time.time;

        // How much audio time has elapsed since we requested the audio source to play?
        double time = AudioSettings.dspTime - audioStartTime;
        float toMinutes = (float)(time / 60);
        int beat = Mathf.FloorToInt(toMinutes * BPM);

        // Calculate the time to spawn the note before the beat
        float secondsBeforeBeat = 2.0f; // Replace this value with the desired number of seconds
        float spawnTime = beat + secondsBeforeBeat;
        //print(spawnTime);
        if (previousBeat != beat && spawnTime >= 0)
        {
            int random = Random.Range(0, 2);

            if (beat == 0) noteSpawner.SpawnObjects();
            else if (random == 0)
            {
                noteSpawner.SpawnObjects();
            }
            previousBeat = beat;
        }

        scoreText.text = $"Score: {score}";
        scoreXText.text = $"x{scoreMultiplier}";
    }

    public void AddScore(float hitDelay, Transform note)
    {
        if(hitDelay <= hitTimeTreshHold)
        {
            comboCount += 2;
            //scoreMultiplier = scoreMultiplier * 3;
            var feedback = Instantiate(feebackTextPrefab, note.position, note.rotation);
            feedback.GetComponentInChildren<TMP_Text>().text = "Great!";
            Destroy(feedback, 0.8f);
        }
        else if(hitDelay <= hitTimeTreshHold * 1.5)
        {
            comboCount += 1;
            //scoreMultiplier = scoreMultiplier * 2;
            var feedback = Instantiate(feebackTextPrefab, note.position, note.rotation);
            feedback.GetComponentInChildren<TMP_Text>().text = "Ok!";
            Destroy(feedback, 0.8f);

        }
        else if (hitDelay <= hitTimeTreshHold * 2)
        {
            comboCount += 1;

            var feedback = Instantiate(feebackTextPrefab, note.position, note.rotation);
            feedback.GetComponentInChildren<TMP_Text>().text = "Kinda bad";
            Destroy(feedback, 0.8f);
        }
        else //player missed badly
        {
            comboCount = 0;
            var feedback = Instantiate(feebackTextPrefab, note.position, note.rotation);
            feedback.GetComponentInChildren<TMP_Text>().text = "U suck!";
            Destroy(feedback, 0.8f);
        }
        scoreMultiplier = Mathf.RoundToInt(Mathf.Sqrt(comboCount));
        score += scoreMultiplier;

        totalScore += score;
        totalHighScore = Mathf.Max(totalHighScore, score);

        SaveScore();
        UpdateScoreUI();
    }

    private void LoadScore()
    {
        // Load the total score and total high score from PlayerPrefs or other storage method
        totalScore = PlayerPrefs.GetInt("TotalScore", 0);
        string filePath = Path.Combine(Application.dataPath, "MyHighscores", "highscore.txt");

        if (File.Exists(filePath))
        {
            string scoreString = File.ReadAllText(filePath);
            if (int.TryParse(scoreString, out int loadedScore))
            {
                totalHighScore = loadedScore;
                PlayerPrefs.SetInt("TotalHighScore", totalHighScore);
                PlayerPrefs.Save();

            }
        }
    }

    private void SaveScore()
    {
        // Save the total score and total high score to PlayerPrefs or other storage method
        PlayerPrefs.SetInt("TotalScore", totalScore);
        PlayerPrefs.SetInt("TotalHighScore", totalHighScore);
        PlayerPrefs.Save();
    }

    private void UpdateScoreUI()
    {
        highScoreText.text = $"Highest Score: {totalHighScore}"; // Update the high score text
    }
}
