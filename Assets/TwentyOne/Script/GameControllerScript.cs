using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameControllerScript : MonoBehaviour
{
    public List<string> cardsDeck;
    public List<string> dealerCards;
    public List<string> playerCards;
    public List<GameObject> playerCardsGameObjects;
    public List<GameObject> dealerCardsGameObjects;
    public GameObject cardPrefab;
    public GameObject canvas;

    public int previousDeckCardsNum;

    public Transform handTransform;
    private Vector3 handTransformBasePosition;

    public int playerHandValue;
    public int dealerHandValue;

    public TMP_Text handValueText;
    public TMP_Text dealerHandValueText;
    public TMP_Text gameOverText;

    private bool isDealerTurn;

    private void Awake()
    {
        previousDeckCardsNum = cardsDeck.Count;
            
        handTransformBasePosition = handTransform.position;

        AddCardsToList(); //adds all 52 cards to list

    }

    // Start is called before the first frame update
    void Start()
    {
        GiveStartHand();


    }

    // Update is called once per frame
    void Update()
    {
        if(cardsDeck.Count != previousDeckCardsNum)
        {
            UpdateCardsOnHands();
        }

        if (isDealerTurn)
        {
            GiveDealerCards();
            dealerHandValueText.text = "dealer hand: " +dealerHandValue.ToString();

        }

        previousDeckCardsNum = cardsDeck.Count;

    }

    private void AddCardsToList()
    {
        for (int i = 2; i <= 10; i++)
        {
            cardsDeck.Add("spade " + i.ToString()); //adds all spades number cards
            cardsDeck.Add("heart " + i.ToString()); //adds all hearts number cards
            cardsDeck.Add("diamond " + i.ToString()); //adds all diamonds number cards
            cardsDeck.Add("club " + i.ToString()); //adds all clubs  number cards
        }
        //adds spade letters
        cardsDeck.Add("spade A");
        cardsDeck.Add("spade J");
        cardsDeck.Add("spade Q");
        cardsDeck.Add("spade K");

        //adds hearts letters
        cardsDeck.Add("heart A");
        cardsDeck.Add("heart J");
        cardsDeck.Add("heart Q");
        cardsDeck.Add("heart K");

        //adds diamonds letters
        cardsDeck.Add("diamond A");
        cardsDeck.Add("diamond J");
        cardsDeck.Add("diamond Q");
        cardsDeck.Add("diamond K");

        //adds clubs letters
        cardsDeck.Add("club A");
        cardsDeck.Add("club J");
        cardsDeck.Add("club Q");
        cardsDeck.Add("club K");
    }

    private void GiveStartHand()
    {
        for(int i = 0; i<2; i++)
        {
            //gets random card, giver to player and removes from deck
            var playerCard1 = cardsDeck[Random.RandomRange(1, cardsDeck.Count + 1)];
            cardsDeck.Remove(playerCard1);
            playerCards.Add(playerCard1);
        }

        for (int i = 0; i < 2; i++)
        {
            //gets random card, giver to player and removes from deck
            var dealerCard1 = cardsDeck[Random.RandomRange(1, cardsDeck.Count + 1)];
            cardsDeck.Remove(dealerCard1);
            dealerCards.Add(dealerCard1);
        }

    }

    private void UpdateCardsOnHands()
    {
        //destroys all player cards to update
        foreach(GameObject card in playerCardsGameObjects)
        {
            Destroy(card);
        }
        //destroys all dealer cards to update
        foreach (GameObject card in dealerCardsGameObjects)
        {
            Destroy(card);
        }

        dealerCardsGameObjects.Clear();
        playerCardsGameObjects.Clear();

        foreach (string card in playerCards)
        {
            var cardObj = Instantiate(cardPrefab, handTransform.position, Quaternion.identity);
            print("esse" + card);
            cardObj.GetComponent<CardObjectScript>().cardName = card;
            cardObj.transform.SetParent(canvas.transform, false);
            playerCardsGameObjects.Add(cardObj);
            handTransform.position = new Vector3(handTransform.position.x + 90, handTransform.position.y, handTransform.position.z);
        }
        handTransform.position = handTransformBasePosition;

        foreach (string card in dealerCards)
        {
            var cardObj = Instantiate(cardPrefab, new Vector3(handTransform.position.x, handTransform.position.y + 250, handTransform.position.z), Quaternion.identity);
            cardObj.GetComponent<CardObjectScript>().cardName = card;
            cardObj.transform.SetParent(canvas.transform, false);
            dealerCardsGameObjects.Add(cardObj);
            handTransform.position = new Vector3(handTransform.position.x + 90, handTransform.position.y, handTransform.position.z);
        }
        handTransform.position = handTransformBasePosition;
        CheckHandValues();
    }

    private void CheckHandValues()
    {
        print("xx" + playerCardsGameObjects.Count);
        playerHandValue = 0;
        dealerHandValue = 0;
        foreach(GameObject card in playerCardsGameObjects)
        {
            card.GetComponent<CardObjectScript>().CheckCardValueAndType();
            var CardValue = card.GetComponent<CardObjectScript>().value;
            print("value" + card.GetComponent<CardObjectScript>().value);
            print("card" + CardValue);
            playerHandValue += CardValue;
        }
        handValueText.text = "your hand: " + playerHandValue.ToString();
        if (playerHandValue > 21) DealerWins();

        foreach (GameObject card in dealerCardsGameObjects)
        {
            card.GetComponent<CardObjectScript>().CheckCardValueAndType();

            var CardValue = card.GetComponent<CardObjectScript>().value;
            dealerHandValue += CardValue;
        }
        dealerHandValueText.text = "dealer hand: ?";

    }

    public void Hit()
    {
        var playerCard = cardsDeck[Random.Range(0, cardsDeck.Count)];
        cardsDeck.Remove(playerCard);
        playerCards.Add(playerCard);
    }

    public void Stand()
    {
        isDealerTurn = true;
    }

    private void GiveDealerCards()
    {
        if (dealerHandValue < 17)
        {
            var dealerCard = cardsDeck[Random.Range(0, cardsDeck.Count)];
            cardsDeck.Remove(dealerCard);
            dealerCards.Add(dealerCard);
            CheckHandValues();
            CompareHandValues();
        }
        else if ((dealerHandValue > 17) && (dealerHandValue < 21))
        {
            CheckHandValues();
            CompareHandValues();
        }
        else if (dealerHandValue > 21)
        {
            CheckHandValues();
            //burst
        }
        else
        {
            CheckHandValues();

            DealerWins();
        }
    }

    private void CompareHandValues()
    {
        if (playerHandValue > dealerHandValue)
        {
            PlayerWins();
        }
        else DealerWins();
    }

    private void DealerWins()
    {
        gameOverText.text = "Dealer Wins";
    }

    private void PlayerWins()
    {
        gameOverText.text = "Player Wins";

    }
}
