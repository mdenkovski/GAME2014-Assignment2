using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// Michael Dnekovski 101222288 Game 2014
/// BackButtonBehaviour.cs
/// Last edit Oct 3, 2020: 
/// - simple load to start screen
/// - added audio effect for button press
/// </summary>

public class BackButtonBehaviour : MonoBehaviour
{
    //Audio Effect
    public AudioSource ButtonPressEffect;

    // Event Handler for the StartButton_Pressed Event
    public void OnBackButtonPressed()
    {
        Debug.Log("BackButton Pressed");
        ButtonPressEffect.Play();
        SceneManager.LoadScene("Start");
    }
}
