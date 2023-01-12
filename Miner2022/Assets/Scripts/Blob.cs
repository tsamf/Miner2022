using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blob : MonoBehaviour
{
    [SerializeField]float movementSpeed = 10f;
    [SerializeField] int points = 100;

    private Rigidbody2D myRigidbody2D; 
    private GameManager gameManager;
    private SoundManager soundManager;

    void Awake()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        gameManager = FindObjectOfType<GameManager>();
        soundManager = FindObjectOfType<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        myRigidbody2D.velocity = new Vector2(movementSpeed, 0f);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        
        if(other.tag == "Platforms") {return;}
        if(other.tag == "PickAxe")
        {
            gameManager.AddPointsToScore(points);
            soundManager.PlayBlobDeathSFX();
            Destroy(gameObject); 
        }
        else
        {
            movementSpeed = -movementSpeed;
            FlipEnemyFacing();
        }
        
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Platforms")
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
