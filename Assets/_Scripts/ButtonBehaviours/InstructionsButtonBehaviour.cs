using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Michael Dnekovski 101222288 Game 2014
/// InstructionsButtonBehaviour.cs
/// Last edit Oct 24, 2020: 
/// - simple load to instructions screen
/// - added audio effect for button press
/// </summary>

public class InstructionsButtonBehaviour : MonoBehaviour
{
    //Audio Effect
    public AudioSource ButtonPressEffect;
    public void OnInstructionsButtonPressed()
    {
        Debug.Log("Instructions Button Pressed");
        ButtonPressEffect.Play();
        SceneManager.LoadScene("Instructions");
    }
}
