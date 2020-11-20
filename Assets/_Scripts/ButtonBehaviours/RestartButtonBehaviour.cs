using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



/// <summary>
/// Michael Dnekovski 101222288 Game 2014
/// RestartButtonBehaviour.cs
/// Last Update Oct 24, 2020: 
/// - simple load to play screen
/// - added audio effect play
/// </summary>

public class RestartButtonBehaviour : MonoBehaviour
{
    //Audio Effect
    public AudioSource ButtonPressEffect;

    // Event Handler for the StartButton_Pressed Event
    public void OnRestartButtonPressed()
    {
        Debug.Log("RestartButton Pressed");
        ButtonPressEffect.Play();
        SceneManager.LoadScene("Play");
    }
}
