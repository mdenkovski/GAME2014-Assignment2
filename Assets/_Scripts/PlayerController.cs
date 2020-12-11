using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Mathematics;
using UnityEngine;

/// <summary>
/// Michael Denkovski 101222288 Game 2014
/// PlayerController.cs
/// Last Edit Dec 10, 2020
/// - added movement to take input from our joystick
/// - added animation transitions based on actions
/// - added player attack
///  - attack in correct direction
///  - respawn feature
///  - added audio effect for swinging weapon
///  - implemented asset joystick instead of custom joystick
///  - attach and detatch from moving platforms when interact with them
///  - make sure we are not dead before attacking as well
/// </summary>

public class PlayerController : MonoBehaviour
{
    //the joystick we use to control the player
    public Joystick joystick;
    //our body that we are moving
    public Rigidbody2D Rigidbody;

    public Transform respawn;

    //how fast we move
    public float Speed = 5;

    [Header("Jumping")]
    //how strong we jump
    public float JumpPower  = 5;
    [SerializeField]
    bool isJumping;
    [SerializeField]
    bool isGrounded;
    public LayerMask collisionGroundLayer;

    [Header("Effects")]
    //use to control our animations
    public Animator animator;
    //use to manipulate the direction of our sprite
    private SpriteRenderer spriteRenderer;

    //AUdio effects
    public AudioSource SwingWeaponAudio;


    //all our variables we need to control our attacks
    [Header("Attacking")]
    public Transform AttackPositoin;
    public float AttackRange = 0.2f; // how far from the attack position we can reach
    public LayerMask enemyLayers; // know where to find our enemies
    public float AttackPower = 10; //how much damage we deal
    public float AttackSpeed = 1.0f; // how many seconds need to pass for us to attack
    private float lastAttack;
    private Vector3 m_direction;


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Respawn();
    }


    // Update is called once per frame
    void Update()
    {
        //parse movement
        if (joystick.Horizontal> 0.2f)//move right
        {
            //Debug.Log("Moving Right");
            m_direction = Vector3.right;
            Rigidbody.velocity = new Vector2(Speed , Rigidbody.velocity.y);
            spriteRenderer.flipX = false;
        }
        else if (joystick.Horizontal < -0.2f) // move left
        {
            m_direction = Vector3.left;
            //flip our sprite to face the opposite direction
            spriteRenderer.flipX = true;
            //Debug.Log("Moving Left");
            Rigidbody.velocity = new Vector2(-Speed , Rigidbody.velocity.y);
        }
        else //dont move left or right
        {
            //dont want to move left or right here
            Rigidbody.velocity = new Vector2(0.0f, Rigidbody.velocity.y);
        }
        animator.SetFloat("Speed", math.abs(Rigidbody.velocity.x));


        if (joystick.Vertical > 0.6f) //jump
        {
            if (IsGrounded() && !isJumping) //if near gorund level and not already jumping
            {
                //Debug.Log("Jumping");
                //preserve our x velocit while jumping
                Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, JumpPower);
                isJumping = true;
                animator.SetBool("IsJumping", true);
            }
        }
        else
        {
            //not jumping
            isJumping = false;
            animator.SetBool("IsJumping", false);
        }

        //Debug.DrawLine(transform.position, transform.position +  new Vector3(joystick.InputDirection.normalized.x, joystick.InputDirection.normalized.y, 0.0f)*5);

        Debug.DrawLine(AttackPositoin.position, AttackPositoin.position + m_direction * AttackRange, Color.red);
    }

    /// <summary>
    /// check if the ground is below us
    /// </summary>
    /// <returns></returns>
    private bool IsGrounded()
    {
        RaycastHit2D groundHit = Physics2D.CircleCast(transform.position - new Vector3(0.0f, 0.75f, 0.0f), 0.3f, Vector2.down, 0.2f, collisionGroundLayer);
        if(groundHit)
        {
            isGrounded = true;
            return true;
        }
        return false;
    }

    /// <summary>
    /// draw the circle cast gizmo
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;

        Gizmos.DrawWireSphere(transform.position - new Vector3(0.0f, 0.75f, 0.0f), 0.3f);
    }

    /// <summary>
    /// Call when we press the attack button to perform our attack
    /// </summary>
    public void Attack()
    {
        //check if the difference from the last time we attacked is greater than our attack speed in seconds and are not dead
        if (Time.time - lastAttack > AttackSpeed && !GetComponent<PlayerStats>().IsDead())
        {
            //Debug.Log("Attacking");
            animator.SetTrigger("Attack"); //attack animation
            SwingWeaponAudio.Play();
            RaycastHit2D hit = Physics2D.Linecast(AttackPositoin.position, AttackPositoin.position + m_direction * AttackRange, enemyLayers);
           // Debug.Log(hit.collider);
            if (hit.collider != null)
            {
                hit.collider.GetComponent<EnemyStats>().TakeDamage(AttackPower);
            }
            //set our last attack time to the current time
            lastAttack = Time.time;
        }
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "FlyingEnemy")
        {
            other.gameObject.GetComponent<EnemyStats>().TakeDamage(AttackPower);
        }

        if (other.gameObject.tag  == "Moving Platform")
        {
            other.gameObject.GetComponent<MovingPlatformController>().isActive = true;
            transform.SetParent(other.gameObject.transform);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag  == "Moving Platform")
        {
            other.gameObject.GetComponent<MovingPlatformController>().isActive = false;
            transform.SetParent(null);
        }
    }

    /// <summary>
    /// respawn the player and reset parameters
    /// </summary>
    public void Respawn()
    {
        m_direction = Vector3.right;
        transform.position = respawn.position;
        animator.SetBool("IsDead", false);
        animator.SetTrigger("Respawn");
        isJumping = false;
        isGrounded = false;
    }
}
