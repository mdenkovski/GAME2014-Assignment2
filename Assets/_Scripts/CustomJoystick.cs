using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Michael Dnekovski 101222288 Game 2014
/// CustomJoystick.cs
/// Last Edit Dec 8, 2020
/// - created joystic to hold a normalized direction in where the player is touching the screen vs the touch pad
/// - renamed to custom joystick to differentiate with imported ones
/// </summary>
public class CustomJoystick : MonoBehaviour
{
    private Vector2 m_InputDirection;
    public Vector2 InputDirection
    {
        get { return m_InputDirection; }
        set { m_InputDirection = value; }
    }

    //image of the thumb pad
    public Image PadImage;

    private void Start()
    {
        //dont want pad visible on start
        PadImage.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var touch in Input.touches)
        {
            //Debug.Log("Touch Position: " + touch.position.ToString());
            //Debug.Log("Transform Position: " + transform.position.ToString());

            //check if we touch outside of  our circle
            if (math.distance(touch.position, new Vector2(transform.position.x, transform.position.y)) < 300 )
            {
                //Debug.Log("In Circle");
                //set the direction vector of our movememnt and normalize it
                InputDirection = touch.position - new Vector2(transform.position.x, transform.position.y);
                InputDirection = InputDirection.normalized;

                //Debug.Log(InputDirection);

                //update the pad image to provide feedback to the player
                PadImage.enabled = true;
                PadImage.transform.position = touch.position; //set the position of the pad based on the touch position

            }
            else
            {
                //if we are not touching the pad close enough then will not use it
                PadImage.enabled = false;
                InputDirection = new Vector2(0.0f, 0.0f);
            }
        }
        if(Input.touches.Length == 0)
        {
            //Debug.Log("Not Touching");
            InputDirection = new Vector2(0.0f, 0.0f);
            PadImage.enabled = false;
        }
    }
}
