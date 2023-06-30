using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    public TMP_Text highScoreText;

    // Start is called before the first frame update
    private void Start()
    {
        int highScore = PlayerPrefs.GetInt("TotalHighScore", 0);
        Debug.Log("High Score: " + highScore);

        highScoreText.text = "High Score: " + highScore;
    }
}
