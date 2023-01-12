using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Speed")]
    [SerializeField] float jumpSpeed = 10f;
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float climbSpeed = 10f;

    [Header("Timers")]
    [SerializeField] float deathTimer = 1f;
    
    [Header("Weapons")]
    [SerializeField] Transform weaponPrefab;
    [SerializeField] Vector2 throwSpeed = new Vector2(2,2);
    [SerializeField] float destroyAfterTime = 2f;
    
    private Rigidbody2D myRigidbody2D;
    private Vector2 moveInput;
    private BoxCollider2D myBoxCollider2D;
    private CapsuleCollider2D myCapsuleCollider2D; 
    private Animator myAnimator;
    private GameManager gameManager;
    private SoundManager soundManager;
    private bool isDead;
    private float myGravityScaleAtStart;

    private void Awake()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myGravityScaleAtStart = myRigidbody2D.gravityScale;
        myBoxCollider2D = GetComponent<BoxCollider2D>();
        myCapsuleCollider2D = GetComponent<CapsuleCollider2D>();
        myAnimator = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>();
        soundManager = FindObjectOfType<SoundManager>();
    }

    private void Update()
    {
        if(isDead){return;}
        move();
        flip();
        Climb(); 
    }

    private void Climb()
    {
        if (!myCapsuleCollider2D.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            myRigidbody2D.gravityScale = myGravityScaleAtStart;
            myAnimator.SetBool("isClimbing", false);
            return;
        }
        
        Vector2 climbVelocity = new Vector2(myRigidbody2D.velocity.x, moveInput.y * climbSpeed);
        myRigidbody2D.velocity = climbVelocity;
        myRigidbody2D.gravityScale = 0;

        bool playerHasVerticalSpeed = Math.Abs(myRigidbody2D.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("isClimbing", playerHasVerticalSpeed);

    }

    private void move()
    {
        myRigidbody2D.velocity = new Vector2(moveInput.x * runSpeed, myRigidbody2D.velocity.y);

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody2D.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(-Mathf.Sign(myRigidbody2D.velocity.x), 1f);
        }
    }

    private void flip()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody2D.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
    }

    private void OnMove(InputValue value)
    {
        if(isDead){return;}
        moveInput = value.Get<Vector2>();
    }

    private void OnJump(InputValue value)
    {
        if(isDead){return;}
        if (!myBoxCollider2D.IsTouchingLayers(LayerMask.GetMask("Platforms"))) { return; }

        if (value.isPressed)
        {
            myRigidbody2D.velocity += new Vector2(0, jumpSpeed);
            myAnimator.SetBool("isJumping", true);
            soundManager.PlayPlayerJumpSFX();
        }
    }

    private void OnFire(InputValue value)
    {
        Transform weapon = Instantiate(weaponPrefab, transform.position, Quaternion.identity);
        Vector2 throwDistance = new Vector2(-transform.localScale.x * (Mathf.Abs(myRigidbody2D.velocity.x)+ throwSpeed.x), myRigidbody2D.velocity.y + throwSpeed.y);

        weapon.GetComponent<Rigidbody2D>().velocity = throwDistance;

        Destroy(weapon.gameObject, destroyAfterTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
         if(isDead){return;}
        myAnimator.SetBool("isJumping", false);

        if(other.tag == "Enemy")
        {
            StartCoroutine(Die());
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(isDead){return;}
        if(other.gameObject.tag == "Hazards" || other.gameObject.tag == "Enemy")
        {
           StartCoroutine(Die());
        }    
    }

    private IEnumerator Die()
    {
        isDead = true;
        myRigidbody2D.velocity = Vector2.zero;
        myAnimator.SetBool("isDead", true); 
        soundManager.PlayerPlayerDeathSFX();
        yield return new  WaitForSeconds(deathTimer);
        gameManager.reloadScene();
    } 
}
