using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Michael Dnekovski 101222288 Game 2014
/// RotatingPlatformController.cs
/// Last Edit Dec 8, 2020
/// - added rotation based on rotatoin speed and delay timers
/// </summary>

public class RotatingPlatformController : MonoBehaviour
{
    public float platformTimer;
    public float RotatingDelay;
    public float RotationSpeed;
    public bool isActive;

    private Vector3 lastRotation;
    private Vector3 targetRotation;

    // Start is called before the first frame update
    void Start()
    {
        platformTimer = 0;

        isActive = false;
        lastRotation = new Vector3(0.0f, 0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(platformTimer > RotatingDelay)
        {
            platformTimer = 0.0f;
            isActive = true;
            targetRotation = lastRotation + new Vector3(0.0f, 0.0f, 180.0f);
            if (targetRotation.z == 540)
            {
                targetRotation.z = 180;
            }
        }
        if(isActive)
        {
            _Rotate();
    
        }
        platformTimer += Time.deltaTime;
    }


    /// <summary>
    /// rotate the platform to a target rotation based on the rotation speed
    /// </summary>
    private void _Rotate()
    {
        Vector3 Rotation  =  Vector3.Lerp(lastRotation, targetRotation, platformTimer / RotationSpeed);
        transform.rotation = Quaternion.Euler(Rotation);
        if (platformTimer > RotationSpeed)
        {
            lastRotation = transform.rotation.eulerAngles;
            isActive = false;
        }
    }
}
