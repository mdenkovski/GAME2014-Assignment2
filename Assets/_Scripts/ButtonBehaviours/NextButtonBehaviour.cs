using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



/// <summary>
/// Michael Dnekovski 101222288 Game 2014
/// NedxtButtonBehaviour.cs
/// Oct 3, 2020: simple load to end screen
/// </summary>

public class NextButtonBehaviour : MonoBehaviour
{
    // Event Handler for the StartButton_Pressed Event
    public void OnNextButtonPressed()
    {
        Debug.Log("NextButton Pressed");
        SceneManager.LoadScene("End");
    }
}
