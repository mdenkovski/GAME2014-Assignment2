using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Michael Dnekovski 101222288 Game 2014
/// EndScreenManager.cs
/// Last Edit Oct 23, 2020
/// - take informaiton from the game stats and display it on the text
/// </summary>
public class EndScreenManager : MonoBehaviour
{
    private int m_score;
    private bool m_gameWon;

    public TMP_Text GameStatusText;
    public TMP_Text GameScoreText;

    // Start is called before the first frame update
    void Start()
    {
        //transfer the game stats from the previous scene to our manager
        GameStats stats = FindObjectOfType<GameStats>();
        if (stats != null) //make sure we have a valid stats object
        {
            m_score = stats.Score;
            m_gameWon = stats.GameWon;
            Destroy(stats.gameObject); //destroy the stats game object so we can make a new one if we play again


            if (m_gameWon) //if the player won
            {
                GameStatusText.text = "Congratulations You Win!!!";
            }
            else //player lost
            {
                GameStatusText.text = "GAME OVER!!!";
            }

            GameScoreText.text = "Your final score was: " + m_score.ToString();
        }

    }

}
