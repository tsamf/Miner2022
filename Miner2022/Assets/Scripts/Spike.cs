using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    [SerializeField] float dropTime = 1f;
    [SerializeField] float dropGravityScale = 1f;

    Animator myAnimator;
    bool isDropping = false;
    float timeSinceDroppingTrigger  = 0;
    Rigidbody2D myRigidBody2D;

    private void Awake() {
        myAnimator = GetComponent<Animator>();
        myRigidBody2D = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        if(isDropping)
        {
            timeSinceDroppingTrigger += Time.deltaTime;

            Debug.Log(timeSinceDroppingTrigger); 

            if(timeSinceDroppingTrigger > dropTime)
            {
                myRigidBody2D.gravityScale = dropGravityScale;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player")
        {
            myAnimator.SetTrigger("isWiggling");
            isDropping = true;
        }
    }
}
