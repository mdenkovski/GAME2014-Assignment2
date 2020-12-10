using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Michael Dnekovski 101222288 Game 2014
/// TreasureScript.cs
/// Last Edit Dec 12, 2020
/// - transition game to end scene upon overlap with player
/// - able to choose which level the treasure leads to
/// </summary>

public class TreasureScript : MonoBehaviour
{
    public string levelToLoad;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //make sure the player enters the treasure
        if (collision.GetComponent<PlayerStats>() != null)
        {
            FindObjectOfType<GameController>().SetGameWonStatus(true);
            //transition to the end scene
            SceneManager.LoadScene(levelToLoad);

        }
    }
}
