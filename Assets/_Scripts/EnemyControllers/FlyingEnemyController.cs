using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

/// <summary>
/// Michael Dnekovski 101222288 Game 2014
/// FlyingEnemyController.cs
/// Last Edit Dec 8, 2020
/// - created flying enemy controller based on ground enemy controller
/// - detects player below the enemy
/// - turns if it detects anything infront of it
/// - stops to hover over the player
/// </summary>

public class FlyingEnemyController : EnemyController
{

    [Header("Movement")]
    public float speed = 10;
    //duration of time to pass before we switch direction in patrol
    public float PatrolDuration = 1.0f;
    public Transform groundCheckTransform;
    public LayerMask groundCheckLayers; // layers to detect ground
    private float TimeSinceLastPatrol;

    [Header("Detection")]
    //how far our line trace will go out
    public float DetectionRange = 10.0f;
    Vector3 DirectionFacing;
    public LayerMask playerLayer; // know where to find our player


    public EnemyStats stats;
    private float lastAttack;


    private GameObject playerCharacter;

    //AUdio effects
    public AudioSource AttackAudio;

    // Start is called before the first frame update
    void Start()
    {
        TimeSinceLastPatrol = Time.time;
        DirectionFacing = Vector3.left;
        //find the player so we can detect their proximity later
        playerCharacter = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //if player is in detection range
        if (_CheckForPlayer())
        {
            _MoveTowardPlayer();
        }
        else
        {
            _Patrol();
        }


    }

    /// <summary>
    /// check to see if we hit the player in our detection range
    /// </summary>
    /// <returns></returns>
    private bool _CheckForPlayer()
    {
        RaycastHit2D hit = Physics2D.Linecast(transform.position, transform.position + Vector3.down * DetectionRange, playerLayer);
        Debug.DrawLine(transform.position, transform.position + Vector3.down * DetectionRange, Color.red);
        //Debug.Log(hit.collider);
        if (hit)
        {
            if(hit.collider.gameObject.tag == "Player")
            {
                return true;

            }
        }

        return false;
    }

    /// <summary>
    /// move towards the player and initiate an attack if close enough
    /// </summary>
    private void _MoveTowardPlayer()
    {
        //find the distance to the player
        float distance = math.distance(playerCharacter.transform.position.x, transform.position.x);
        if (distance > 1 && CanMoveForward()) //player detected and can move forward
        {
            Rigidbody.velocity = (DirectionFacing * speed);
        }
        else if (_CheckForPlayer()) //in range and player is in our line of sight
        {
            Rigidbody.velocity = new Vector2(0.0f, 0.0f);
            Attack();
        }
        else // if fails just patrol
        {
            _Patrol();
        }
    }

    /// <summary>
    /// move the enemy left and right based on the patrol time 
    /// </summary>
    private void _Patrol()
    {
        if (Time.time - TimeSinceLastPatrol > PatrolDuration || !CanMoveForward())
        {
            //reverse out patrol direction
            DirectionFacing *= -1;
            TimeSinceLastPatrol = Time.time;
            transform.localScale = new Vector3(transform.localScale.x * -1.0f, transform.localScale.y, transform.localScale.z);
        }

        Rigidbody.velocity = (DirectionFacing * speed);
    }

    /// <summary>
    /// is the enemy able to move forward or is there no ground or a trap blocking the way
    /// </summary>
    /// <returns></returns>
    private bool CanMoveForward()
    {
        RaycastHit2D hit = Physics2D.Linecast(transform.position, groundCheckTransform.position, groundCheckLayers);
        Debug.DrawLine(transform.position, groundCheckTransform.position, Color.red);


        //if anything is in the way dont move towards it
        if (hit)
        {
            return false;
        }


        return true;
    }

    /// <summary>
    /// Perform our attack when called
    /// </summary>
    void Attack()
    {
        //check if the difference from the last time we attacked is greater than our attack speed in seconds
        if (Time.time - lastAttack > stats.AttackSpeed)
        {
            //Debug.Log("Attacking");
            //attack animation
            animator.SetTrigger("Attack");
            StartCoroutine(PlayDelayedAttackAudio());
            //set our last attack time to the current time
            lastAttack = Time.time;
            //deal guaranteed damage to our player
            playerCharacter.GetComponent<PlayerStats>().TakeDamage(stats.AttackPower);

        }

    }

    /// <summary>
    /// Play the audio after a delay for better timing
    /// </summary>
    /// <returns></returns>
    IEnumerator PlayDelayedAttackAudio()
    {
        yield return new WaitForSeconds(0.2f);
        //play our audio attack effect
        AttackAudio.Play();
    }
}
