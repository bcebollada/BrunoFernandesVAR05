using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using UnityEngine.UIElements;

public class GuessingGameScript : MonoBehaviour
{
    [SerializeField] private TMP_InputField input;
    [SerializeField] private TMP_Text text;
    [SerializeField] private GameObject button;

    private int randomNum;
    private int buttonPressTimes;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GuessSubmit()
    {
        buttonPressTimes += 1;

        if(buttonPressTimes == 1)
        {
            int submitNum = int.Parse(input.text);
            randomNum = Random.Range(1, submitNum + 1);
            text.text = $"Guess a number between 1 and {submitNum}";
        }

        else
        {
            int guess = int.Parse(input.text);
            if (guess > randomNum)
            {
                text.text = guess + " is too high, guess again";
            }

            else if (guess < randomNum)
            {
                text.text = guess + " is too low, guess again";
            }

            else
            {
                text.text = "got it";
                button.SetActive(false);
                this.SendMessage("CloseGame", 3f);
            }
        }
    
    }

    private void CloseGame()
    {
        Application.Quit();
    }
}
