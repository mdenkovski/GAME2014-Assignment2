using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Michael Dnekovski 101222288 Game 2014
/// PotionScript.cs
/// Last Edit Oct 23, 2020
/// - adds 1 life to player upon trigger
/// </summary>

public class PotionScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerStats playerStats = collision.GetComponent<PlayerStats>();
        //make sure the player picks up the coins
        if (playerStats != null)
        {
            //increase the players lives by 1
            playerStats.AddLife();
            Destroy(gameObject);
        }
    }
}
