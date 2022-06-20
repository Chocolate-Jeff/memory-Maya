using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class gameController : MonoBehaviour {

    public bool singleplayer;
    public bool lost;
    public float timer;

    public int scoreAmount;
    public List<card> selectedCards;
    public int currentPlayer = 1;

    public int playerOneScore;
    public int playerTwoScore;

    [Header("Win Screen")]
    public List<card> correctCards;
    public cardSpawner spawner;
    public TextMeshProUGUI WinText;
    public GameObject WinPanel;

    [Header("UI")]
    public TextMeshProUGUI playerOneText;
    public TextMeshProUGUI playerTwoText;
    public TextMeshProUGUI whoTurn;
    public TextMeshProUGUI highText;

    [Header("Spam proof")]
    public float cardTimeLeft;

    [Header("Multiplayer")]
    private Multiplayer Manager;
    
    void Start() //when the game loads
    {
        Manager = GameObject.FindObjectOfType<Multiplayer>();
        Time.timeScale = 1; //starts the game time
        int diffi = PlayerPrefs.GetInt("diff"); // gets the current difficulty, selected from the main menu
        spawner = GameObject.FindObjectOfType<cardSpawner>(); //finds the card spawner object

        if (PlayerPrefs.GetInt("gamemode") == 1) // checks if singleplayer:
        {
            if (diffi == 0) //sets the time allowed based on difficulty
            {
                timer = 40; // 40 seconds
            }
            if (diffi == 1)
            {
                timer = 60; // 60 seconds
            }
            if (diffi == 2)
            {
                timer = 120; // 120 seconds
            }

            whoTurn.transform.localScale = new Vector3(0, 0, 0); // hides the "whoTurn" text box, as there is only one player

            singleplayer = true; //sets singleplayer to true
        }
        else
        {
            singleplayer = false; //sets singleplayer to false
        }
    }
    public void OnlineMultiplayerCheck()
    {
        if (selectedCards[0].cardNumber == selectedCards[1].cardNumber) //checks if the cards are the same and then :
        {
            if (currentPlayer == 1) //if the current player is player one:
            {
                playerOneScore += scoreAmount; //gives score to player one
            }
            else //current player is player two:
            {
                playerTwoScore += scoreAmount;//gives score to player two
            }

            correctCards.Add(selectedCards[0]); //adds the cards to correct cards so they can't be selected again
            correctCards.Add(selectedCards[1]);
        }
        else // if the cards are different
        {
            if (currentPlayer == 1) //if the current player is player 1 then:
            {
                playerOneScore -= scoreAmount / 10; //takes score away from player one
            }
            else // otherwise
            {
                playerTwoScore -= scoreAmount / 10; //takes score away from player two
            }

            foreach (card item in selectedCards) //flip the cards back over because they are not the same.
            {
                item.flipCard();
            }

            if (currentPlayer == 1) //passes the turn to the other player
            {
                currentPlayer = 2;
            }
            else
            {
                currentPlayer = 1;
            }
        }
        Manager.gameFile.playerOneScore = playerOneScore;
        Manager.gameFile.playerTwoScore = playerTwoScore;
        Manager.gameFile.currentPlayer = currentPlayer;
        Manager.sendData();
    }

        void Update() //every frame
    {
        if (cardTimeLeft > 0)
        {
            cardTimeLeft -= Time.deltaTime;
        }
        if (singleplayer == true) 
        {
            if (timer > 0) // if there is more than 0 seconds left then:
            {
                timer -= Time.deltaTime; //takes time off the timer. Counts down to 0
                playerTwoText.text = "Time: " + (timer).ToString("N0") + "s"; // sets the text to say how much time is left
            }
            else // otherwise
            {
                if (!lost) //the player has lost
                {
                    lost = true;
                    SinglePlayerEnd(); // run the single player end function
                }
            }
        }
    }
    void SinglePlayerEnd()
    {
        playerOneScore *= Mathf.RoundToInt(timer/10); //mutiplies the score by the amount of remaining time in seconds/10, reason for the player to play faster and beat their high score
        int highscore = PlayerPrefs.GetInt("highscore"); //gets the old highscore

        if (playerOneScore > highscore) //checks if the highscore is less that the player's score
        {
            PlayerPrefs.SetInt("highscore", playerOneScore); //set the new highscore
        }

        highText.text = "Highscore: " + highscore; //display the highscore on a text object

        if (timer > 0) // if there is time left then:
        {
            WinText.text = "YOU WIN!\n" + playerOneScore; //the player wins
        }
        else //otherwise, there is no time left, so game over
        {
            WinText.text = "GAME OVER!";
        }
        Time.timeScale = 0; //freezes "time" to stop the timer
        WinPanel.SetActive(true);
    }
    

    public void CheckCards() //whenever two cards are selected
    {
        if (singleplayer == true)
        {
            SingleCheck(); //run the single player card check function
        }
        else
        {
            PassNPlayCheck(); //run the multi player card check function
        }
    }

    public void SingleCheck()// checks if the cards are the same
    {
        if (selectedCards[0].cardNumber == selectedCards[1].cardNumber) //if they are the same then:
        {
            playerOneScore += scoreAmount; //gain some score

            correctCards.Add(selectedCards[0]); //add selected cards to correct cards list
            correctCards.Add(selectedCards[1]);
        }
        else //if they're not the same then:
        {
            playerOneScore -= scoreAmount / 10; //lose some score

            foreach (card item in selectedCards) //flips the selected cards back over
            {
                item.flipCard();
            }
        }

        selectedCards.Clear(); //deselects the cards so the player can guess more cards

        if (playerOneScore < 0) // if the player has a score less than 0:
        {
            playerOneScore = 0; //set the score to 0 (stops negative score)
        }

        RefreshScore(); //run RefreshScore function
    }

    public void PassNPlayCheck()
    {
        if (selectedCards[0].cardNumber == selectedCards[1].cardNumber) //checks if the cards are the same and then :
        {
            if (currentPlayer == 1) //if the current player is player one:
            {
                playerOneScore += scoreAmount; //gives score to player one
            }
            else //current player is player two:
            {
                playerTwoScore += scoreAmount;//gives score to player two
            } 

            correctCards.Add(selectedCards[0]); //adds the cards to correct cards so they can't be selected again
            correctCards.Add(selectedCards[1]);
        }
        else // if the cards are different
        {
            if (currentPlayer == 1) //if the current player is player 1 then:
            {
                playerOneScore -= scoreAmount / 10; //takes score away from player one
            }
            else // otherwise
            {
                playerTwoScore -= scoreAmount / 10; //takes score away from player two
            }

            foreach (card item in selectedCards) //flip the cards back over because they are not the same.
            {
                item.flipCard();
            }

            if (currentPlayer == 1) //passes the turn to the other player
            {
                currentPlayer = 2;
            }
            else
            {
                currentPlayer = 1;
            }
        }

        selectedCards.Clear(); //clears the selected cards so more cards can be selected

        if (playerTwoScore < 0) //stops negative scores
        {
            playerTwoScore = 0;
        }
        if (playerOneScore < 0)
        {
            playerOneScore = 0;
        }

        RefreshScore(); // runs the refresh score function
    }

    public void RefreshScore() 
    {
        playerOneText.text = playerOneScore.ToString(); //sets the player texts to the correct score
        playerTwoText.text = playerTwoScore.ToString();

        if (currentPlayer == 1) //displays which player's turn it is
        {
            whoTurn.text = "Player 1's turn";
        }
        else
        {
            whoTurn.text = "Player 2's turn";
        }

        if (correctCards.Count == spawner.cardAmount) //if all of the card combos have been found then:
        {
            if (!singleplayer) //if not singleplayer then:
            {
                if (playerOneScore > playerTwoScore) //display the winner
                {
                    WinText.text = "Player 1\nWins";
                }
                else
                {
                    WinText.text = "Player 2\nWins";
                }
                highText.gameObject.SetActive(false);
                WinPanel.SetActive(true); //Activates the end panel for winning / menu
            }
            else
            {
                SinglePlayerEnd(); //run the singleplayerend function
            }
        }
    }
}
