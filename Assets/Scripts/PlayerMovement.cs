using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float runSpeed = 2f;
    [SerializeField] private float jumpSpeed = 5f;
    [SerializeField] private float climbSpeed = 5f;
    [SerializeField] private float playerGravity;
    [SerializeField] private Vector2 deathKick = new Vector2(10, 10);
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform gun;
    
    private Vector2 moveInput;
    private Rigidbody2D myRigidbody;
    private CapsuleCollider2D myBodyCollider;
    private BoxCollider2D myFeetCollider;
    private Animator myAnimator;
    private bool hasHorizontalSpeed;
    private bool hasVerticalSpeed;
    private int groundLayer;
    private bool isAlive = true;
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        myAnimator = GetComponent<Animator>();
        
        groundLayer = LayerMask.GetMask("Ground");

        playerGravity = myRigidbody.gravityScale;
    }

    void Update()
    {
        if (!isAlive) { return; }
        Run();
        TurnCharacter();
        ClimbLadder();
        Die();
    }

    void OnMove(InputValue value)
    {
        if (!isAlive) { return; }
        
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (!isAlive) { return; }
        
        if (value.isPressed && myFeetCollider.IsTouchingLayers(groundLayer))
        {
            myRigidbody.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    void OnFire(InputValue value)
    {
        if (!isAlive) { return; }

        Instantiate(bullet, gun.position, transform.rotation);
    }
    void Run()
    {
        hasHorizontalSpeed = Mathf.Abs(moveInput.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", hasHorizontalSpeed);

        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;
    }

    void TurnCharacter()
    {
        if (hasHorizontalSpeed) transform.localScale = new Vector2(Mathf.Sign(moveInput.x), 1f);
    }

    void ClimbLadder()
    {
        if (!isAlive) { return; }

        if (myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            hasVerticalSpeed = Mathf.Abs(moveInput.y) > Mathf.Epsilon;
            myAnimator.SetBool("isClimbing", hasVerticalSpeed);

            myRigidbody.velocity = new Vector2(moveInput.x * runSpeed, moveInput.y * climbSpeed);
            myRigidbody.gravityScale = 0f;
        }
        else
        {
            myAnimator.SetBool("isClimbing", false);
            myRigidbody.gravityScale = playerGravity;
        }
    }

    void Die()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazards")))
        {
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            myRigidbody.velocity = deathKick;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }
}