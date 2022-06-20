using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cardSpawner : MonoBehaviour {
    //the amount of cards to spawn
    public int cardAmount;
    public GameObject cardPrefab;
    public Transform cardParent;

    public Sprite[] cards;

    public List<card> spawnedCards;

    [Header("Card Grids")]
    public Sprite[] cardsEasy;
    public Sprite[] cardsMedium;
    public Sprite[] cardsHard;

    void Start () //runs when the game has loaded
    {
        if (PlayerPrefs.GetInt("diff") == 0)//checks the difficulty: 
        {
            cards = cardsEasy;//sets the amount of cards to easy cardpack
        }
        if (PlayerPrefs.GetInt("diff") == 1)//checks the difficulty:
        {
            cards = cardsMedium;//sets the amount of cards to medium cardpack
        }
        if (PlayerPrefs.GetInt("diff") == 2)//checks the difficulty
        {
            cards = cardsHard;//sets the amount of cards to hard cardpack
        }

        cardAmount = cards.Length*2;
        for (int i = 0; i < cardAmount; i++)
        {
            card cardObject = Instantiate(cardPrefab, cardParent).GetComponent<card>();
            cardObject.cards = cards;

            bool runAgain = true;

            while (runAgain) {
                int randNum = Random.Range(0, cards.Length);
                int same = 0;

                foreach (card spawnedCard in spawnedCards)
                {
                    if (spawnedCard.cardNumber == randNum)
                    {
                        same += 1;
                    }
                }
                if (same < 2)
                {
                    cardObject.cardNumber = randNum;
                    spawnedCards.Add(cardObject);
                    runAgain = false;
                }
                else
                {
                    runAgain = true;
                }
            }
        }
	}
}
