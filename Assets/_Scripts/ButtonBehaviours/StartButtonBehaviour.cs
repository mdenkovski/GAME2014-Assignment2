using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// Michael Dnekovski 101222288 Game 2014
/// StartButtonBehaviour.cs
/// Last Edit Oct 3, 2020: 
/// - simple load to play scene
/// - added audio effect for button press
/// </summary>

public class StartButtonBehaviour : MonoBehaviour
{

    //Audio Effect
    public AudioSource ButtonPressEffect;

    // Event Handler for the StartButton_Pressed Event
    public void OnStartButtonPressed()
    {
        Debug.Log("StartButton Pressed");
        ButtonPressEffect.Play();
        SceneManager.LoadScene("Play");
    }
}
