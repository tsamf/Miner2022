using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    [SerializeField] float movementSpeed = 10f;
    [SerializeField] int points = 100;
    [SerializeField] float deathTimer = 1f;

    private Rigidbody2D myRigidbody2D;
    private GameManager gameManager;
    private Animator myAnimator;
    private SoundManager soundManager;
    private bool isDying = false;

    void Awake()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>();
        soundManager = FindObjectOfType<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        myRigidbody2D.velocity = new Vector2(movementSpeed, 0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Platforms") { return; }
        if (other.tag == "PickAxe")
        {
            if(!isDying)
            {
                StartCoroutine(die());
            }
            
        }
        else
        {
            movementSpeed = -movementSpeed;
            FlipEnemyFacing();
        }
    }

    private IEnumerator die()
    {
        isDying = true;
        gameManager.AddPointsToScore(points);
        soundManager.PlaySlimeDeathSFX();
        myAnimator.SetTrigger("Death");
        movementSpeed = 0;
        gameObject.tag = "Untagged";
        yield return new WaitForSeconds(deathTimer);
        Destroy(gameObject);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Platforms")
        {
            movementSpeed = -movementSpeed;
            FlipEnemyFacing();
        }
    }

    private void FlipEnemyFacing()
    {
        transform.localScale = new Vector2(-transform.localScale.x, 1f);
    }
}
