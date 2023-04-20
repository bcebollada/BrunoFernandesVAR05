using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneReloader : MonoBehaviour
{
    public GameObject resetPumpkin;
    public float countdownTime = 5f; // time in seconds for the countdown
    public TMP_Text restartimeText;
    
    private float timer; // current time on the countdown

    public bool resetToggle = false;

    void Start()
    {
        timer = countdownTime;
    }

    void Update()
    {
        if (resetPumpkin != null)
        {
            if (resetPumpkin.GetComponent<ResetPumpkin>().resetPumpkinHasBeenHit == true)
            {
                resetToggle = true;
            }
        }

        
        if(resetToggle  == true)
        {
            timer -= Time.deltaTime;
            restartimeText.text = "Restarting in: " + Mathf.RoundToInt(timer).ToString();

            if (timer <= 0)
            {
                ReloadScene();
            }
        }

    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}