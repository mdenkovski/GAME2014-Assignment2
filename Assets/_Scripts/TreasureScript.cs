using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Michael Dnekovski 101222288 Game 2014
/// TreasureScript.cs
/// Last Edit Oct 23, 2020
/// - transition game to end scene upon overlap with player
/// </summary>

public class TreasureScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //make sure the player enters the treasure
        if (collision.GetComponent<PlayerStats>() != null)
        {
            FindObjectOfType<GameController>().SetGameWonStatus(true);
            //transition to the end scene
            SceneManager.LoadScene("End");

        }
    }
}
