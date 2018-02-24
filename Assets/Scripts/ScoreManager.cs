using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {
    public int Player1Score
    {
        get { return player1Score; }
        private set
        {
            player1Score = value;
            Player1Text.text = player1Score.ToString();
        }
    }

    public int Player2Score
    {
        get { return player2Score; }
        private set
        {
            player2Score = value;
            Player2Text.text = player2Score.ToString();
        }
    }

    public Text Player1Text;
    public Text Player2Text;

    private int player1Score;
    private int player2Score;


    void Awake ()
	{
	    Player1Score = 0;
	    Player1Score = 0;
	}

    public void IncreasePlayer1Score()
    {
        Player1Score++;
    }

    public void IncreasePlayer2Score()
    {
        Player2Score++;
    }
}
