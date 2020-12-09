using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Michael Denkovski 101222288 Game 2014
/// CheckpointScript.cs
/// Last Edit Dec 8, 2020
/// - update player repawn when player triggers checkpoint collision
/// </summary>
public class CheckpointScript : MonoBehaviour
{
    public Transform spawnPoint;
    //public PlayerBehaviour player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            player.respawn = spawnPoint;
        }
    }
}
