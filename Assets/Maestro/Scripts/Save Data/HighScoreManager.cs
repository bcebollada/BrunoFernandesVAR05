using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
    public string fileName = "highscore.txt";
    private string folderName = "MyHighscores";
    private int highScore;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("TotalHighScore"))
        {
            highScore = PlayerPrefs.GetInt("TotalHighScore");
        }

        else
        {
            highScore=0;
        }
    }
    private void Start()
    {
        LoadHighScore();
    }

    public void SaveHighScore()
    {
        string filePath = Path.Combine(Application.dataPath, folderName, fileName);

        try 
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.Write(highScore.ToString());
            }
        
        }

        catch (IOException e)
        {
            Debug.LogWarning("Failed to save high score: " + e.Message);
        }
    }

    public void LoadHighScore()
    {
        string filePath = Path.Combine(Application.dataPath, folderName, fileName);
        if (File.Exists(filePath))
        {
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string highScoreString = reader.ReadToEnd();
                    if (int.TryParse(highScoreString, out int score))
                    {
                        highScore = score;
                    }

                    else
                    {
                        Debug.LogWarning("Invalid high score data in file.");
                    }
                }
            }
            catch (IOException e)
            {
                Debug.LogWarning("Failed to load high score: " + e.Message);
            }
        }

        else
        {
            Debug.LogWarning("High score file not found. Creating a new file.");
            SaveHighScore(); // Create a new file with the default high score.
        }
    }
}
