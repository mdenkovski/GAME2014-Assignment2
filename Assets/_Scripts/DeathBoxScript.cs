using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Michael Dnekovski 101222288 Game 2014
/// DeathBoxScript.cs
/// Last Edit Oct 23, 2020
/// - kill the player if they enter the trigger
/// </summary>

public class DeathBoxScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Player Entered Collider");
        //kill the player when they enter the collider
        PlayerStats stats = collision.GetComponent<PlayerStats>();
        if(stats != null) //make sure a valid player enters the trigger
        {
            stats.Death();
        }
    }


    
}
