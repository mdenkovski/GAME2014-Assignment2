using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Michael Dnekovski 101222288 Game 2014
/// EnemyStats.cs
/// Last Edit Oct 24, 2020
/// - added stats that an enemy would need
/// - added take damage and death functions to control those functions
/// - give score when die
/// - added audio effect for taking hit and death
/// </summary>
public class EnemyStats : MonoBehaviour
{
    //parameters for control
    public EnemyController Controller;
    public BoxCollider2D Collider;
    public Animator animator;

    //for updating the score
    public int scoreValue = 10;
    private GameController gameController;

    //set our max health in editor
    public float maxHealth;
    //health is initialized to max health on start
    private float health;

    public float AttackPower = 10;
    public float AttackSpeed = 1.0f;

    //AUdio effects
    public AudioSource TakeHitAudio;
    public AudioSource DeathAudio;

    // Start is called before the first frame update
    void Start()
    {
        //set health to max health
        health = maxHealth;
        gameController = FindObjectOfType<GameController>();
    }

    /// <summary>
    /// call whenever we take damage of x amount
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(float damage)
    {
        health -= damage;
        TakeHitAudio.Play();
        animator.SetTrigger("Hit");

        if(health <= 0)
        {
            _Death();
        }
    }

    /// <summary>
    /// when we die deactivate all active components
    /// </summary>
    private void _Death()
    {
        Debug.Log("Dead");
        animator.SetBool("IsDead",true);
        //every time the enemy dies increase the game score by x amount
        gameController.IncreaseScore(scoreValue);
        DeathAudio.Play();
        //disable our enemy
        Controller.Rigidbody.gravityScale = 0;
        //Controller.Rigidbody.velocity = new Vector3();
        Controller.enabled = false;
        Collider.enabled = false;
        this.enabled = false;
    }

    
}
