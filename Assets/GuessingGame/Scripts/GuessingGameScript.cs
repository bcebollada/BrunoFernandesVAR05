using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class GuessingGameScript : MonoBehaviour
{
    [SerializeField] private InputField input;
    [SerializeField] private Text text;

    private int randomNum;

    // Start is called before the first frame update
    void Start()
    {
        var RndB = new System.Random();
        randomNum = RndB.Next(0, 10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GuessSubmit()
    {
        int guess = int.Parse(input.text);
        if (guess > randomNum)
        {
            text.text = "too high, guess again";
        }

        else if (guess < randomNum)
        {
            text.text = "too low, guess again";
        }

        else
        {
            text.text = "got it";
            this.SendMessage("CloseGame", 3f);
        }
    }

    private void CloseGame()
    {
        Application.Quit();
    }
}
