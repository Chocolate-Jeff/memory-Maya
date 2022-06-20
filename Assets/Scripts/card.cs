using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class card : MonoBehaviour {
    public Animator cardAnim1;
    public Image cardImage;
    public int cardNumber;
    
    public Sprite cardBack;
    public gameController game;

    public Sprite[] cards;

    // Use this for initialization, when the game is played / opened
    void Start () {
        cardAnim1 = GetComponent<Animator>();
        cardImage = GetComponent<Image>();
        game = FindObjectOfType<gameController>();
	}

    // Triggers when you click the card with the event trigger
    public void OnCardClick ()
    {
        if (cardImage.sprite == cardBack)
        {
            if (game.cardTimeLeft < 0.05f)
            {
                if (game.selectedCards.Count < 2)
                {
                    game.cardTimeLeft = .5f;
                    cardAnim1.Play("cardFlip");
                    StartCoroutine(cardChanger());
                    game.selectedCards.Add(this);
                    if (game.selectedCards.Count == 2)
                    {
                        game.CheckCards();
                    }
                }
            }
        }
    }

    IEnumerator cardChanger()
    {
        yield return new WaitForSeconds(.5f);
        cardImage.sprite = cards[cardNumber];
    }

    public void flipCard()
    {
        StartCoroutine(cardChangerBack());
    }

    IEnumerator cardChangerBack()
    {
        yield return new WaitForSeconds(2f);
        cardAnim1.Play("cardFlip");
        yield return new WaitForSeconds(.5f);
        cardImage.sprite = cardBack;
    }
}