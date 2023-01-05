using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float jumpSpeed = 10f;
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float deathTimer = 1f;

    [Header("Sounds")]
    [SerializeField] AudioClip deathSFX;
    
    private Rigidbody2D myRigidbody2D;
    private Vector2 moveInput;
    private BoxCollider2D myBoxCollider2D;
    private Animator myAnimator;
    private GameManager gameManager;
     

    private void Awake()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myBoxCollider2D = GetComponent<BoxCollider2D>();
        myAnimator = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        myRigidbody2D.velocity = new Vector2(moveInput.x * runSpeed, myRigidbody2D.velocity.y);

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody2D.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(-Mathf.Sign(myRigidbody2D.velocity.x), 1f);
        }

        myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
    }

    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    private void OnJump(InputValue value)
    {
        if (!myBoxCollider2D.IsTouchingLayers(LayerMask.GetMask("Platforms"))) { return; }

        if (value.isPressed)
        {
            myRigidbody2D.velocity += new Vector2(0, jumpSpeed);
            myAnimator.SetBool("isJumping", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        myAnimator.SetBool("isJumping", false);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Hazards")
        {
           StartCoroutine(Die());
        }    
    }

    private IEnumerator Die()
    {
        myAnimator.SetBool("isDead", true); 
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position);
        yield return new  WaitForSeconds(deathTimer);
        gameManager.reloadScene();
    } 
}
