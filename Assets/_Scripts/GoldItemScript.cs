using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Michael Dnekovski 101222288 Game 2014
/// GoldItemScript.cs
/// Last Edit Oct 23, 2020
/// - adds x amount of score to player upon trigger
/// - update the score
/// </summary>

public class GoldItemScript : MonoBehaviour
{
    public int PointValue;

    private GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //make sure the player picks up the coins
        if(collision.GetComponent<PlayerStats>() != null)
        {
            gameController.IncreaseScore(PointValue);
            Destroy(gameObject);

        }
    }
}
