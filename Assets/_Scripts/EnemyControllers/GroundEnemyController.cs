using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;


/// <summary>
/// Michael Dnekovski 101222288 Game 2014
/// GroundEnemyController.cs
/// Last Edit Dec 8, 2020
/// - added simple AI to move towards player if within certain range
/// - animations based on actions
/// - attack the player and deal damage if within a certain range
/// - reworked AI to use line tracing to detect the player
/// - movement based on rigidbody
/// - patroling behaviour
/// - move toward player when detected
/// - added audio effect for attacking
/// - added ground check for patrolling
/// </summary>

public class GroundEnemyController : MonoBehaviour
{
    public Animator animator;
    //our body that we are moving
    public Rigidbody2D Rigidbody;

    [Header("Movement")]
    public float speed = 5;
    //duration of time to pass before we switch direction in patrol
    public float PatrolDuration = 3.0f;
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
    //use to manipulate the direction of our sprite
    private SpriteRenderer spriteRenderer;

    //AUdio effects
    public AudioSource AttackAudio;

    // Start is called before the first frame update
    void Start()
    {
        TimeSinceLastPatrol = Time.time;
        DirectionFacing = Vector3.left;
        spriteRenderer = GetComponent<SpriteRenderer>();
        //find the player so we can detect their proximity later
        playerCharacter = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //if player is in detection range
        if(_CheckForPlayer())
        {
            _MoveTowardPlayer();
        }
        else
        {
            _Patrol();
        }
        animator.SetFloat("Speed", math.abs(Rigidbody.velocity.x));

        
    }

    /// <summary>
    /// check to see if we hit the player in our detection range
    /// </summary>
    /// <returns></returns>
    private bool _CheckForPlayer()
    {
        RaycastHit2D hit = Physics2D.Linecast(transform.position, transform.position + DirectionFacing * DetectionRange, playerLayer);
        Debug.DrawLine(transform.position, transform.position + DirectionFacing * DetectionRange, Color.red);
        //Debug.Log(hit.collider);
        if (hit.collider != null)
        {
            return true;
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
        else if(_CheckForPlayer()) //in range and player is in our line of sight
        {
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
        if(Time.time - TimeSinceLastPatrol > PatrolDuration || !CanMoveForward())
        {
            //reverse out patrol direction
            DirectionFacing *= -1;
            //spriteRenderer.flipX = (DirectionFacing.x == 1? false :true);
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

        

        if(hit)
        {
            if (hit.collider.gameObject.tag == "Trap")
            {
                //Debug.Log("Trap detected");
                return false;
            }

            //Debug.Log("Ground hit");
            return true;
        }


        return false;
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
