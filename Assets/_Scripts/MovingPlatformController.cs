using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformController : MonoBehaviour
{
    public Transform start;
    public Transform end;
    public bool isActive;
    public float platformTimer;
    public float threshold;

    //get the player details from here
    public PlayerStats player;

    private Vector3 distance;


    // Start is called before the first frame update
    void Start()
    {
        platformTimer = 0;
        isActive = false;
        distance = end.position - start.position;

        player = FindObjectOfType<PlayerStats>();
        player.PlayerDeath.AddListener(_ResetSpawn);
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            platformTimer += Time.deltaTime;
            _Move();

        }
        else
        {
            var distanceStart = Vector3.Distance(player.transform.position, start.position);
            var distanceEnd = Vector3.Distance(player.transform.position, end.position);

            if (distanceStart < distanceEnd) //closer to the start position
            {
                if (!(Vector3.Distance(transform.position, start.position) < threshold))
                {
                    platformTimer += Time.deltaTime;
                    _Move();
                }
            }
            else // closer to the end position
            {
                if (!(Vector3.Distance(transform.position, end.position) < threshold))
                {
                    platformTimer += Time.deltaTime;
                    _Move();
                }
            }
        }
    }

    /// <summary>
    /// move the platform back and forth from the start position to the end postion in a ping pong manner
    /// </summary>
    private void _Move()
    {
        var distanceX = (distance.x > 0) ? start.position.x + Mathf.PingPong(platformTimer, distance.x) : start.position.x;
        var distanceY = (distance.y > 0) ? start.position.y + Mathf.PingPong(platformTimer, distance.y) : start.position.y;

        transform.position = new Vector3(distanceX,
                distanceY, 0.0f);
    }

    /// <summary>
    /// subscribes to unity event of player death to reset its position to the start
    /// </summary>
    private void _ResetSpawn()
    {
        platformTimer = 0;
        transform.position = start.position;
    }
}
