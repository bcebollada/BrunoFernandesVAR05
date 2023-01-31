using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardObjectScript : MonoBehaviour
{
    public string cardName;
    public string type;
    public int value;
    public TMP_Text nameText;

    private List<string> cardTypes = new List<string> { "diamond", "club", "heart", "spade" };
    private List<string> cardLetter = new List<string> { "A", "J", "Q", "K" };

    private void Awake()
    {
        //checks value and type of card
        

    }

    // Start is called before the first frame update
    void Start()
    {
        nameText.text = cardName;
        CheckCardValueAndType();
        print("calc");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CheckCardValueAndType()
    {
        for (int i = 2; i <= 10; i++)
        {
            var spadeNumbers = "spade " + i;
            var heartNumbers = "heart " + i;
            var clubNumbers = "club " + i;
            var diamondNumbers = "diamond " + i;
            if (cardName == heartNumbers)
            {
                value = i;
                type = "heart";
            }

            if (cardName == spadeNumbers)
            {
                value = i;
                type = "spade";
            }

            if (cardName == clubNumbers)
            {
                value = i;
                type = "club";
            }

            if (cardName == diamondNumbers)
            {
                value = i;
                type = "diamond";
            }
        }
        foreach (string types in cardTypes)
        {
            foreach (string letter in cardLetter)
            {
                var possibleCardName = $"{types} {letter}";
                if (cardName == possibleCardName)
                {
                    type = types;
                    if (letter == "A") value = 11;
                    else value = 10;
                }
            }
        }
    }
}
