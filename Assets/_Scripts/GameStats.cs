using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Michael Dnekovski 101222288 Game 2014
/// GameStats.cs
/// Last Edit Dec 12, 2020
/// - hold the game stats unto the next scene
/// - initialize score at 0
/// </summary>
public class GameStats : MonoBehaviour
{

    public int Score;
    public bool GameWon;

    // Start is called before the first frame update
    void Start()
    {
        Score = 0;
        DontDestroyOnLoad(gameObject);
    }

    
}
