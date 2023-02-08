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
    public GameObject replayButton;
    public GameObject standButton;
    public GameObject hitButton;

    public int previousDeckCardsNum;

    public Transform handTransform;
    private Vector3 handTransformBasePosition;

    public int playerHandValue;
    public int dealerHandValue;

    public TMP_Text handValueText;
    public TMP_Text dealerHandValueText;
    public TMP_Text gameOverText;

    private bool isDealerTurn;
    private bool isDealerCourutineRunning;

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
        replayButton.SetActive(false);

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
            if(!isDealerCourutineRunning) StartCoroutine(GiveDealerCards());
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
            var playerCard1 = cardsDeck[Random.Range(0, cardsDeck.Count)];
            cardsDeck.Remove(playerCard1);
            playerCards.Add(playerCard1);
        }

        for (int i = 0; i < 2; i++)
        {
            //gets random card, giver to player and removes from deck
            var dealerCard1 = cardsDeck[Random.Range(0, cardsDeck.Count)];
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

        foreach (string card in playerCards) //displays players cards
        {
            var cardObj = Instantiate(cardPrefab, handTransform.position, Quaternion.identity);
            cardObj.GetComponent<CardObjectScript>().cardName = card;
            cardObj.transform.SetParent(handTransform, true);
            cardObj.transform.localScale = new Vector3(1, 1, 1);
            cardObj.transform.position = handTransform.position;

            playerCardsGameObjects.Add(cardObj);
            cardObj.transform.position = new Vector3(handTransform.position.x + (25 * playerCardsGameObjects.Count), handTransform.position.y, handTransform.position.z);
        }
        handTransform.position = handTransformBasePosition;

        foreach (string card in dealerCards)  //displays dealer cards
        {
            var cardObj = Instantiate(cardPrefab, new Vector3(handTransform.position.x, handTransform.position.y + 50, handTransform.position.z), Quaternion.identity);
            cardObj.GetComponent<CardObjectScript>().cardName = card;
            cardObj.transform.SetParent(handTransform, true);
            cardObj.transform.localScale = new Vector3(1, 1, 1);
            dealerCardsGameObjects.Add(cardObj);
            if ((!isDealerTurn) && (!isDealerCourutineRunning))
            {
                if (dealerCardsGameObjects[0] == cardObj) cardObj.GetComponent<CardObjectScript>().cardName = "?";
            }


            cardObj.transform.position = new Vector3(handTransform.position.x + (25 * dealerCardsGameObjects.Count), handTransform.position.y + 50, handTransform.position.z);
        }
        handTransform.position = handTransformBasePosition;
        CheckHandValues();
    }

    private void CheckHandValues() //updates the value of the player and dealer`s hands
    {
        playerHandValue = 0;
        dealerHandValue = 0;
        foreach(GameObject card in playerCardsGameObjects)
        {
            card.GetComponent<CardObjectScript>().CheckCardValueAndType();
            var CardValue = card.GetComponent<CardObjectScript>().value;
            playerHandValue += CardValue;
        }
        handValueText.text = "your hand: " + playerHandValue.ToString();
        if (playerHandValue > 21)
        {
            DealerWins();
            dealerCardsGameObjects[0].GetComponent<CardObjectScript>().cardName = dealerCards[0];
        }

        foreach (GameObject card in dealerCardsGameObjects)
        {
            card.GetComponent<CardObjectScript>().CheckCardValueAndType();

            var CardValue = card.GetComponent<CardObjectScript>().value;
            dealerHandValue += CardValue;
        }
        if ((!isDealerTurn) && (!isDealerCourutineRunning))
        {
            dealerHandValueText.text = "dealer hand: ?";
        }

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

    IEnumerator GiveDealerCards()
    {
        isDealerCourutineRunning = true;

        while (isDealerTurn)
        {
            dealerCardsGameObjects[0].GetComponent<CardObjectScript>().cardName = dealerCards[0];
            CheckHandValues();
            yield return new WaitForSeconds(1);

            print("num" + dealerCardsGameObjects.Count);
            print("card" + dealerHandValue);
            if ((dealerHandValue > playerHandValue) && (dealerHandValue < 17)) DealerWins();

            else if (dealerHandValue < 17)
            {
                print("1");
                var dealerCard = cardsDeck[Random.Range(0, cardsDeck.Count)];
                cardsDeck.Remove(dealerCard);
                dealerCards.Add(dealerCard);
                if (dealerHandValue > playerHandValue) DealerWins();
            }
            else if ((dealerHandValue >= 17) && (dealerHandValue < 21))
            {
                print("2");
                CompareHandValues();
            }
            else if (dealerHandValue > 21)
            {
                print("3");
                PlayerWins();
                //burst
            }
            else
            {
                print("4");
                DealerWins();
            }
            UpdateCardsOnHands();
            CheckHandValues();
            yield return new WaitForSeconds(1);
        }
    }

    private void CompareHandValues()
    {
        isDealerTurn = false;   
        if (playerHandValue > dealerHandValue)
        {
            PlayerWins();
        }
        else DealerWins();
    }

    private void DealerWins()
    {
        hitButton.SetActive(false);
        standButton.SetActive(false);
        isDealerTurn = false;

        replayButton.SetActive(true);

        gameOverText.text = "Dealer Wins";
    }

    private void PlayerWins()
    {
        hitButton.SetActive(false);
        standButton.SetActive(false);
        isDealerTurn = false;
        replayButton.SetActive(true);

        gameOverText.text = "Player Wins";

    }

    public void Replay()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
