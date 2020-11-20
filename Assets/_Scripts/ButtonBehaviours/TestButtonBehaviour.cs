using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Michael Dnekovski 101222288 Game 2014
/// TestButtonBehaviour.cs
/// Oct 3, 2020: test feature to move label positions (currently not being used)
/// </summary>

public class TestButtonBehaviour : MonoBehaviour
{

    public TMP_Text LivesLabel;
    public TMP_Text ScoreLabel;


    // Event Handler for the TestButton_Pressed Event
    public void OnTestButtonPressed()
    {
        Debug.Log("testButton Pressed");
        LivesLabel.rectTransform.anchoredPosition = new Vector2(370.0f, -83.0f);
        ScoreLabel.rectTransform.anchoredPosition = new Vector2(-353.0f, -83.0f);
    }
}
